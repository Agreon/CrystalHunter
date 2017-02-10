using UnityEngine;
using System.Collections;

public class Theft : Character {

	/**
		TOOD: 
			+ Krystall
			+ Trap
			+ 
	
	**/
	[SerializeField]public GameObject m_Trap;
	[SerializeField]public GameObject m_TrapSpawn;

	
	public void Kill() {
		// Start Animation
	}
	
	/**
		OnCollision
	**/
	public override void OnCollisionEnter(Collision collision){

		var go = collision.gameObject;

		if (go.tag == "Pickup") {
			m_Items++;
			Destroy (go);
		} 
	}

	/**
		TODO: Lay Trap
	**/
	public override void Action(){
		GameObject trap = Instantiate (m_Trap, m_TrapSpawn.transform.position, m_TrapSpawn.transform.rotation, null);

		Physics.IgnoreCollision(trap.GetComponent<Collider>(), GetComponent<Collider>());

		/**
		 * 
		 * TODO: Crouch Animation
		 * */

	}

}
