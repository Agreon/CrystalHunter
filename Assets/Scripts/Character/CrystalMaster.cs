using UnityEngine;
using System.Collections;

public class CrystalMaster : Character {


	[SerializeField]public int m_ShotSpeed = 7;
	[SerializeField]public GameObject m_CrystalShot;
	[SerializeField]public GameObject m_ShotSpawn;
	[SerializeField]public GameObject m_ObjectContainer;
	[SerializeField]public float m_SpellReloadCounter = 0;
	[SerializeField]public float m_SpellReloadTime = 3;

	/**
	 * Checks collision with other gameObjects
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
			var trap = go.GetComponent<Trap> ();
			trap.Trigger (this);
		} else if (go.tag == "Theft") {

			// Check if CM is actually looking at the Theft
			Vector3 toTheft = go.transform.position - transform.position;
			Vector3 lookAt = transform.forward;

			if (Vector3.Dot (toTheft, lookAt) < 0) {
				Debug.Log ("Not looking at");
				return;
			}

			// Look at theft directly
			transform.rotation = Quaternion.LookRotation(toTheft);

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

	public void Update(){
		m_SpellReloadCounter += Time.deltaTime;
	}

	public float GetReloadCounter(){
		return m_SpellReloadCounter;
	}

	/**
	 * Shoots Crystal-Magic
	 * */
	public override void Action(){

		// If not enough crystals
		//if (m_CrystalLoads < 3) {
		if (m_SpellReloadCounter < m_SpellReloadTime){
			GameManager.instance.ShowNotification (false, "Not enough Crystals!");
			AudioManager.instance.PlaySound ("not_possible");
			return;
		}

		m_SpellReloadCounter = 0;
		m_CrystalLoads = 0;

		m_Animator.Play ("Shoot");

		GameObject shot = Instantiate (m_CrystalShot, m_ShotSpawn.transform.position, transform.rotation, m_ObjectContainer.transform);

		Physics.IgnoreCollision(shot.GetComponent<Collider>(), GetComponent<Collider>());
		shot.GetComponent<Rigidbody>().velocity = transform.forward * m_ShotSpeed;		
	}

	public override void Reset(){
		base.Reset ();
		m_SpellReloadCounter = 0;
	}
}
