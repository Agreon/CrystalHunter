using UnityEngine;
using System.Collections;
using AI.FiniteStateMachine;

public class CollectCrystal : FSMState<AIInput>
{
	private UnityEngine.AI.NavMeshAgent m_Agent;
	
	private Transform m_Target;
	private Transform m_Character;
	
	public override void begin() {
	
		Debug.Log("started collecting" );
		
		m_Target = _context.m_TargetedCrystal.transform;
		m_Character = _context.m_Character.transform;
		
		m_Agent = _context.GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
		
	/**
	* Check distances
	**/
	public override void reason() {

		// If Crystal collected, chase theft again
		if(_context.m_TargetedCrystal == null) {
			_machine.changeState<ChaseTheft>();
			return;
		}

		float distanceToPlayer = Vector3.Distance (_context.m_Theft.transform.position, m_Character.position);
		float distanceToCrystal = Vector3.Distance ( m_Target.position, m_Character.position);

		// If distance to player < targetedCrystal
		if (distanceToPlayer < distanceToCrystal) {
			_machine.changeState<ChaseTheft> ();
		}
	}
		
	/**
		Moves to destination
	*/
	public override void update( float deltaTime ) {

		if(m_Agent.enabled) {
			m_Agent.SetDestination(m_Target.position);
		}

		Vector3 velocity = m_Agent.nextPosition - m_Character.position;
		_context.m_Character.Move(velocity);
	}
}
