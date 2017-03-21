using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AI.FiniteStateMachine;

public class CountdownState : FSMState<GameManager>
{
	private float m_CountdownDuration = 3;
	private float m_Counter;

	private Text m_CountdownText;

	public override void begin() {
		m_CountdownText = GameObject.Find("CountdownText").GetComponent<Text>();
		m_CountdownText.enabled = true;

		Camera.main.GetComponent<Animator> ().Play ("CameraFly");

		_context.m_CrystalMaster.GetComponent<Animator> ().Play ("Growl");
		AudioManager.instance.PlaySound ("monster_scream");

		m_Counter = 0;

		GlobalConfig.IN_GAME = true;
	}
		
	public void shutdown(){
		m_CountdownText.enabled = false;
	}

	public override void update( float deltaTime ) {
		m_Counter += deltaTime;

		// Set UI
		if ((int)(m_CountdownDuration - m_Counter) == 0) {
			m_CountdownText.text = "Start!";
		} else {
			m_CountdownText.text = ((int)(m_CountdownDuration-m_Counter)).ToString();
		}

		if (m_Counter > m_CountdownDuration) {
			shutdown ();
			_context.m_PlayTime = 0;
			_machine.changeState<PlayState> ();
		}
	}
}
