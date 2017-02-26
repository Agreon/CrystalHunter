using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartSingle(){
		SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
	}

	public void StartMulti(){
		GlobalConfig.MULTIPLAYER = true;
		SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
	}

	public void toggleMetalMode(bool toggled){
		GlobalConfig.METAL_MODE = toggled;
		if (toggled) {
			AudioManager.instance.Play (false, "Sylosis");
		} else {
			AudioManager.instance.Play (true, null);
		}
	}

	public void ExitGame(){
		Application.Quit();
	}
}
