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
	private List<Pillar> m_Pillars;
	/**
	 * Get UI-Compos, enable them 
	 **/
	public override void begin() {

		Cursor.visible = true;

		if (GlobalConfig.METAL_MODE) {
			GameObject.Find ("MetalModeToggle").GetComponent<Toggle> ().isOn = true;
		}

		GlobalConfig.IN_GAME = false;

		// Spawn special MenuCrystal with better Lighting and smaller collision-box
		GameObject menuCrystal = GameObject.Find ("MenuCrystalPickup");
		GameObject.Instantiate (menuCrystal, _context.m_MenuCrystalStart.position, Quaternion.LookRotation (new Vector3 (1, 0, 0)));

		m_Pillars = GameObject.FindObjectsOfType<Pillar> ();
		foreach(Pillar p in m_Pillars){
			p.LoadMaterial ();
			p.UpdateMaterial(); 
		}

		_context.m_Theft.GetComponent<Animator> ().Play ("GrabbingCrystal");

		enableUI ();
	}

	public void enableUI(){
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

		_context.m_CrystalMaster.m_SpellReloadTime = 3;

		GlobalConfig.MULTIPLAYER = false;
		_machine.changeState<IntroState> ();
	}

	public void StartMulti(){

		AudioManager.instance.PlaySound("menu_click");
		shutdown ();

		// Make it harder for human player
		_context.m_CrystalMaster.m_SpellReloadTime = 6;

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


		// TODO: Find should be done on init
		foreach(Pillar p in m_Pillars){
			p.UpdateMaterial(); 
		}
	}

	public void ExitGame(){
		AudioManager.instance.PlaySound("menu_click");

		Application.Quit();
	}
		
	public override void update( float deltaTime ) {
	}
}
