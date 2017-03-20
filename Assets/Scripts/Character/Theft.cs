using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Theft : Character {

	[SerializeField]public GameObject m_Trap;
	[SerializeField]public GameObject m_TrapSpawn;
	[SerializeField]public GameObject m_ObjectContainer;
	[SerializeField] public float m_CrouchTime = 0.7f;
	private float m_CrouchCounter = 0;

	// Start Animation "Die"
	public void Kill() {
		AudioManager.instance.PlaySoundQueue ("wilhelm");
		m_Animator.Play("Die");
		Debug.Log ("Die");
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
		if (m_CrouchCounter > 0) {
			m_CrouchCounter += Time.deltaTime;		
		}

		if (m_CrouchCounter >= m_CrouchTime) {
			//m_Animator.SetBool ("Crouch", false);
			Debug.Log ("Finish crouch");
			m_CrouchCounter = 0;
		}
	}

	/**
		Lay Trap
		TODO: UI-Feedback (ShowPopup!)
	**/
	public override void Action(){

		// If Animation is already playing, dont execute
		AnimatorStateInfo s = m_Animator.GetCurrentAnimatorStateInfo(0);
		if (s.shortNameHash == Animator.StringToHash ("Crouch")) {
			return;
		}

		// If not enough crystals
		if (m_CrystalLoads < 3) {
			GameManager.instance.ShowNotification (true, "Not enough Crystals!");
			AudioManager.instance.PlaySound ("not_possible");
			return;
		}

		m_CrystalLoads = 0;

		var spawnPos = m_TrapSpawn.transform.position;
		spawnPos.y = 0.4f;	// TODO: Remove through inital good start
		
		GameObject trap = Instantiate (m_Trap, spawnPos, m_TrapSpawn.transform.rotation, m_ObjectContainer.transform);

		// If in Singleplayer show a traphint
		if(GlobalConfig.MULTIPLAYER == false){
			trap.GetComponent<Trap>().ShowHint(true);
		}

		m_Animator.Play("Crouch");

		//m_Animator.SetBool ("Crouch", true);
		m_CrouchCounter = Time.deltaTime;
	}

	public override void Reset(){
		base.Reset ();
		m_CrouchCounter = 0;
		/*
		transform.position = m_StartTransform.position;
		transform.rotation = m_StartTransform.rotation;
		m_CrouchCounter = 0;
		m_SpeedUpCounter = 0;
		m_Crystals = 0;
		m_CrystalLoads = 0;*/
	}
}
