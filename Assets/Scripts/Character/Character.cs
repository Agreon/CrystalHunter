using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public abstract class Character : Trappable
{
	public int m_Crystals = 0;
	public int m_CrystalLoads = 0;
	public float m_MovementSpeed = 1;
	public float m_CurrentSpeed;
	public float m_SpeedUpDuration = 2;
	//TODO: SpeedUpMultiplier

	private float m_OverloadCounter;
	protected float m_SpeedUpCounter;

	public Transform m_StartTransform;

	// TODO: Check whats needed
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others

	Rigidbody m_Rigidbody;
	protected Animator m_Animator;
	const float k_Half = 0.5f;
	float m_TurnAmount;
	float m_ForwardAmount;
	Vector3 m_GroundNormal;
	
	public Character() {}
	
	void Start()
	{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			
			m_CurrentSpeed = m_MovementSpeed;
	}

	public void Move(Vector3 move)
	{
		// If Speeded Up
		if(m_CurrentSpeed != m_MovementSpeed) {
			m_SpeedUpCounter += Time.deltaTime;	
		}
		
		// End Speed Up
		if(m_SpeedUpCounter > 0 && m_SpeedUpCounter >= m_SpeedUpDuration) {
			m_CurrentSpeed = m_MovementSpeed;
			m_SpeedUpCounter = 0;
		}
	
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);
		
		move = Vector3.ProjectOnPlane(move, m_GroundNormal);
		m_TurnAmount = Mathf.Atan2(move.x, move.z);
		m_ForwardAmount = move.z;

		m_ForwardAmount *= m_CurrentSpeed;

		ApplyExtraTurnRotation();

		// send input and other state parameters to the animator
		UpdateAnimator(move);
	}

	public void SpeedUp(){
		m_CurrentSpeed = m_MovementSpeed*1.5f;
		m_SpeedUpCounter = 0;

		// Workaround for animator-bug
		if (GlobalConfig.MULTIPLAYER && gameObject.name == "CrystalMaster") {
			m_CurrentSpeed = m_MovementSpeed * (1.5f * 0.8f);
		}
	}

	public void Overload(){
		//FindObjectOfType<Camera> ().GetComponent<Twirl> ().enabled = true;

	}

	void UpdateAnimator(Vector3 move)
	{
		if (m_Trapped) {
			m_Animator.SetFloat ("Forward", 0, 0.2f, Time.deltaTime);
			m_Animator.SetFloat ("Turn", 0, 0.2f, Time.deltaTime);
		} else {
			m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);

		}

		// calculate which leg is behind, so as to leave that leg trailing in the jump animation
		// (This code is reliant on the specific run cycle offset in our animations,
		// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
		float runCycle =
			Mathf.Repeat(
				m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
		float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
		m_Animator.SetFloat("JumpLeg", jumpLeg);
	}

	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}


	public void OnAnimatorMove()
	{
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
		if (Time.deltaTime > 0)
		{
			Vector3 v = (m_Animator.deltaPosition * m_CurrentSpeed) / Time.deltaTime;
			// we preserve the existing y part of the current velocity.
			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}
	}

	public override void Trap(){
		Debug.Log ("Catched");
		m_Trapped = true;
		this.DisableInput ();

		/*// So that the char is in the middle of the trap
		this.Move (new Vector3 (0,0,0));
		m_Animator.SetFloat("Forward", 0, 0.5f, Time.deltaTime);
		m_Animator.SetFloat("Turn", 0, 0.5f, Time.deltaTime);*/
	}

	public override void Release(){
		m_Trapped = false;

		//m_Animator.Play("Grounded");
	
		// TODO: Test
		if (GlobalConfig.MULTIPLAYER) {
			GetComponent<PlayerInput> ().enabled = true;		
		} else {
			if (GetComponent<AIInput> () != null) {
				GetComponent<AIInput> ().enabled = true;
			}

			// Enable NavMeshAgent
			if (GetComponent<UnityEngine.AI.NavMeshAgent> () != null) {
				GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
			}
		}

	}

	public int GetCrystals(){
		return m_Crystals;
	}
		
	public int GetCrystalLoads(){
		return m_CrystalLoads;
	}
		
	public void DisableInput(){
		GetComponent<PlayerInput>().enabled = false;
		if (GetComponent<AIInput> () != null) {
			GetComponent<AIInput>().enabled = false;
		}

		this.Move (new Vector3 (0, 0, 0));

		m_Animator.SetFloat("Forward", 0);
		m_Animator.SetFloat("Turn", 0);

		// Disable navmeshagent
		if (GetComponent<NavMeshAgent> () != null) {
			GetComponent<NavMeshAgent>().enabled = false;
		}
	}

	public virtual void Reset(){
		transform.position = m_StartTransform.position;
		transform.rotation = m_StartTransform.rotation;
		m_CurrentSpeed = m_MovementSpeed;
		m_SpeedUpCounter = 0;
		m_Crystals = 0;
		m_CrystalLoads = 0;
	}
		
	public abstract void Action();
	public abstract void OnCollisionEnter(Collision collision); 
}
