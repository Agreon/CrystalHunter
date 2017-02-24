using UnityEngine;
using System.Collections.Generic;
using AI.FiniteStateMachine;

[RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof (Character))]
public class AIInput : MonoBehaviour {
	
	// TODO: Dont serialize
	public Character m_Character;
	public Character m_Theft;

	public GameObject m_TargetedCrystal;

	private FSM<AIInput> m_Machine;

	void Start() {
		m_Character = GetComponent<Character>();

		m_Machine = new FSM<AIInput>( this, new ChaseTheft() );

		m_Machine.addState( new CollectCrystal() );
	}

	void Update() {
		m_Machine.update( Time.deltaTime );
	}
}
