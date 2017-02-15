using UnityEngine;
using System.Collections;

public class Theft : Character {

	[SerializeField]public GameObject m_Trap;
	[SerializeField]public GameObject m_TrapSpawn;

	// Start Animation "Die"
	public void Kill() {
		m_Animator.Play("Die");
	}
	
	/**
		OnCollision
	**/
	public override void OnCollisionEnter(Collision collision){

		var go = collision.gameObject;

		if (go.tag == "Pickup") {
			m_Crystals++;
			Destroy (go);
			SpeedUp();
		} 
	}

	/**
		Lay Trap
	**/
	public override void Action(){

		if (m_Crystals != 3) {
			return;
		}
		m_Crystals = 0;

		var spawnPos = m_TrapSpawn.transform.position;
		spawnPos.y = 0.4f;

		GameObject trap = Instantiate (m_Trap, spawnPos, m_TrapSpawn.transform.rotation, null);

		m_Animator.Play("Crouch");
		/**
		 * TODO: shorter Crouch Animation
		 * */
	}

}
