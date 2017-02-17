using UnityEngine;
using System.Collections.Generic;
using AI.FiniteStateMachine;

[RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof (Character))]
public class AIInput : MonoBehaviour {
	
	// TODO: Dont serialize
	public Character m_Character;
	public Character m_Theft;
	public CrystalManager m_CrystalManager;
	
	public GameObject m_TargetedCrystal;

	private FSM<AIInput> m_Machine;

	void Start() {
		m_Character = GetComponent<Character>();

		m_CrystalManager = Object.FindObjectOfType<CrystalManager>();

		// the initial state has to be passed to the constructor
		m_Machine = new FSM<AIInput>( this, new ChaseTheft() );

		// we can now add any additional states
		m_Machine.addState( new CollectCrystal() );
	}

	void Update() {
		// update the state machine
		m_Machine.update( Time.deltaTime );
	}
}
