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
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis("Vertical");
            float action = CrossPlatformInputManager.GetAxis("Jump");
			//bool action = Input.GetKey(KeyCode.Space);
            
			// When action is pressed, don't walk
			if(action > 0.1f) {//if(action) {
				m_Character.Action();
				return;
			}

          	// we use world-relative directions
            Vector3 move = v*Vector3.forward + h*Vector3.right;

            // pass all parameters to the character control script
            m_Character.Move(move);
        }
    }

