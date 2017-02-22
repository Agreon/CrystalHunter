using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using AI.FiniteStateMachine;

public class ScoreState : FSMState<GameManager>
{
	private Text m_ScoreText;

	public override void begin() {
		m_ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		
		if(GlobalConfig.MULTIPLAYER){
			// TODO: Show Both Scores and who won
		
		} else {
			// TODO: Vlt. mit Zeit adden
			m_ScoreText.text = "Score: " + _context.m_Rounds[0].ToString();
		}
	}
		
	public void shutdown(){
		m_ScoreText.gameObject.SetActive(false);
	}

	public override void update( float deltaTime ) {
		
		if (Input.GetKeyUp (KeyCode.Space)) {
			
			shutdown();
			
			if(GlobalConfig.MULTIPLAYER && _context.m_CurrentRound == 0){
				// Return to PlayState
				_context.m_CurrentRound = 1;
				_machine.changeState<PlayState> ();		
			} else {
				// Change to MenuScene
				SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
			}
		}
		
	}
}
