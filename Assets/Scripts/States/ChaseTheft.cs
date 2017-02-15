using UnityEngine;
using System.Collections;
using AI.FiniteStateMachine;

public class ChaseTheft : FSMState<AIInput>
{
	private UnityEngine.AI.NavMeshAgent m_Agent;
	
	private Transform m_Target;
	private Transform m_Character;
	
	public override void begin() {
		Debug.Log( "started chasing" );
		
		m_Target = _context.m_Theft.transform;
		m_Character = _context.m_Character.transform;
		
		m_Agent = _context.GetComponent<UnityEngine.AI.NavMeshAgent>();
		m_Agent.updatePosition = false;
		m_Agent.updateRotation = false;
		m_Agent.SetDestination(m_Target.position);
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

		// Shoot at player
		if(canSeeTheft && _context.m_Character.m_Crystals >= 3) {
			Debug.Log("Shoot");
			// TODO: Rotate to player?
			 _context.m_Character.Action();
			 return;
		} 
		
		/**
			if player very near
				if  DistToCrystal + DistCrystalToPlayer*0.5(x) < DistToPlayer
					=> Verhindert, dass er sich umdreht
			else 
				if DistToCrystal < y 
				
		**/
		
		/*foreach( GameObject c in _context.m_CrystalManager.m_Crystals ) {
			if(_context.m_Character.m_Crystals < 3 )
			// &&c. Theft further away than next crystal?  MinTreshold) {
				// TODO: Parameter übergeben?
				_context.m_TargetedCrystal = c;
				_machine.changeState<CollectCrystal>();
			}
		}*/
	}
		
	/**
		Moves the character
	*/
	public override void update( float deltaTime ) {
		
		if(m_Agent.enabled) {
			m_Agent.SetDestination(m_Target.position);
		}
		
		Vector3 velocity = m_Agent.nextPosition - m_Character.position;

		
		_context.m_Character.Move(velocity);
	}
}
