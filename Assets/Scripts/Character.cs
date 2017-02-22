using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

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
	
	private float m_SpeedUpCounter;

	public CrystalManager m_CrystalManager;
	
	// TODO: Check whats needed
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
	[SerializeField] float m_MoveSpeedMultiplier = 1f;
	[SerializeField] float m_AnimSpeedMultiplier = 1f;

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
			Debug.Log ("SpeedEnd");
		}

		//move *= m_CurrentSpeed;
				
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
		Debug.Log ("SpeedUp");
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
		

		// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		// which affects the movement speed because of the root motion.
		if (move.magnitude > 0)
		{
			m_Animator.speed = m_AnimSpeedMultiplier;
		}
		else
		{
			m_Animator.speed = 1;
		}
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
		// So that the char is in the middle of the trap
		this.Move (new Vector3 (0,0,0));
		m_Animator.SetFloat("Forward", 0, 0.5f, Time.deltaTime);
		m_Animator.SetFloat("Turn", 0, 0.5f, Time.deltaTime);

		m_Trapped = true;
		
		// Disable NavMeshAgent
		if (GetComponent<UnityEngine.AI.NavMeshAgent> () != null) {
			GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
		}
	}

	public override void Release(){
		m_Trapped = false;
	
		// Enable NavMeshAgent
		if (GetComponent<UnityEngine.AI.NavMeshAgent> () != null) {
			GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
		}
	}

	public int GetCrystals(){
		return m_Crystals;
	}
		
	public int GetCrystalLoads(){
		return m_CrystalLoads;
	}
		
	public abstract void Action();
	public abstract void OnCollisionEnter(Collision collision); 
	
}
