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

		Camera camera = FindObjectOfType <Camera>();

		GlobalConfig.METAL_MODE = toggled;

		if (toggled) {
			camera.backgroundColor = new Color (57/255, 0, 0, 0);
		} else {
			camera.backgroundColor = new Color (0, 0, 50/255, 0);
		}
	}

	public void ExitGame(){
		
	}
}
