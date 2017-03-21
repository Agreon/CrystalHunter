using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using AI.FiniteStateMachine;

public class PauseState : FSMState<GameManager>
{
	private Text m_PauseText;
	private Text m_PauseText2;


	// Get UI-Compos, enable them 
	public override void begin() {
		m_PauseText = GameObject.Find ("PauseText").GetComponent<Text> ();
		m_PauseText.enabled = true;
		m_PauseText2 = GameObject.Find ("PauseText2").GetComponent<Text> ();
		m_PauseText2.enabled = true;

		GlobalConfig.PAUSED = true;
	}
		
	public void shutdown(){
		// Disable UI
		m_PauseText.enabled = false;
		m_PauseText2.enabled = false;

		GlobalConfig.PAUSED = false;
	}

	public override void update( float deltaTime ) {

		// OnKey 'startGame'
		if (Input.GetKeyUp(KeyCode.Space)) {

			shutdown ();
			_machine.changeState<PlayState> ();
		}

		// On Menu
		if(Input.GetKeyUp(KeyCode.Return)) {

			shutdown ();
			_context.ResetObjects ();
			_machine.changeState<MenuState> ();
		}
	}
}
