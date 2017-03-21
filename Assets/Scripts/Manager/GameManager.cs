using System.Collections;
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

	public Transform m_CameraStart;
	public Transform m_MenuCrystalStart;

	public Canvas m_Canvas;

	private FSM<GameManager> m_Machine;

	public int m_CurrentRound = 0;
	public int[] m_Rounds;
	public float m_PlayTime;

	// Temp
	public bool m_GameOver = false;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
			return;
		}
			
		m_Rounds = new int[2];
		m_Machine = new FSM<GameManager>( this, new MenuState() );

		m_Machine.addState( new IntroState() );
		m_Machine.addState( new CountdownState() );
		m_Machine.addState( new PlayState() );
		m_Machine.addState( new ScoreState() );
		m_Machine.addState( new PauseState() );
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


	public void ResetObjects(){
		// Clear temp objects
		var ObjectContainer = GameObject.Find ("ObjectContainer");
		foreach (Transform child in ObjectContainer.transform) {
			GameObject.Destroy(child.gameObject);
		}

		//AudioManager.instance.ClearQueue ();

		CrystalManager.instance.clear ();

		// Reset Chars
		m_Theft.Reset();
		m_CrystalMaster.Reset ();

		m_Theft.GetComponent<Animator> ().SetBool("Crouch",true);
		m_Theft.GetComponent<Animator> ().Play ("GrabbingCrystal");
		m_CrystalMaster.GetComponent<Animator> ().SetFloat ("Forward", 0);
		m_CrystalMaster.GetComponent<Animator> ().SetFloat ("Turn", 0);
		m_CrystalMaster.GetComponent<Animator> ().Play ("Grounded");

		// Reset Camera
		Camera.main.GetComponent<CameraPositioner> ().enabled = false;
		Camera.main.GetComponent<CameraController> ().Reset ();
	
	}

	public void ShowNotification(bool theft, string message, float time = 3f){
		if (theft) {
			m_TheftNotification.Show (message, 3);
		} else {
			m_CMNotification.Show (message, 3);
		}
	}
	
}
