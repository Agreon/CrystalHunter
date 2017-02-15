﻿using UnityEngine;

public abstract class Trappable : MonoBehaviour {

	protected bool m_Trapped = false;

	public abstract void Trap();
	public abstract void Release();
}
