using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (Character))]
    public class AIInput : MonoBehaviour
    {      
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
            bool action = Input.GetKey(KeyCode.C);
            
            Vector3 move = new Vector3();
            
            // When action is pressed, don't walk
            if(action) {
    			// Stop char	
            	m_Character.Move(move);
            	
            	m_Character.Action();
            	return;
            }
		
             // we use world-relative directions
             move = v*Vector3.forward + h*Vector3.right;
           
			/**
				TODO: do stuff with currentSpeed of char
				=> Falls er kristalle aufhebt
			**/

            // pass all parameters to the character control script
            m_Character.Move(move);
        }
    }
}
