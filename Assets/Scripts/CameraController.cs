using UnityEngine;
using System.Collections.Generic;



	public class CameraController : MonoBehaviour
	{

		public Color m_ColorStandart = new Color (0, 0, (float)50/255);
	public Color m_ColorMetal = new Color ((float)163 / 255, (float)57/255, (float)32/255);
	public Color m_ColorMetal2 = new Color ((float)170/ 255, (float)90/255, (float)26/255);

		public float m_LerpDuration = 3;
		public float m_ColorVariance = 30;

		private float m_ColodAdd = 0;

		private bool m_Beat = false;
		private float m_Counter = 0;

		void Start(){
		}


		// TODO: Lerping between red and orange
		void Update(){

			float t = Mathf.PingPong(Time.time, m_LerpDuration) / m_LerpDuration;


			Color color1, color2;

			if (GlobalConfig.METAL_MODE) {
				color1 = m_ColorMetal;
				color2 = m_ColorMetal2;
			} else {
				color1 = m_ColorStandart;
				color2 = m_ColorStandart;
				color2.b += m_ColorVariance / 255;		
			}

			color1 = Color.Lerp(color1, color2, t);
			GetComponent<Camera>().backgroundColor = color1;
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

