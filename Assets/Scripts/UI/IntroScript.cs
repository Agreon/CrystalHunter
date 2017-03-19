using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

	private float m_Counter;
	private float m_ShowTime = 3f;

	// Use this for initialization
	void Start () {
		m_Counter = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		m_Counter += Time.deltaTime;

		if (m_Counter > m_ShowTime) {
			SceneManager.LoadScene ("GameScene");
		}

	}
}
