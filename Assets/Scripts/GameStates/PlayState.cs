using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AI.FiniteStateMachine;

public class PlayState : FSMState<GameManager>
{
	private float m_PlayTime = 0;

	private Text m_TimeText;
	private Text m_CrystalText;

	public override void begin() {
		// TODO: Enable Input


		m_TimeText = GameObject.Find("CurrentTimetext").GetComponent<Text>();
		m_TimeText.enabled = true;

		m_CrystalText = GameObject.Find ("Crystaltext").GetComponent<Text> ();
		m_CrystalText.enabled = true;
	}
		
	public void shutdown(){
		m_TimeText.enabled = false;
		m_CrystalText.enabled = false;
	}

	public override void update( float deltaTime ) {

		m_PlayTime += deltaTime;

		// TODO: ParseTime?
		m_TimeText.text = ((int)m_PlayTime).ToString();

		int crystals = _context.m_CurrentChar.GetCrystals ();
		m_CrystalText.text = "Crystals: " + crystals.ToString();
	}

	public void GameOver(){
		Debug.Log("GAMEOVER");
	}
		
}
