using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
	private Text m_TextHeader;
	private Text m_MainText;

	private float m_Counter;
	private float m_ShowTime;

	void Start(){
		m_TextHeader = transform.Find ("HeaderText").GetComponent<Text>();
		m_MainText = transform.Find ("MainText").GetComponent<Text>();
	}

	void Update(){
		m_Counter += Time.deltaTime;

		if (m_Counter >= m_ShowTime) {
			gameObject.SetActive (false);
		}

	}

	// TODO: Think of fade in
	/**
	 * Entweder FadeIn
	 * Oder leichter => Einfliegen
	 * */

	public void Show(string header, string text, float time){
		m_TextHeader.text = header;
		m_MainText.text = text;
		m_Counter = 0;
		m_ShowTime = time;
		gameObject.SetActive (true);
	}
}


