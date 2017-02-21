using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShotController : MonoBehaviour {

	public GameObject m_Trap;
	public GameObject m_SpawnObject;

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
			var position = theft.m_TrapSpawn.transform.position;
			position.y = 0.4f;
			
			// Using thefts Trapspawn-object for positioning
			var trapObject = Instantiate (m_Trap, position, theft.m_TrapSpawn.transform.rotation, null);
			TrapController trap = trapObject.GetComponent<TrapController> ();
			trap.m_Duration = 2f;

			trap.Trigger(theft);

			Destroy (this.gameObject);

		} else if (go.tag == "Wall") {
			   Vector3 collNormal = collision.contacts[0].normal;
			   collNormal.y = -0.65f;
				transform.rotation = Quaternion.LookRotation(collNormal);
			   GetComponent<Rigidbody>().velocity = collNormal * 5;		
			   
		} else if (go.tag == "Ground") {
			// Spawn Wall
			var position = transform.position;
			position.y = -0.8f;
			
			Instantiate (m_SpawnObject, position, Quaternion.identity, null);
			
			Destroy(this.gameObject);
		}
	}

}
