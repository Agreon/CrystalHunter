﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrystalManager : MonoBehaviour {

	public static CrystalManager instance = null;

	[SerializeField] public GameObject m_CrystalPickup;
	[SerializeField] public GameObject m_CrystalSpawns;
	[SerializeField] public GameObject m_ObjectContainer;
	[SerializeField] public int m_MaxCrystals;
	[SerializeField] public int m_CrystalSpawnRate = 3;

	private List<GameObject> m_Crystals = new List<GameObject>();

	private int m_SpawnPosAmount = 0;
	private bool m_Spawning = false;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	void Start() {
		foreach (Transform child in m_CrystalSpawns.transform)
		{
			m_SpawnPosAmount++;
		}
	}


	void Update(){
		if (m_Spawning && m_Crystals.Count < m_MaxCrystals) {
			int r = Random.Range (0, 1000);
			if (r <= m_CrystalSpawnRate) {
				SpawnCrystal ();
			}
		}
	}
		
	/**
	 * Removes the Crystal from the list, destroys it.
	*/
	public void CrystalCollected(GameObject crystal) {
		m_Crystals.Remove (crystal);
		Destroy (crystal);
	}

	public void clear(){
		m_Crystals = new List<GameObject>();
	}
		
	public void SpawnCrystal(Vector3 position = default(Vector3)){

		// If position is already set
		if (position.magnitude != 0) {
			m_Crystals.Add (Instantiate(m_CrystalPickup, position, Quaternion.LookRotation(new Vector3(1,0,0)), m_ObjectContainer.transform));
			return;
		}

		Vector3 spawnAt = new Vector3 ();

		while (true) {

			int spawnPos = Random.Range (0, m_SpawnPosAmount);

			spawnAt = m_CrystalSpawns.transform.GetChild (spawnPos).transform.position;

			// Check if the crystal would be colliding with something
			if (Physics.CheckBox (spawnAt, m_CrystalPickup.transform.localScale / 2) == false) {
				break;
			}
		}
			
		var crystal = Instantiate(m_CrystalPickup, spawnAt, Quaternion.LookRotation(new Vector3(1,0,0)), m_ObjectContainer.transform);

		m_Crystals.Add (crystal);
	
	}

	public List<GameObject> getCrystals() {
		return m_Crystals;
	}

	public void enable(){
		m_Spawning = true;
	}

	public void disable(){
		m_Spawning = false;
	}

}
