using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using AI.FiniteStateMachine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	private FSM<GameManager> m_Machine;

	public Character m_CurrentChar;

	public int m_CurrentRound = 0;
	public int[] m_Rounds;
	
	
	// Temp
	public bool m_GameOver = false;

	void Awake(){
		if (instance == null) {
			instance = this;
		}/* else if (instance != this) {
			Destroy (gameObject);
			return;
		}*/
		/*
		m_Rounds = new int[2];

		//m_Machine = new FSM<GameManager>( this, new PlayState() );
		m_Machine = new FSM<GameManager>( this, new IntroState() );

		m_Machine.addState( new CountdownState() );
		m_Machine.addState( new PlayState() );
		m_Machine.addState( new ScoreState() );*/
		Debug.Log ("AHAHA");
		m_Rounds = new int[2];
		m_Machine = new FSM<GameManager>( this, new IntroState() );

		m_Machine.addState( new CountdownState() );
		m_Machine.addState( new PlayState() );
		m_Machine.addState( new ScoreState() );
	}

	// Use this for initialization
	void Start () {
		// https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
		/*
		using(var reader = new BinaryReader(FileSystemWatcher.OpenRead("Highscore"))){
			m_HighScore = reader.ReadInt32();
			reader.close();
		}
		*/
	/*	m_Rounds = new int[2];
		Debug.Log ("FUU");
		//m_Machine = new FSM<GameManager>( this, new PlayState() );
		m_Machine = new FSM<GameManager>( this, new IntroState() );

		m_Machine.addState( new CountdownState() );
		m_Machine.addState( new PlayState() );
		m_Machine.addState( new ScoreState() );*/
		Debug.Log ("FSM Start");
	/*	m_Rounds = new int[2];
		m_Machine = new FSM<GameManager>( this, new IntroState() );

		m_Machine.addState( new CountdownState() );
		m_Machine.addState( new PlayState() );
		m_Machine.addState( new ScoreState() );*/
	}

	void Update() {
		// update the state machine
		m_Machine.update( Time.deltaTime );
	}
	
}
