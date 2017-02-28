using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = true;
		if (GlobalConfig.METAL_MODE) {
			GameObject.Find ("MetalModeToggle").GetComponent<Toggle> ().isOn = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartSingle(){
		GlobalConfig.MULTIPLAYER = false;
		SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
	}

	public void StartMulti(){
		GlobalConfig.MULTIPLAYER = true;
		SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
	}

	public void toggleMetalMode(bool toggled){
		GlobalConfig.METAL_MODE = toggled;
		if (toggled) {
			AudioManager.instance.Play (true, "Sylosis");
		} else {
			AudioManager.instance.Play (false, null);
		}
	}

	public void ExitGame(){
		Application.Quit();
	}
}
