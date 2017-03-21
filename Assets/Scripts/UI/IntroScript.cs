using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {

	private float m_Counter;
	public float m_ShowTime = 3.5f;

	public Text m_LeftText;
	public Text m_RightText;

	// Use this for initialization
	void Start () {
		m_Counter = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		m_Counter += Time.deltaTime;

		if (m_Counter >= 0.3) {
			m_LeftText.GetComponent<Text> ().enabled = true;
			m_LeftText.GetComponent<Animator> ().enabled = true;
			m_RightText.GetComponent<Text> ().enabled = true;
			m_RightText.GetComponent<Animator> ().enabled = true;
		}

		if (m_Counter > m_ShowTime) {
			SceneManager.LoadScene ("GameScene");
		}

	}
}
