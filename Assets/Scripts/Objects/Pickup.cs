using System;
using UnityEngine;

[RequireComponent(typeof (BoxCollider))]
public class Pickup : MonoBehaviour
{
	private CrystalManager manager;

	void Start(){
		manager = FindObjectOfType<CrystalManager>();
	}
		

	// If its inside a spawned wall or smth. else. reposition
	public void OnCollisionEnter(Collision collision) {
		manager.CrystalCollected (this.gameObject);
	}
}


