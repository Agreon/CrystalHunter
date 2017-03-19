using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AI.FiniteStateMachine;

public class MenuState : FSMState<GameManager>
{
	private GameObject m_SinglePlayerBtn;
	private GameObject m_MultiPlayerBtn;
	private GameObject m_ExitBtn;
	private GameObject m_MetalModeToggle;

	/**
	 * Get UI-Compos, enable them 
	 **/
	public override void begin() {

		Cursor.visible = true;

		if (GlobalConfig.METAL_MODE) {
			GameObject.Find ("MetalModeToggle").GetComponent<Toggle> ().isOn = true;
		}

		GlobalConfig.IN_GAME = false;

		Pillar[] pillars = GameObject.FindObjectsOfType<Pillar> ();
		foreach(Pillar p in pillars){
			p.UpdateMaterial(); 
		}
			
		m_SinglePlayerBtn = GameObject.Find ("SingleplayerButton");
		m_SinglePlayerBtn.GetComponent<Button>().onClick.AddListener (() => {
			StartSingle();
		});
		setButton (m_SinglePlayerBtn, true);

		m_MultiPlayerBtn = GameObject.Find ("MultiplayerButton");
		m_MultiPlayerBtn.GetComponent<Button>().onClick.AddListener (() => {
			StartMulti();
		});
		setButton (m_MultiPlayerBtn, true);

		m_ExitBtn = GameObject.Find ("ExitButton");
		m_ExitBtn.GetComponent<Button>().onClick.AddListener (() => {
			ExitGame();
		});
		setButton (m_ExitBtn, true);

		m_MetalModeToggle = GameObject.Find ("MetalModeToggle");
		m_MetalModeToggle.GetComponent<Toggle>().onValueChanged.AddListener ((toggled) => {
			ToggleMetalMode(toggled);
		});

		m_MetalModeToggle.GetComponentInChildren<Image> ().enabled = true;
		m_MetalModeToggle.GetComponentInChildren<Text> ().enabled = true;


	}

	public void setButton(GameObject btn, bool enabled){
		btn.GetComponent<Image> ().enabled = enabled;
		btn.GetComponentInChildren<Text> ().enabled = enabled;
		btn.GetComponentInChildren<Button> ().enabled = enabled;
	}

	public void shutdown() {
		setButton (m_SinglePlayerBtn, false);
		setButton (m_MultiPlayerBtn, false);
		setButton (m_ExitBtn, false);

		m_MetalModeToggle.GetComponentInChildren<Image> ().enabled = false;
		m_MetalModeToggle.GetComponentInChildren<Text> ().enabled = false;


		Cursor.visible = false;
	}

	public void StartSingle(){
		
		AudioManager.instance.PlaySound("menu_click");
		shutdown ();

		GlobalConfig.MULTIPLAYER = false;
		_machine.changeState<IntroState> ();
	}

	public void StartMulti(){

		AudioManager.instance.PlaySound("menu_click");
		shutdown ();

		GlobalConfig.MULTIPLAYER = true;
		_machine.changeState<IntroState> ();
	}

	public void ToggleMetalMode(bool toggled){

		AudioManager.instance.PlaySound("menu_click");

		GlobalConfig.METAL_MODE = toggled;
		if (toggled) {
			AudioManager.instance.Play (true, "Sylosis");
		} else {
			AudioManager.instance.Play (false, null);
		}

		// TODO: Find sollte wo anders hin
		Pillar[] pillars = GameObject.FindObjectsOfType<Pillar> ();
		foreach(Pillar p in pillars){
			p.UpdateMaterial(); 
		}

		//GameObject.Find ("MetalModeToggle").GetComponent<Toggle> ().isOn = toggled;
	}

	public void ExitGame(){

		AudioManager.instance.PlaySound("menu_click");

		Application.Quit();
	}
		
	public override void update( float deltaTime ) {
	}
}
