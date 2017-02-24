using UnityEngine;
using UnityEngine.Events;
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
			if(m_CrystalLoads < 3) {
				m_CrystalLoads++;			
			}
			CrystalManager.instance.CrystalCollected (go);
			SpeedUp ();
		} /*else if (go.tag == "CrystalWall") {
			
			//TODO: Check if in animation Get position
			// Or rather check if position treshold

			Debug.Log ("COLLISION");

			Destroy (go);

			var trapObject = Instantiate (m_Trap, m_TrapSpawn.transform.position, m_TrapSpawn.transform.rotation, null);
			TrapController trap = trapObject.GetComponent<TrapController> ();
			trap.m_Duration = 2f;

			trap.Trigger(this);
		}*/
	}

	/**
		Lay Trap
		TODO: UI-Feedback
	**/
	public override void Action(){

		// If Animation is already playing, dont execute
		AnimatorStateInfo s = m_Animator.GetCurrentAnimatorStateInfo(0);
		if (s.shortNameHash == Animator.StringToHash ("Crouch")) {
			return;
		}

		// If not enough crystals
		if (m_CrystalLoads < 3) {
			Debug.Log ("Not enough crystals");
			return;
		}

		m_CrystalLoads = 0;

		var spawnPos = m_TrapSpawn.transform.position;
		spawnPos.y = 0.4f;	// TODO: Remove through inital good start
		

		GameObject trap = Instantiate (m_Trap, spawnPos, m_TrapSpawn.transform.rotation, null);

		// If in Singleplayer show a traphint
		if(GlobalConfig.MULTIPLAYER == false){
			trap.GetComponent<Trap>().ShowHint(true);
		}

		m_Animator.Play("Crouch");

		Debug.Log ("Trap layed");
	}

}
