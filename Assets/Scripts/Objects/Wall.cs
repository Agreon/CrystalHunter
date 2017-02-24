using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	private float m_Counter = 0;
	private bool m_Beat = false;

	// Use this for initialization
	void Start () {
		AudioManager.instance.m_AudioProcessor.onBeat.AddListener (OnBeat);
	}
	
	// Update is called once per frame
	void Update () {


		// TODO: Vlt. eher mit Sinus

		if(m_Beat && m_Counter < 1){
			m_Counter += Time.deltaTime * 8;
		}

		if (m_Counter > 1) {
			m_Beat = false;
			m_Counter -= Time.deltaTime * 8;
		}
			
		if(m_Beat == false && m_Counter < 0){
			m_Counter = 0;
		}
	
		// TODO: Add counter to intensity of light

	}

	public void OnBeat(){
		m_Counter = 0.001f;
		m_Beat = true;
	}
}
