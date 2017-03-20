using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shot : MonoBehaviour {

	public GameObject m_Trap;
	public GameObject m_SpawnObject;
	public GameObject m_ObjectContainer;

	private Rigidbody m_RigidBody;
	private Vector3 m_SavedVelocity;

	// Use this for initialization
	void Start () {
		m_RigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (GlobalConfig.PAUSED && m_RigidBody.velocity.magnitude != 0) {
			m_SavedVelocity = m_RigidBody.velocity;
			m_RigidBody.velocity = new Vector3(0,0,0);
		}

		if (GlobalConfig.PAUSED == false && m_RigidBody.velocity.magnitude == 0) {
			m_RigidBody.velocity = m_SavedVelocity;
		}

	}

	private void CatchTheft(Theft theft){
		Debug.Log ("Hit theft");

		var position = theft.m_TrapSpawn.transform.position;
		position.y = 0.4f;

		// Using thefts Trapspawn-object for positioning
		var trapObject = Instantiate (m_Trap, position, theft.m_TrapSpawn.transform.rotation, m_ObjectContainer.transform);
		Trap trap = trapObject.GetComponent<Trap> ();

		trap.Trigger(theft);

		Destroy (this.gameObject);
	}

	private void CollideWall(Vector3 wallNormal){
		wallNormal.y = -0.65f;
		transform.rotation = Quaternion.LookRotation(wallNormal);
		GetComponent<Rigidbody>().velocity = wallNormal * 5;
	}

	private void SpawnWall(Vector3 position){
		position.y = -0.8f;
		Instantiate (m_SpawnObject, position, Quaternion.identity, m_ObjectContainer.transform);

		AudioManager.instance.PlaySound ("raiseWall");

		Destroy(this.gameObject);
	}

	public void OnCollisionEnter(Collision collision){

		var go = collision.gameObject;

		if (go.tag == "Theft") {
			var theft = go.GetComponent<Theft> ();
			CatchTheft (theft);
		} else if (go.tag == "Wall" || go.tag == "CrystalWall") {
			CollideWall (collision.contacts[0].normal);
		} else if (go.tag == "Ground") {
			
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);

			for (int i = 0; i < hitColliders.Length; i++) {
				if (hitColliders [i].gameObject.tag == "Theft") {
					Debug.Log ("Thorugh area");
					CatchTheft (hitColliders[i].gameObject.GetComponent<Theft>());
					return;
				}
			}

			/*var theft = FindObjectOfType<Theft>();
			var distance = Vector3.Distance (transform.position, theft.transform.position);

			Debug.Log ("distance");
			Debug.Log (distance);

			// If theft near, spawn trap
			if (distance < 3.3) {
				CatchTheft (theft);
				return;
			}*/

			// Otherwise, spawn Wall
			SpawnWall (transform.position);
		}
	}

}
