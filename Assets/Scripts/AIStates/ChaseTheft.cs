using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AI.FiniteStateMachine;

public class ChaseTheft : FSMState<AIInput>
{
	private UnityEngine.AI.NavMeshAgent m_Agent;
	
	private float m_ShootChance = 1;
	
	private Transform m_Target;
	private Transform m_Character;
	
	public override void begin() {
		Debug.Log( "started chasing" );
		
		m_Target = _context.m_Theft.transform;
		m_Character = _context.m_Character.transform;

		// Disable automatic AI-Update
		m_Agent = _context.GetComponent<UnityEngine.AI.NavMeshAgent>();
		m_Agent.updatePosition = false;
		m_Agent.updateRotation = false;
	}
		

	public bool ShootingAllowed() {
		return !((GlobalConfig.METAL_MODE && _context.m_Character.m_SpellReloadCounter < _context.m_Character.m_SpellReloadTime) ||
			(!GlobalConfig.METAL_MODE && _context.m_Character.m_CrystalLoads < 3));
	}

	/**
	 * Tries to shoot at player
	 * */
	public bool CheckShooting(){
		// can we see the player?
		RaycastHit hit;
		bool canSeeTheft = false;

		if( Physics.Raycast( m_Character.position, m_Target.position - m_Character.position, out hit ) ) {
			if( hit.collider.tag == "Theft" ) {
				canSeeTheft = true;
			} 
		}

		// check if shooting allowed
		if(canSeeTheft && ShootingAllowed()){
			// Check if CM is actually looking at the Theft
			Vector3 toTheft = m_Target.position - m_Character.position;
			Vector3 lookAt = m_Character.forward;

			if (Vector3.Dot (toTheft, lookAt) > 0) {

				// Only shoot with chance 1:100
				if(Random.Range(0,100) < m_ShootChance) {		
					// Look directly at theft
					_context.m_Character.transform.rotation = Quaternion.LookRotation(toTheft);
					// Shoot
					_context.m_Character.Action();
					return true;
				}	
			}
		} 
		return false;
	}

	/**
		 * Check if CM should rather collect a crystal than follow the player
	* */
	public void CheckCrystals(List<GameObject> crystals){

		// Get nearest Crystal
		float minDistance = float.MaxValue;
		GameObject nearestCrystal = null;

		foreach( GameObject c in crystals ) {

			float distanceToCrystal = Vector3.Distance (m_Character.position, c.transform.position);	// TODO: Faile bc of not deletion of crystalmanager

			if (distanceToCrystal < minDistance) {
				minDistance = distanceToCrystal;
				nearestCrystal = c;
			}
		}

		float distanceToPlayer = Vector3.Distance (_context.m_Theft.transform.position, _context.m_Character.transform.position);
		float distanceCrystallToPlayer = Vector3.Distance (_context.m_Theft.transform.position, nearestCrystal.transform.position);

		// If Player is very near  | TODO: Formel verbessern
		//if ( distanceToPlayer < 9 && (minDistance + (distanceCrystallToPlayer*0.5) < distanceToPlayer)) {

		// If difference between crystal- and playerdistance big enough, target crystal
		if(distanceToPlayer < 5 && distanceToPlayer > minDistance){
			Debug.Log ("Player Far enough");
			_context.m_TargetedCrystal = nearestCrystal;
			_machine.changeState<CollectCrystal>();
		}


		if (distanceToPlayer > 8 && distanceToPlayer > minDistance) {
			Debug.Log ("Crystal Near enough");
			_context.m_TargetedCrystal = nearestCrystal;
			_machine.changeState<CollectCrystal>();
		}

		//if(distanceToPlayer < 8) {
		/*context.m_TargetedCrystal = nearestCrystal;
			_machine.changeState<CollectCrystal>();
			Debug.Log ("Crystal Near enough");*/
		// If Crystal is near enough
		/*} else {//if(minDistance < 14) {
			_context.m_TargetedCrystal = nearestCrystal;
			_machine.changeState<CollectCrystal>();
			Debug.Log ("Player Far enough");
		}*/

	}


	/**
		TODO: Is a crystall near?
		=> Reference to Crystal-Manager?
			=> Check his array, before searching by tag everytime
		=> Variables in AIInput? (MinDistToCrystal/Player)
		**/
	public override void reason() {

		// If already shot, we can abort
		if (CheckShooting ()) {
			return;
		}

		// If the Theft is trapped, ignore all crystals
		if (_context.m_Theft.IsTrapped ()) {
			return;
		}

		List<GameObject> crystals = CrystalManager.instance.getCrystals ();

		// If there are some crystals the CM needs and Metal_mode is inactive
		if (_context.m_Character.m_CrystalLoads < 3 && crystals.Count > 0 && GlobalConfig.METAL_MODE == false) {
			CheckCrystals(crystals);
		}
	}
		
	/**
	 * Moves the character
	 * */
	public override void update( float deltaTime ) {

		if(m_Agent.enabled) {
			m_Agent.SetDestination(m_Target.position);
		}
			
		Vector3 velocity = m_Agent.nextPosition - m_Character.position;
		_context.m_Character.Move(velocity);
	}
}
