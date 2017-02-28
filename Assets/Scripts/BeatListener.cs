using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatListener : MonoBehaviour {

	private float m_Counter = 0;
	private bool m_Beat = false;
	private Light m_Light;
	private float m_BaseIntensity;

	public Color m_TechnoColor = new Color(78f/255,126f/255,245f/255);
	public Color m_MetalColor = new Color(210f/255,17f/255,17f/255);

	// Use this for initialization
	void Start () {
		m_Light = GetComponentInChildren<Light> ();
		m_BaseIntensity = m_Light.intensity;

		AudioManager.instance.listen (this);
		//AudioManager.instance.m_AudioProcessor.onBeat.AddListener (OnBeat);
	}
	
	// Update is called once per frame
	void Update () {

		if (GlobalConfig.METAL_MODE) {
			m_Light.color = m_MetalColor;
		} else {
			m_Light.color = m_TechnoColor;
		}

		// Ping Pong between 0 and 1
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
	
		// Add counter to intensity of light
		m_Light.intensity = m_BaseIntensity + m_Counter*2;
	}

	public void OnBeat(){
		Debug.Log ("BEAT");
		m_Counter = 0.001f;
		m_Beat = true;
	}
}
