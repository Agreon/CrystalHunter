using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatListener : MonoBehaviour {

	public Color m_TechnoColor = new Color(78f/255,126f/255,245f/255);
	public Color m_MetalColor = new Color(210f/255,17f/255,17f/255);
	public float m_ColorMultiplier = 0.5f;

	private float m_Counter = 0;
	private bool m_Beat = false;
	private Light m_Light;
	private float m_BaseIntensity;

	private float m_CurrentValue;

	// Use this for initialization
	void Start () {
		m_Light = GetComponent<Light> ();
		m_BaseIntensity = m_Light.intensity;

		AudioManager.instance.Listen (this);
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
	
		if (m_CurrentValue > 0.02) {
			m_Light.intensity = m_BaseIntensity + (1);
		} else {
			m_Light.intensity = m_BaseIntensity;
		}

		// Add counter to intensity of light
		//m_Light.intensity = m_BaseIntensity + (m_Counter * m_ColorMultiplier);
		//m_Light.intensity = m_BaseIntensity + (m_CurrentValue * 50 * m_ColorMultiplier);
	}

	public void OnBeat(){
		Debug.Log ("BEAT");
		m_Counter = 0.001f;
		m_Beat = true;
	}

	public void OnSpectrum(float[] avgs){
		float avg = 0;

		for (int i = 0; i < avgs.Length; i++) {
			avg += avgs[i];
		}
				
	//	Debug.Log (avg);

		m_CurrentValue = avg / avgs.Length;
	}
}
