using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
	private Text m_TextHeader;
	private Text m_MainText;

	private Animator m_Animator;

	private float m_Counter;
	private float m_ShowTime;

	void Awake(){
		m_TextHeader = transform.Find ("HeaderText").GetComponent<Text>();
		m_MainText = transform.Find ("MainText").GetComponent<Text>();
		m_Animator = GetComponent<Animator> ();
	}

	void Update(){
		m_Counter += Time.deltaTime;

		if (m_Counter >= m_ShowTime) {
			m_Animator.SetBool ("FlyIn", false);
		}

	}

	public void Show(string header, string text, float time){
		m_TextHeader.text = header;
		m_MainText.text = text;
		m_Counter = 0;
		m_ShowTime = time;
		m_Animator.SetBool ("FlyIn", true);
	}
}


