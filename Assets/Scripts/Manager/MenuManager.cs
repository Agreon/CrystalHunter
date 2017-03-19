using System.Collections;
using System.Collections.Generic;
//using System.Collections.Generic.Dictionary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	Dictionary<string, GameObject> m_Menus;

	// Use this for initialization
	void Start () {
		Cursor.visible = true;

		if (GlobalConfig.METAL_MODE) {
			GameObject.Find ("MetalModeToggle").GetComponent<Toggle> ().isOn = true;
		}

		GlobalConfig.IN_GAME = false;

		Pillar[] pillars = FindObjectsOfType<Pillar> ();
		foreach(Pillar p in pillars){
			p.UpdateMaterial(); 
		}

		m_Menus = new Dictionary<string, GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartSingle(){

		AudioManager.instance.PlaySound("menu_click");

		GlobalConfig.MULTIPLAYER = false;
		SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
	}

	public void StartMulti(){

		AudioManager.instance.PlaySound("menu_click");

		GlobalConfig.MULTIPLAYER = true;
		SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
	}

	public void toggleMetalMode(bool toggled){

		AudioManager.instance.PlaySound("menu_click");


		GlobalConfig.METAL_MODE = toggled;
		if (toggled) {
			AudioManager.instance.Play (true, "Sylosis");
		} else {
			AudioManager.instance.Play (false, null);
		}

		Pillar[] pillars = FindObjectsOfType<Pillar> ();
		foreach(Pillar p in pillars){
			p.UpdateMaterial(); 
		}
	}

	public void ExitGame(){

		AudioManager.instance.PlaySound("menu_click");

		Application.Quit();
	}
}
