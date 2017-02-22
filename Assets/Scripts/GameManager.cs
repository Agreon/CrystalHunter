using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using AI.FiniteStateMachine;

public class GameManager : MonoBehaviour {
	
	private FSM<GameManager> m_Machine;

	public Character m_CurrentChar;

	// Use this for initialization
	void Start () {
		// https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
		/*
		using(var reader = new BinaryReader(FileSystemWatcher.OpenRead("Highscore"))){
			m_HighScore = reader.ReadInt32();
			reader.close();
		}
		*/

		m_Machine = new FSM<GameManager>( this, new IntroState() );

		m_Machine.addState( new CountdownState() );
		m_Machine.addState( new PlayState() );
	}

	void Update() {
		// update the state machine
		m_Machine.update( Time.deltaTime );
	}
}
