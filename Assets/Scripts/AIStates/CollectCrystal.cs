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
		TODO: Is Player near ? Is Crystal Collected?
		=> Variables in AIInput? (MinDistToCrystal/Player)
	
	**/
	public override void reason() {
		if(_context.m_TargetedCrystal == null) {
			_machine.changeState<ChaseTheft>();
		}
		
		//TODO: Distance to player
		
	}
		
	/**
		TODO: Get move-direction of agent
	*/
	public override void update( float deltaTime ) {
		if(m_Agent.enabled) {
			m_Agent.SetDestination(m_Target.position);
		}

		Vector3 velocity = m_Agent.nextPosition - m_Character.position;

		_context.m_Character.Move(velocity);
	}
}
