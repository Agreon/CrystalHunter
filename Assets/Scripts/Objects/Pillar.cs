using System;
using UnityEngine;

public class Pillar : MonoBehaviour
{
	private Material m_SBlueCrystal;
	private Material m_SRedCrystal;
	private Material m_BlueCrystal;
	private Material m_RedCrystal;

	void Start(){
	}

	/**
	 * Because Start() is called later than UpdateMaterial
	 * */
	public void LoadMaterial(){
		m_SBlueCrystal = Resources.Load ("SBlueCrystal", typeof(Material)) as Material;
		m_SRedCrystal = Resources.Load ("SRedCrystal", typeof(Material)) as Material;
		m_BlueCrystal = Resources.Load("BlueCrystal", typeof(Material)) as Material;
		m_RedCrystal = Resources.Load("RedCrystal", typeof(Material)) as Material;
	}

	public void UpdateMaterial(){
		Material material;

		if (GlobalConfig.METAL_MODE) {
			material = m_SRedCrystal;
		} else {
			material = m_SBlueCrystal;
		}

		/*if (GlobalConfig.IN_GAME) {
			if (GlobalConfig.METAL_MODE) {
				material = m_SRedCrystal;
			} else {
				material = m_SBlueCrystal;
			}
		} else {
			if (GlobalConfig.METAL_MODE) {
				material = m_RedCrystal;
			} else {
				material = m_BlueCrystal;
			}
		}*/

		// Set material accordingly to mode
		foreach (Transform child in transform)
		{
			var renderer = child.gameObject.GetComponent<Renderer> ();
			renderer.material = material;
		}
	}
}


