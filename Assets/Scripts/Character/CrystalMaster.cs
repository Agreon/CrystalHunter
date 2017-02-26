using UnityEngine;
using System.Collections;

public class CrystalMaster : Character {


	[SerializeField]public int m_ShotSpeed = 7;
	[SerializeField]public GameObject m_CrystalShot;
	[SerializeField]public GameObject m_ShotSpawn;
	[SerializeField]public GameObject m_ObjectContainer;

	/**
	 * Checks collision with other gameObjects
	 * 
	 * TODO: Sounds / Anims / UI
	 **/ 
	public override void OnCollisionEnter(Collision collision){

		var go = collision.gameObject;

		if (go.tag == "Pickup") {
			m_Crystals++;
			if(m_CrystalLoads < 3) {
				m_CrystalLoads++;			
			}
			CrystalManager.instance.CrystalCollected (go);
			SpeedUp();
		} else if (go.tag == "Trap") {
			Debug.Log ("Collision with Trap");
			var trap = go.GetComponent<Trap> ();
			trap.Trigger (this);
		} else if (go.tag == "Theft") {
			m_Animator.Play ("Punch");
		
			var theft = go.GetComponent<Theft> ();
			theft.Kill ();
			
			StartCoroutine(WinGame());
		}
	}
	
	IEnumerator WinGame(){
		yield return new WaitForSeconds(1);
		GameManager.instance.m_GameOver = true;
	}
		
	/**
	 * Shoots Magic
	 * */
	public override void Action(){

		// If not enough crystals
		if (m_CrystalLoads < 3) {
			Debug.Log ("Not enough crystals");
			return;
		}
		m_CrystalLoads = 0;

		m_Animator.Play ("Shoot");

		GameObject shot = Instantiate (m_CrystalShot, m_ShotSpawn.transform.position, transform.rotation, m_ObjectContainer.transform);

		Physics.IgnoreCollision(shot.GetComponent<Collider>(), GetComponent<Collider>());
		shot.GetComponent<Rigidbody>().velocity = transform.forward * m_ShotSpeed;		
	}
}
