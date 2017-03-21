using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Theft : Character {

	[SerializeField]public GameObject m_Trap;
	[SerializeField]public GameObject m_TrapSpawn;
	[SerializeField]public GameObject m_ObjectContainer;

	// Start Animation "Die"
	public void Kill() {
		if (GlobalConfig.METAL_MODE) {
			AudioManager.instance.PlaySoundQueue ("mad_scream");
		} else {
			AudioManager.instance.PlaySoundQueue ("wilhelm");
		}
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
		}
	}

	public void Update(){
	}

	public override bool ActionAllowed(){
		// If Animation is already playing, dont execute
		AnimatorStateInfo s = m_Animator.GetCurrentAnimatorStateInfo(0);
		if (s.shortNameHash == Animator.StringToHash ("Crouch")) {
			return false;
		}

		// If not enough crystals
		if (m_CrystalLoads < 3) {
			// Show Popup
			GameManager.instance.ShowNotification (true, "Not enough Crystals!");
			AudioManager.instance.PlaySound ("not_possible");
			return false;
		}

		return true;
	}

	/**
		Lay Trap
	**/
	public override void Action(){

		if (!ActionAllowed ()) {
			return;
		}

		m_CrystalLoads = 0;

		// Set SpawnPos of Trap
		var spawnPos = m_TrapSpawn.transform.position;
		spawnPos.y = 0.4f;	
		
		GameObject trap = Instantiate (m_Trap, spawnPos, m_TrapSpawn.transform.rotation, m_ObjectContainer.transform);

		// If in Singleplayer show a traphint
		if(GlobalConfig.MULTIPLAYER == false){
			trap.GetComponent<Trap>().ShowHint(true);
		}

		m_Animator.Play("Crouch");
		AudioManager.instance.PlaySound ("lay_trap");
	}

	public override void Reset(){
		base.Reset ();
	}
}
