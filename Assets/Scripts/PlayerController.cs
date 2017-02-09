using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 5f;


	void Update() {
		// really simple player input. just move on the x-z plane
		var input = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );
		
		var move = input * moveSpeed;
		
		GetComponent<CharacterController>().Move(move * Time.deltaTime);
				
				
		GetComponent<Animator>().SetFloat("Blend", move.z, 0.1f, Time.deltaTime);
		
		//GetComponent<ThirdPersonCharacter>().Move( input * moveSpeed * Time.deltaTime,false,false );
	}
}
