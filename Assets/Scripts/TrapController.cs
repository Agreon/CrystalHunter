using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour {
	private bool m_IsTriggered = false;

	/**
		TODO: Some Animation
		**/


	public void Trigger() {
		Debug.Log ("Triggered");
		m_IsTriggered = true;
	}
}