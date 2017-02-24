using UnityEngine;
using System.Collections.Generic;



	public class CameraController : MonoBehaviour
	{

	public Color m_ColorStandart = new Color (0, 0, (float)50/255);
	public Color m_ColorMetal = new Color ((float)41 / 255, 0, 0);

		public float m_LerpDuration = 3;
		public float m_ColorVariance = 30;

		private float m_ColodAdd = 0;

		private bool m_Beat = false;
		private float m_Counter = 0;

		void Start(){
		}


		// TODO: Lerping between red and orange
		void Update(){

			//float t = Mathf.PingPong(Time.time, m_LerpDuration) / m_LerpDuration;


			Color color1, color2;

			/*if (GlobalConfig.METAL_MODE) {
				color1 = m_ColorMetal;
				color2 = m_ColorMetal;
				color2.r += m_ColorVariance / 255;
			} else {
				color1 = m_ColorStandart;
				color2 = m_ColorStandart;
				color2.b += m_ColorVariance / 255;		
			}*/
		color1 = m_ColorMetal;
		color2 = m_ColorMetal;
		color2.r += m_ColorVariance / 255;
		//	color1.a = m_ColodAdd;
			
			if(m_Beat && m_Counter < 1){
			m_Counter += Time.deltaTime * 8;
			//float t = Mathf.PingPong(Time.time, m_Counter);
			//float t = Mathf.PingPong(Time.time, m_Counter) / m_Counter;
			}

			if (m_Counter > 1) {
				m_Beat = false;
			m_Counter -= Time.deltaTime * 8;
				
			//	Debug.Log ("End");
			}


		if(m_Beat == false && m_Counter < 0){
			m_Counter = 0;
		}

		color1 = Color.Lerp(color1, color2, m_Counter);


			GetComponent<Camera>().backgroundColor = color1;
			//GetComponent<Camera>().backgroundColor = Color.Lerp(color1, color2, t);

	}

	public void OnBeat(){
		//m_ColorStandart = new Color ((float)(Random.Range (50, 120))/255f, 0, 0, 255);
		m_Counter = 0.001f;
		m_Beat = true;
		//Debug.Log ("Beat");
	}

	public void OnSpectrum(float[] spectrum){
		m_ColodAdd = spectrum [0];
	}

}

