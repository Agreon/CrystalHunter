﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using AI.FiniteStateMachine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public Notification m_TheftNotification;
	public Notification m_CMNotification;

	public Theft m_Theft;
	public CrystalMaster m_CrystalMaster;

	public Transform m_TheftStart;
	public Transform m_CrystalMasterStart;
	public Transform m_CameraStart;

	public Canvas m_Canvas;

	private FSM<GameManager> m_Machine;

	public int m_CurrentRound = 0;
	public int[] m_Rounds;
	public bool m_FromPause = false;
	
	// Temp
	public bool m_GameOver = false;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
			return;
		}

		GlobalConfig.IN_GAME = true;

		m_TheftStart = m_Theft.transform;
		m_CrystalMasterStart = m_CrystalMaster.transform;
		m_CameraStart = Camera.main.transform;

		m_Rounds = new int[2];
		m_Machine = new FSM<GameManager>( this, new MenuState() );

		m_Machine.addState( new IntroState() );
		m_Machine.addState( new CountdownState() );
		m_Machine.addState( new PlayState() );
		m_Machine.addState( new ScoreState() );
		m_Machine.addState( new PauseState() );

		Cursor.visible = false;
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
	}

	void Update() {
		// update the state machine
		m_Machine.update( Time.deltaTime );
	}

	public void ShowNotification(bool theft, string message, float time = 3f){
		if (theft) {
			m_TheftNotification.Show (message, 3);
		} else {
			m_CMNotification.Show (message, 3);
		}
	}
	
}