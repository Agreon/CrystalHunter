using UnityEngine;
using System.Collections.Generic;

namespace UnityStandardAssets.Utility
{
	/**
	 * TODO: Remove unecessary stuff
	 * 
	 * */
	public class CameraController : MonoBehaviour
	{

		// The target we are following
		[SerializeField]
		private List<Transform> targets;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
		// the height we want the camera to be above the target
		[SerializeField]
		private float height = 1.0f;

		[SerializeField]
		private float heightDamping;

		[SerializeField]
		private float m_MinHeight = 2f;
		
		[SerializeField]
		private float maxHeight = 8f;

		// TODO: Debug
		[SerializeField]
		private GameObject centerObject;
	
		// Use this for initialization
		void Start() { 		
		}

		// Update is called once per frame
		void LateUpdate()
		{
			if(targets.Count < 1)
				return;
				
				
			// Find Center of Objects	
			var center = new Vector3(0,0,0);	
			
			foreach(Transform _target in targets) {
				center += _target.position;
			}
				
			var target = centerObject.transform;

			target.position = center / targets.Count;

			// Calculate Height
			
			var centerDist = Vector3.Distance(targets[0].position, target.position);


			var wantedHeight = target.position.y + height + (centerDist*1.8f);
			if (wantedHeight < m_MinHeight) {
				wantedHeight = m_MinHeight;
			}


			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position - (Vector3.forward * distance);

			//transform.position.z += 3;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x ,wantedHeight , transform.position.z);


		}
	}
}
