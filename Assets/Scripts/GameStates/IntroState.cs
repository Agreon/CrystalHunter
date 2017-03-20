using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AI.FiniteStateMachine;

public class IntroState : FSMState<GameManager>
{
	private Text m_ConfirmText;

	/**
	 * Get UI-Compos, enable them 
	 **/
	public override void begin() {
		GlobalConfig.IN_GAME = true;

		m_ConfirmText = GameObject.Find ("ConfirmText").GetComponent<Text> ();
		m_ConfirmText.enabled = true;
	}

	// Switches to the Countdown
	public void startGame() {

		Debug.Log ("StartGame");

		// Disable UI
		m_ConfirmText.enabled = false;

		_machine.changeState<CountdownState>();
	}

	public override void update( float deltaTime ) {
		// OnKey 'startGame'
		if (Input.anyKey) {
			Debug.Log ("AnyKey");
			startGame ();
		}
	}
}
