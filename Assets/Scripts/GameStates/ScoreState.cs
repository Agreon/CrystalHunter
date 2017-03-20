using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using AI.FiniteStateMachine;

public class ScoreState : FSMState<GameManager>
{
	private Text m_ScoreText;
	private Text m_Player1Score;
	private Text m_Player2Score;
	private Text m_WinnerText;
	private Text m_ContinueText;


	public override void begin() {
		// TODO:	_context.m_Canvas.transform.Find(
		m_ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		m_Player1Score = GameObject.Find("Player1Score").GetComponent<Text>();
		m_Player2Score = GameObject.Find("Player2Score").GetComponent<Text>();
		m_WinnerText = GameObject.Find("WinnerText").GetComponent<Text>();
		m_ContinueText = GameObject.Find("ContinueText").GetComponent<Text>();
		
		if(GlobalConfig.MULTIPLAYER){

			m_ContinueText.enabled = true;

			// Show single score
			if (_context.m_CurrentRound == 0) {

				m_Player1Score.enabled = true;
				m_Player1Score.text = "Player 1: " + _context.m_Rounds [0].ToString ();
				m_ContinueText.text = "Press a key to continue!";

				_context.m_GameOver = false;
			// Show both scores
			} else {

				m_Player1Score.enabled = true;
				m_Player2Score.enabled = true;
				m_WinnerText.enabled = true;

				m_Player1Score.text = "Player 1: " + _context.m_Rounds [0].ToString ();
				m_Player2Score.text = "Player 2: " + _context.m_Rounds [1].ToString ();
				m_ContinueText.text = "Press a key to get to the menu!";

				if (_context.m_Rounds [0] > _context.m_Rounds [1]) {
					m_WinnerText.text = "Player 1 Wins!"; 
				} else {
					m_WinnerText.text = "Player 2 Wins!";
				}
			}
				
		} else {
			m_ScoreText.enabled = true;
			m_ContinueText.enabled = true;

			m_ScoreText.text = "Your score is: " + _context.m_Rounds[0].ToString() + "!";
			m_ContinueText.text = "Press a key to get to the menu!";
		}
	}
		
	public void shutdown() {
		// Diable UI
		m_ScoreText.enabled = false;
		m_ContinueText.enabled = false;
		m_Player1Score.enabled = false;
		m_Player2Score.enabled = false;
		m_WinnerText.enabled = false;

		// Clear temp objects
		var ObjectContainer = GameObject.Find ("ObjectContainer");
		foreach (Transform child in ObjectContainer.transform) {
			GameObject.Destroy(child.gameObject);
		}

		CrystalManager.instance.clear ();

		// Reset Chars
		_context.m_Theft.Reset();
		_context.m_CrystalMaster.Reset ();
	
		_context.m_Theft.GetComponent<Animator> ().Play ("Crouch");
		_context.m_CrystalMaster.GetComponent<Animator> ().SetFloat ("Forward", 0);
		_context.m_CrystalMaster.GetComponent<Animator> ().SetFloat ("Turn", 0);
		_context.m_CrystalMaster.GetComponent<Animator> ().Play ("Grounded");

		// Reset Camera
		Camera.main.GetComponent<CameraPositioner> ().enabled = false;
	}

	public override void update( float deltaTime ) {
		
		if (Input.GetKeyUp (KeyCode.Space)) {
			
			shutdown();
			
			if(GlobalConfig.MULTIPLAYER && _context.m_CurrentRound == 0){
				// Return to PlayState
				_context.m_CurrentRound = 1;
				_machine.changeState<CountdownState> ();		
			} else {
				// Change to MenuScene
				_machine.changeState<MenuState>();
			}
		}
		
	}
}
