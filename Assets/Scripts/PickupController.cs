using System;
using UnityEngine;

[RequireComponent(typeof (BoxCollider))]
public class PickupController : MonoBehaviour
{
	private CrystalManager manager;

	void Start(){
		manager = FindObjectOfType<CrystalManager>();
	}
		
	public void OnCollisionEnter(Collision collision) {
		manager.CrystalCollected (this.gameObject);
	}
}


