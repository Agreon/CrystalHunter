using UnityEngine;
using System.Collections;
using AI.FiniteStateMachine;

public class IntroState : FSMState<GameManager>
{
	private GameObject m_IntroView;

	/**
	 * Get UI-Compos, enable them 
	 **/
	public override void begin() {
		m_IntroView = GameObject.Find ("IntroductionView");
		m_IntroView.gameObject.SetActive (true);
	}

	// Switches to the Countdown
	public void startGame() {

		/**
		 * Disable UI
		 * */
		m_IntroView.gameObject.SetActive (false);

		_machine.changeState<CountdownState>();
	}

	public override void update( float deltaTime ) {
		// OnKey 'startGame'
		if (Input.anyKey) {
			startGame ();
		}
	}
}
