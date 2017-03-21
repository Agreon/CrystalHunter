using UnityEngine;
using System.Collections.Generic;
using AI.FiniteStateMachine;

[RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof (Character))]
public class AIInput : MonoBehaviour {

	public Theft m_Theft;

	[HideInInspector] public CrystalMaster m_Character;
	[HideInInspector] public GameObject m_TargetedCrystal;

	private FSM<AIInput> m_Machine;

	void Start() {
		m_Character = GetComponent<CrystalMaster>();

		m_Machine = new FSM<AIInput>( this, new ChaseTheft() );

		m_Machine.addState( new CollectCrystal() );
	}

	void Update() {
		m_Machine.update( Time.deltaTime );
	}
}
