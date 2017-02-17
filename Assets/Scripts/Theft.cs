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
		TODO: UI-Feedback
	**/
	public override void Action(){

		// If Animation is already playing
		AnimatorStateInfo s = m_Animator.GetCurrentAnimatorStateInfo(0);
		if (s.shortNameHash == Animator.StringToHash ("Crouch")) {
			Debug.Log ("Currently crouching");
			return;
		}

		// If not enough crystals
		if (m_Crystals < 3) {
			Debug.Log ("Not enough crystals");
			return;
		}

		m_Crystals = 0;

		var spawnPos = m_TrapSpawn.transform.position;
		spawnPos.y = 0.4f;

		GameObject trap = Instantiate (m_Trap, spawnPos, m_TrapSpawn.transform.rotation, null);

		m_Animator.Play("Crouch");

		Debug.Log ("Trap layed");
	}

}
