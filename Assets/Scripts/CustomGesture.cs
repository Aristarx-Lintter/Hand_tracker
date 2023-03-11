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
	sealed class CustomGesture : MonoBehaviour
	{
        [SerializeField] private Rigidbody physicsBody;
		[SerializeField] private int magnitude = 10;

		
		#region MonoBehaviour Overrides

		void FixedUpdate() {
			string leftGesture = WXRGestureHand.GetSingleHandGesture(true);
			string rightGesture = WXRGestureHand.GetSingleHandGesture(false);

			ApplyGesture(leftGesture, -1);
			ApplyGesture(rightGesture, 1);
			
		}
		void Update(){
			string leftGesture = WXRGestureHand.GetSingleHandGesture(true);
			string rightGesture = WXRGestureHand.GetSingleHandGesture(false);

			if (leftGesture.Contains("Rock") && rightGesture.Contains("Rock")){
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}

		void ApplyGesture(string gesture, int turn){
			if (gesture.Contains("ThumbUp")){
				AddForce_(turn);
			}
		}
		void AddForce_(int turn){
			physicsBody.AddForce(transform.right*magnitude*turn);
		}
		#endregion
	}
}
