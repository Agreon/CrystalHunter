using UnityEngine;
using System.Collections.Generic;

namespace UnityStandardAssets.Utility
{

	public class CameraController : MonoBehaviour
	{
		/*
		public Color color1 = Color.red;
		public Color color2 = Color.blue;*/
		public Color m_ColorStandart = new Color (57 / 255, 0, 0);
		public Color m_ColorMetal = new Color (57 / 255, 0, 0);

//		public float duration = 3.0F;

		void Start(){
		}

		void Update(){
			if (GlobalConfig.METAL_MODE) {
				GetComponent<Camera>().backgroundColor = m_ColorMetal;
			} else {
				GetComponent<Camera>().backgroundColor = m_ColorStandart;
			}
			//float t = Mathf.PingPong(Time.time, duration) / duration;
			//GetComponent<Camera>().backgroundColor = Color.Lerp(color1, color2, t);
		}
	}
}
