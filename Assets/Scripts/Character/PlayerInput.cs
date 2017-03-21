using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

    [RequireComponent(typeof (Character))]
    public class PlayerInput : MonoBehaviour
    {
      	public bool m_SecondPlayer = false;
      
        private Character m_Character; 

        private void Start()
        {
            m_Character = GetComponent<Character>();
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
	        {
			string player = "";
			if (m_SecondPlayer) {
				player = "P2 ";
			} else {
				player = "P1 ";
			}
			
			float h = CrossPlatformInputManager.GetAxis(player+"Horizontal");
			float v = CrossPlatformInputManager.GetAxis(player+"Vertical");
			// Is float bc of gamepad-input
			float action = CrossPlatformInputManager.GetAxis(player+"Action");

            
			// When action is pressed, don't walk
			if(action > 0.1f) {
				m_Character.Action();
				return;
			}

          	// we use world-relative directions
            Vector3 move = v*Vector3.forward + h*Vector3.right;

			// Dirty workaround, bc the goelm is somehow faster with player-input
			if (gameObject.name == "CrystalMaster") {
				move *= 0.8f;
			}
			
			// pass all parameters to the character control script
            m_Character.Move(move);
        }
    }

