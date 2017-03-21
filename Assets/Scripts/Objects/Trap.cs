using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
	private bool m_IsTriggered = false;
	private float m_TimeActive = 0f;

	[SerializeField] public float m_Duration = 2.0f;

	private Trappable m_TrappedObject;
	private Animator m_Animator;

	void Start() {
		m_Animator = GetComponentInChildren<Animator>();
		Debug.Log ("Start");
	}

	public void Trigger(Trappable trappable) {
		if(m_IsTriggered){
			Debug.Log ("Already Triggered");
			return;
		}

		if (!m_Animator) {
			m_Animator = GetComponentInChildren<Animator>();
		}

		AudioManager.instance.PlaySoundQueue ("raiseTrap");

		m_IsTriggered = true;

		// TODO: Don't know why 'Trigger' is started faster than 'Start'
		if (!m_Animator) {
			m_Animator = GetComponentInChildren<Animator>();
		}

		m_Animator.SetBool ("Triggered", true);

		// Block object
		m_TrappedObject = trappable;
		m_TrappedObject.Trap();

		// Disable collider
		var collider = GetComponent<BoxCollider>();
		collider.enabled = false;

		// Set to center position of object
		var pos = transform.position;
		pos.x = m_TrappedObject.transform.position.x;
		pos.z = m_TrappedObject.transform.position.z;
		transform.position = pos;

		// Disable Hint
		ShowHint (false);

		// Enable Glowing
		ShowTrap(true);
	}

	public void Update() {

		if (GlobalConfig.PAUSED) {
			return;
		}

		if (m_IsTriggered) {
			m_TimeActive += Time.deltaTime;
		}

		if (m_IsTriggered && m_TimeActive >= m_Duration) {

			m_Animator.SetBool ("Triggered", false);

			m_TrappedObject.Release ();

			if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			{
				// Avoid any reload.
				Destroy(this.gameObject);
			}
		}		
	}

	public void ShowHint(bool show){
		transform.Find ("Hint").GetComponent<Renderer> ().enabled = show; 
	}

	public void ShowTrap(bool show){
		transform.Find ("TrapObject").GetComponentInChildren<Light> ().enabled = show; 
	}
}
