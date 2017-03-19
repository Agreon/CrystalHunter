using System;
using UnityEngine;

public class Pillar : MonoBehaviour
{
	void Start(){
	}

	public void UpdateMaterial(){
		Material blueCrystal = null, redCrystal = null;

		if (GlobalConfig.IN_GAME) {
			blueCrystal = Resources.Load ("SBlueCrystal", typeof(Material)) as Material;
			redCrystal = Resources.Load ("SRedCrystal", typeof(Material)) as Material;
		} else {
			blueCrystal = Resources.Load("BlueCrystal", typeof(Material)) as Material;
			redCrystal = Resources.Load("RedCrystal", typeof(Material)) as Material;
		}

		// Set material accordingly to mode
		foreach (Transform child in transform)
		{
			var renderer = child.gameObject.GetComponent<Renderer> ();
			if (GlobalConfig.METAL_MODE) {
				renderer.material = redCrystal;
			} else {
				renderer.material = blueCrystal;
			}
		}
	}
}


