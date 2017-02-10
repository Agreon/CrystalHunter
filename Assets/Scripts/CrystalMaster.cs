using UnityEngine;
using System.Collections;

public class CrystalMaster : Character {


	[SerializeField]public GameObject m_CrystalShot;
	[SerializeField]public GameObject m_ShotSpawn;

	/**
		TOOD: 
			+ Krystall
			+ Trap
			+ 
	
	**/
	public override void OnCollisionEnter(Collision collision){

		var go = collision.gameObject;

		if (go.tag == "Pickup") {
			m_Items++;
			Destroy (go);
		} else if (go.tag == "Trap") {
			var trap = go.GetComponent<TrapController> ();

			trap.Trigger ();
		}


	}

	/**
	 * TODO: ANimations
	 * */
	public override void Action(){
		m_Animator.Play ("Shoot");

		GameObject shot = Instantiate (m_CrystalShot, m_ShotSpawn.transform.position, m_ShotSpawn.transform.rotation, null);

		Physics.IgnoreCollision(shot.GetComponent<Collider>(), GetComponent<Collider>());
	}
	
	


}
