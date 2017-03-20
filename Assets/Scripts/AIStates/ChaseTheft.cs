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
		
		m_Agent = _context.GetComponent<UnityEngine.AI.NavMeshAgent>();
		m_Agent.updatePosition = false;
		m_Agent.updateRotation = false;
	}
		
	/**
		TODO: Is a crystall near?
		=> Reference to Crystal-Manager?
			=> Check his array, before searching by tag everytime

		=> Variables in AIInput? (MinDistToCrystal/Player)
	
	**/
	public override void reason() {
		// can we see the player?
		RaycastHit hit;
		bool canSeeTheft = false;
		
		if( Physics.Raycast( m_Character.position, m_Target.position - m_Character.position, out hit ) ) {
			if( hit.collider.tag == "Theft" ) {
				canSeeTheft = true;
			} 
		}
		//Debug.Log (_context.m_Character.m_SpellReloadCounter);
		// Shoot at player TODO: TEMP
		if(canSeeTheft && _context.m_Character.m_SpellReloadCounter >= _context.m_Character.m_SpellReloadTime/*_context.m_Character.m_CrystalLoads == 3*/) {

			// Check if CM is actually looking at the Theft
			Vector3 toTheft = m_Target.position - m_Character.position;
			Vector3 lookAt = m_Character.forward;

			if (Vector3.Dot (toTheft, lookAt) > 0) {

				// Only shoot with chance
				if(Random.Range(0,100) < m_ShootChance) {		
					Debug.Log("Shoot");

					// Look at theft
					_context.m_Character.transform.rotation = Quaternion.LookRotation(toTheft);

					_context.m_Character.Action();
					return;
				}	
			}
		} 


		List<GameObject> crystals = CrystalManager.instance.getCrystals ();

		// Abort if we got enough crystals or there are no to find
		if (_context.m_Character.m_CrystalLoads >= 3 || crystals.Count == 0) {
			return;
		}

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

		//Debug.Log (distanceToPlayer);

		// If Player is very near  | TODO: Formel verbessern
		//if ( distanceToPlayer < 9 && (minDistance + (distanceCrystallToPlayer*0.5) < distanceToPlayer)) {

		if(distanceToPlayer < 5 && distanceToPlayer > minDistance){
			_context.m_TargetedCrystal = nearestCrystal;
			_machine.changeState<CollectCrystal>();
			Debug.Log ("Player Far enough");
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
	 * Moves the character
	 * */
	public override void update( float deltaTime ) {

		if(m_Agent.enabled) {
			m_Agent.SetDestination(m_Target.position);
		}

		// TODO: TEMP
		if (m_Agent.enabled == false) {
			return;
		}

		Vector3 velocity = m_Agent.nextPosition - m_Character.position;

		// Max. -0.8
		// Debug.Log (velocity);

		_context.m_Character.Move(velocity);
	}
}
