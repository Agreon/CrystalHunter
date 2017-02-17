using UnityEngine;
using System.Collections.Generic;

public class CrystalManager : MonoBehaviour {
	
	[SerializeField] public GameObject m_Ground;
	[SerializeField] public GameObject m_CrystalPickup;
	[SerializeField] public GameObject m_CrystalSpawns;
	[SerializeField] public int m_MaxCrystals;

	private List<GameObject> m_Crystals = new List<GameObject>();

	private int m_SpawnPosAmount = 0;

	void Start() {

		m_Ground = GameObject.FindWithTag ("Ground");

		foreach (Transform child in m_CrystalSpawns.transform)
		{
			m_SpawnPosAmount++;
		}

		for (int i = 0; i < m_MaxCrystals; i++) {
			SpawnCrystal ();
		}

	}

	/**
	 * Removes the Crystal from the list, destroys it and spawns a new one.
	*/
	public void CrystalCollected(GameObject crystal) {

		m_Crystals.Remove (crystal);
		Destroy (crystal);

		SpawnCrystal ();
	}
		
	/**
	 * 7x7 SpawnTiles
	 * */
	void SpawnCrystal(){

		Vector3 spawnAt = new Vector3 ();

		while (true) {

			int spawnPos = Random.Range (0, m_SpawnPosAmount);

			spawnAt = m_CrystalSpawns.transform.GetChild (spawnPos).transform.position;
			spawnAt.y = 4.2f;
			/*if (Physics.CheckBox (spawnAt, m_CrystalPickup.transform.localScale / 2) == false) {
				break;
			}*/
			break;
		}
			
		var crystal = Instantiate(m_CrystalPickup, spawnAt, Quaternion.LookRotation(new Vector3(1,0,0)));

		m_Crystals.Add (crystal);

		/*
		// Auslagern?
		int tileSize = 7;

		Vector3 spawnAt = new Vector3 ();

		while (true) {

			int spawnX = Random.Range (0, tileSize-1);
			int spawnY = Random.Range (0, tileSize-1);

			float tileWidth = m_Ground.transform.localScale.x / tileSize;
			float tileHeight = m_Ground.transform.localScale.z / tileSize;

			Debug.Log (m_Ground.transform.localScale.x);

			spawnAt = new Vector3 (spawnX * tileWidth, m_CrystalPickup.transform.position.y, spawnY * tileHeight);

			break;
			// CHeck for collision
			// Physics.CheckBox
			//if (Physics.CheckBox (spawnAt, m_CrystalPickup.transform.localScale / 2) == false) {
			//	break;
			//}
		}

		var crystal = Instantiate(m_CrystalPickup, spawnAt, Quaternion.LookRotation(new Vector3(0,0,0)));

		m_Crystals.Add (crystal);*/
	}

	public List<GameObject> getCrystals() {
		return m_Crystals;
	}

}
