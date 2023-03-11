// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using UnityEngine.UI;
using Wave.Native;
using Wave.Essence.Hand.StaticGesture;
using UnityEngine.SceneManagement;

namespace Wave.Essence.Hand.Model.Demo
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Text))]
	sealed class CustomGesture1 : MonoBehaviour
	{
        [SerializeField] private Rigidbody physicsBody;
		[SerializeField] private Rigidbody hand;
		[SerializeField] private float selectedTime = 80f;
		[SerializeField] private Text xyz;
		[SerializeField] private int bound = 10;

		private Vector3 initPosition, initVelocity;
		private float time, timeBarrier;
		private bool handflag = false;
		
		void Start() {
			initPosition = hand.position;
			timeBarrier = selectedTime;
		}
		
		#region MonoBehaviour Overrides

		void Update() {
			time += Time.deltaTime;
			string leftGesture = WXRGestureHand.GetSingleHandGesture(true);
			string rightGesture = WXRGestureHand.GetSingleHandGesture(false);

			if (leftGesture.Contains("Fist")
				&& (time > selectedTime)  
				){

				if (handflag) {
					Vector3 Vel = GetRoundedVector3((GetRoundedVector3(hand.position) - initPosition) / Time.deltaTime);
					Vector3 deltaVel = - PutBoundaries(( Vel - initVelocity))*5000;
					physicsBody.AddForce(deltaVel);
					PrintVector(deltaVel);
					initVelocity = Vel;
					
					timeBarrier += 1;
				} else {
					handflag = true;
					initVelocity = PutBoundaries(GetRoundedVector3((GetRoundedVector3(hand.position) - initPosition) / Time.deltaTime));
					initPosition = GetRoundedVector3(hand.position);
				}
			} else {
				handflag = false;
			}
			initPosition = GetRoundedVector3(hand.position);
			
		}

		void PrintVector(Vector3 vector) {
        	xyz.text = ("X : " + vector.x.ToString("F6") + 
						// "\n X Velocity: " + hand.velocity.x.ToString("F6") +
						// "\nX: " + vector.x.ToString("F6") +
						"\nY : " + vector.y.ToString("F6") + 
						// "\n Y Velocity: " + hand.velocity.y.ToString("F6") + 
						"\nZ : " + vector.z.ToString("F6") 
						// "\n Z Velocity: " + hand.velocity.z.ToString("F6")
						// "\nZ: " + physicsBody.position.z.ToString("F4") + " " +  initPosition.z.ToString("F4")
						);
    	}

		Vector3 GetRoundedVector3(Vector3 vector){
			Vector3 newVector;
			
			newVector.x = (float)System.Math.Round((double)vector.x / 10, 5) * 10;
			newVector.y = (float)System.Math.Round((double)vector.y / 10, 5) * 10;
			newVector.z = (float)System.Math.Round((double)vector.z / 10, 5) * 10;

			return newVector;
		}

		Vector3 PutBoundaries(Vector3 vector){
			Vector3 newVector;

			newVector.x = Mathf.Abs(vector.x) > bound ? bound : vector.x;
			newVector.y = Mathf.Abs(vector.y) > bound ? bound : vector.y;
			newVector.z = Mathf.Abs(vector.z) > bound ? bound : vector.z;

			return newVector;
		}
		#endregion
	}
}
