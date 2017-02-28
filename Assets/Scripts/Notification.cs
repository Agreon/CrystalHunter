using System;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
	private Text m_Text;

	private Animator m_Animator;

	private float m_Counter;
	private float m_ShowTime;

	void Awake(){
		m_Text = transform.Find ("Text").GetComponent<Text>();
		m_Animator = GetComponent<Animator> ();

	}

	void Update(){
		m_Counter += Time.deltaTime;

		if (m_Counter >= m_ShowTime) {
			m_Animator.SetBool ("FlyIn", false);
		}
	}
		
	public void Show(string text, float time){
		m_Text.text = text;
		m_Counter = 0;
		m_ShowTime = time;
		m_Animator.SetBool ("FlyIn", true);
	}
}


