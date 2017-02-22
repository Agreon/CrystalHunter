using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AI.FiniteStateMachine;

public class PlayState : FSMState<GameManager>
{
	private Theft m_Theft;
	private CrystalMaster m_CrystalMaster;
	
	private float m_PlayTime = 0;

	private Text m_TimeText;
	private Text m_CrystalText;
	
	private GameObject[] m_CrystalLoads;
	
	public override void begin() {
		
		m_Theft = FindObjectOfType<Theft> ();
		m_CrystalMaster = FindObjectOfType<CrystalMaster> ();		

		// Enable Input
		if(GlobalConfig.MULTIPLAYER) {
			// First Round
			if(_context.m_CurrentRound == 0){
				m_Theft.GetComponent<PlayerInput>().enabled = true;
				m_Theft.GetComponent<PlayerInput>()._secondPlayer = false;
				
				m_CrystalMaster.GetComponent<PlayerInput>().enabled = true;
				m_CrystalMaster.GetComponent<PlayerInput>()._secondPlayer = true;
			}
			// Second Round
			else {
				m_Theft.GetComponent<PlayerInput>().enabled = true;
				m_Theft.GetComponent<PlayerInput>()._secondPlayer = true;
				
				m_CrystalMaster.GetComponent<PlayerInput>().enabled = true;
				m_CrystalMaster.GetComponent<PlayerInput>()._secondPlayer = false;
			}
		} else {
			// Set PlayerIn
			m_Theft.GetComponent<PlayerInput>().enabled = true;
			m_Theft.GetComponent<PlayerInput>()._secondPlayer = false;
			
			// Enable KIIn
			m_CrystalMaster.GetComponent<AIInput>().enabled = true;
		}

		// Enable UI

		m_TimeText = GameObject.Find("CurrentTimetext").GetComponent<Text>();
		m_TimeText.enabled = true;

		m_CrystalText = GameObject.Find ("Crystaltext").GetComponent<Text> ();
		m_CrystalText.enabled = true;

		m_CrystalLoads = new GameObject[3];
		
		m_CrystalLoads[0] = GameObject.Find ("CrystalLoad1");
		m_CrystalLoads[1] = GameObject.Find ("CrystalLoad2");
		m_CrystalLoads[2] = GameObject.Find ("CrystalLoad3");
	}
		
	public void shutdown(){
		m_TimeText.enabled = false;
		m_CrystalText.enabled = false;

		m_Theft.GetComponent<PlayerInput>().enabled = false;
		m_CrystalMaster.GetComponent<PlayerInput>().enabled = false;
		m_CrystalMaster.GetComponent<AIInput>().enabled = false;
	}
	
	/**
		Parses float-time to Minutes 
	**/
	public string parseTime(float time){
		string retTime = "";
		
		int minutes = (int)time/60;
		int seconds = (int)time%60;
		
		retTime = minutes.ToString() + ":" + seconds.ToString(); 
		
		return retTime;
	}

	public override void update( float deltaTime ) {

		m_PlayTime += deltaTime;

		m_TimeText.text = parseTime(m_PlayTime);

		int crystals = m_Theft.GetCrystals ();
		m_CrystalText.text = "Crystals: " + crystals.ToString();
		
		// Show CrystalLoads
		int crystalLoads = m_Theft.GetCrystalLoads();
		
		for(int i = 0; i < 3; i++){
			if(i >= crystalLoads){
				m_CrystalLoads[i].GetComponent<Renderer>().enabled = false;
			}
			else {
				m_CrystalLoads[i].GetComponent<Renderer>().enabled = true;
			}
		}
		
		if(_context.m_GameOver){
			Debug.Log(_context.m_CurrentRound);
			GameOver();
		}
	}

	public void GameOver(){
		Debug.Log("GAMEOVER");
		
		_context.m_Rounds[_context.m_CurrentRound] = m_Theft.GetCrystals();
		
		shutdown();
	
		_machine.changeState<ScoreState> ();
	}
		
}
