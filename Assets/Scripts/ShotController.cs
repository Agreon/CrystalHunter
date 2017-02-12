using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShotController : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnCollisionEnter(Collision collision){

		var go = collision.gameObject;

		if (go.tag == "Theft") {

			Debug.Log ("Hit theft");

			var theft = go.GetComponent<Theft> ();

			// Using thefts Trapspawn-object for positioning
			var trapObject = Instantiate (theft.m_Trap, theft.m_TrapSpawn.transform.position, theft.m_TrapSpawn.transform.rotation, null);
			TrapController trap = trapObject.GetComponent<TrapController> ();

			trap.Trigger(theft);

			Destroy (this);

			/*
			 * Init Trap
			 * Activate Trap
			 * Stop Player
			 * 		go-> Trapped?
			 * */
			// Destroy(this);
		} 
	}

}
