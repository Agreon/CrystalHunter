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

		CrystalManager.instance.disable ();
		_context.m_Theft.disableInput ();
		_context.m_CrystalMaster.disableInput ();
	}
		
	public override void update( float deltaTime ) {

		// OnKey 'startGame'
		if (Input.GetKeyUp(KeyCode.Space)) {

			// Disable UI
			m_PauseText.enabled = false;
			m_PauseText2.enabled = false;

			_context.m_FromPause = true;

			_machine.changeState<PlayState> ();
		}
		if(Input.GetKeyUp(KeyCode.Return)) {

			m_PauseText.enabled = false;
			m_PauseText2.enabled = false;

			_context.m_FromPause = false;

			_machine.changeState<MenuState> ();
		}
	}
}
