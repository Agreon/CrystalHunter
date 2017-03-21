using UnityEngine;

/**
 * 'Interface' for trappable objects
 * */
public abstract class Trappable : MonoBehaviour {

	protected bool m_Trapped = false;

	public bool IsTrapped(){
		return m_Trapped;
	}

	public abstract void Trap();
	public abstract void Release();
}
