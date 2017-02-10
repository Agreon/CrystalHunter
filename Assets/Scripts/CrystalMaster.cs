using UnityEngine;
using System.Collections;

public class CrystalMaster : Character {

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
		TODO: Shoot Wizardry
	**/
	public override void Action(){
		
	}
	
	


}
