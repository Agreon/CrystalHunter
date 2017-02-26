using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


    [RequireComponent(typeof (Character))]
    public class PlayerInput : MonoBehaviour
    {
      	public bool _secondPlayer = false;
      
        private Character m_Character; 

        private void Start()
        {
            m_Character = GetComponent<Character>();
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
	        {
			string player = "";
			if (_secondPlayer) {
				player = "P2 ";
			} else {
				player = "P1 ";
			}
			
			float h = CrossPlatformInputManager.GetAxis(player+"Horizontal");
			float v = CrossPlatformInputManager.GetAxis(player+"Vertical");
			float action = CrossPlatformInputManager.GetAxis(player+"Action");

            
			// When action is pressed, don't walk
			if(action > 0.1f) {
				m_Character.Action();
				return;
			}

          	// we use world-relative directions
            Vector3 move = v*Vector3.forward + h*Vector3.right;

			if (_secondPlayer) {
				Debug.Log (move);	// Max. 1

			}
			
			/*if (move.x > 0.7)
				move.x = 0.7f;
			else if (move.x < -0.7)
				move.x = -0.7f;

			if (move.z > 0.7)
				move.z = 0.7f;
			else if (move.z < -0.7)
				move.z = -0.7f;*/

			// pass all parameters to the character control script
            m_Character.Move(move);
        }
    }

