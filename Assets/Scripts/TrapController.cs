﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour {
	private bool m_IsTriggered = false;
	private float m_TimeActive = 0f;

	[SerializeField] public float m_Duration = 3.0f;

	private Trappable m_TrappedObject;
	private Animator m_Animator;

	public void Start() {
		m_Animator = GetComponentInChildren<Animator>();
	}

	public void Trigger(Trappable trappable) {
		m_IsTriggered = true;

		m_Animator.SetBool ("Triggered", true);

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
	}

	public void Update() {
		if (m_IsTriggered) {
			m_TimeActive += Time.deltaTime;
		}

		if (m_IsTriggered && m_TimeActive >= m_Duration) {

			m_Animator.SetBool ("Triggered", false);

			m_TrappedObject.Release ();

			Destroy (this);
		}
	}
}