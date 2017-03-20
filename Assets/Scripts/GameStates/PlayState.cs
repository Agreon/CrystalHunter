using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AI.FiniteStateMachine;

public class PlayState : FSMState<GameManager>
{	
	private Text m_TimeText;
	private Text m_CrystalText;
	
	private GameObject[] m_TheftCrystalLoads;
	private GameObject[] m_CMCrystalLoads;

	
	public override void begin() {
		_context.m_GameOver = false;

		CrystalManager.instance.enable();

		_context.m_Theft.GetComponent<Animator> ().SetBool ("Crouch", false);

		Camera.main.GetComponent<CameraPositioner> ().enabled = true;

		AudioManager.instance.SetBreathing (true);
	
		/**
		 * Enable Input
		 * */
		if(GlobalConfig.MULTIPLAYER) {
			// First Round
			if(_context.m_CurrentRound == 0){
				_context.m_Theft.GetComponent<PlayerInput>().enabled = true;
				_context.m_Theft.GetComponent<PlayerInput>()._secondPlayer = false;
				
				_context.m_CrystalMaster.GetComponent<PlayerInput>().enabled = true;
				_context.m_CrystalMaster.GetComponent<PlayerInput>()._secondPlayer = true;
			}
			// Second Round
			else {
				_context.m_Theft.GetComponent<PlayerInput>().enabled = true;
				_context.m_Theft.GetComponent<PlayerInput>()._secondPlayer = true;
				
				_context.m_CrystalMaster.GetComponent<PlayerInput>().enabled = true;
				_context.m_CrystalMaster.GetComponent<PlayerInput>()._secondPlayer = false;
			}
		} else {
			// Set PlayerIn
			_context.m_Theft.GetComponent<PlayerInput>().enabled = true;
			_context.m_Theft.GetComponent<PlayerInput>()._secondPlayer = false;
			
			// Enable KIIn
			_context.m_CrystalMaster.GetComponent<AIInput>().enabled = true;
			_context.m_CrystalMaster.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
		}
			

		// Enable UI
		m_TimeText = GameObject.Find("CurrentTimetext").GetComponent<Text>();
		m_TimeText.enabled = true;

		m_CrystalText = GameObject.Find ("Crystaltext").GetComponent<Text> ();
		m_CrystalText.enabled = true;

		m_TheftCrystalLoads = new GameObject[3];
		var theftCrystalContainer = GameObject.Find ("TheftCrystalLoads");
		for (int i = 0; i < theftCrystalContainer.transform.childCount; i++) {
			m_TheftCrystalLoads [i] = theftCrystalContainer.transform.GetChild(i).gameObject;
		}

		m_CMCrystalLoads = new GameObject[3];
		var CMCrystalContainer = GameObject.Find ("CMCrystalLoads");
		for (int i = 0; i < CMCrystalContainer.transform.childCount; i++) {
			m_CMCrystalLoads [i] = CMCrystalContainer.transform.GetChild(i).gameObject;
		}
			
	}

	void shutdown(){
		m_TimeText.enabled = false;
		m_CrystalText.enabled = false;

		_context.m_Theft.DisableInput ();
		_context.m_CrystalMaster.DisableInput ();

		for (int i = 0; i < 3; i++) {
			m_TheftCrystalLoads[i].GetComponent<Renderer>().enabled = false;
		}
		for (int i = 0; i < 3; i++) {
			m_CMCrystalLoads[i].GetComponent<Renderer>().enabled = false;
		}

		CrystalManager.instance.disable ();
		AudioManager.instance.SetBreathing (false);
	}
		

	public override void update( float deltaTime ) {

		_context.m_PlayTime += deltaTime;

		m_TimeText.text = parseTime(_context.m_PlayTime);

		int crystals = _context.m_Theft.GetCrystals ();
		m_CrystalText.text = "Crystals: " + crystals.ToString();


		if (Input.GetKey (KeyCode.Escape)) {
			shutdown ();
			_machine.changeState<PauseState> ();
		}

		// Show CrystalLoads
		int crystalLoads = _context.m_Theft.GetCrystalLoads();
		
		for(int i = 0; i < 3; i++){
			if(i >= crystalLoads){
				m_TheftCrystalLoads[i].GetComponent<Renderer>().enabled = false;
			}
			else {
				m_TheftCrystalLoads[i].GetComponent<Renderer>().enabled = true;
			}

			if (crystalLoads == 3) {
				//m_CrystalLoads [i].GetComponent<Animator> ().enabled = true;
				//m_CrystalLoads [i].GetComponent<Animator> ().Play ("CrystalLight");
				m_TheftCrystalLoads [i].GetComponent<Animator> ().SetBool ("StartLight",false);
			} else if (crystalLoads == 0) {
				//m_CrystalLoads [i].GetComponent<Animator> ().enabled = false;
			}
		}

		// Show CM CrystalLoads
		if (GlobalConfig.MULTIPLAYER) {

			//int CMCrystalLoads = _context.m_CrystalMaster.GetCrystalLoads();
			int CMCrystalLoads = (int)_context.m_CrystalMaster.GetReloadCounter();

			for(int i = 0; i < 3; i++){
				if(i >= CMCrystalLoads){
					m_CMCrystalLoads[i].GetComponent<Renderer>().enabled = false;
				}
				else {
					m_CMCrystalLoads[i].GetComponent<Renderer>().enabled = true;
				}

				if (CMCrystalLoads == 3) {
					//m_CrystalLoads [i].GetComponent<Animator> ().enabled = true;
					//m_CrystalLoads [i].GetComponent<Animator> ().Play ("CrystalLight");
					m_CMCrystalLoads [i].GetComponent<Animator> ().SetBool ("StartLight",false);
				} else if (CMCrystalLoads == 0) {
					//m_CrystalLoads [i].GetComponent<Animator> ().enabled = false;
				}
			}
		}

		// Not-so-beautiful workaround, bc unityevent did not work
		if(_context.m_GameOver){
			GameOver();
		}
	}

	/**
		Parses float-time to minutes 
	**/
	string parseTime(float time){
		string retTime = "";

		int minutes = (int)time/60;
		int seconds = (int)time%60;

		string secondsStr = seconds.ToString();
		if (secondsStr.Length < 2) {
			secondsStr = "0" + secondsStr;
		}

		retTime = minutes.ToString() + ":" + secondsStr; 

		return retTime;
	}

	void GameOver(){

		// Save Points
		_context.m_Rounds[_context.m_CurrentRound] = _context.m_Theft.GetCrystals() + ((int)_context.m_PlayTime/2); 

		shutdown();

		_machine.changeState<ScoreState> ();
	}
		
}
