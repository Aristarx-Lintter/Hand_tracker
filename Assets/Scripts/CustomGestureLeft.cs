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

namespace Wave.Essence.Hand.Model.Demo
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Text))]
	sealed class CustomGestureLeft : MonoBehaviour
	{
		// const string LOG_TAG = "Wave.Essence.Hand.Model.Demo.StaticGestureText";
		// private void DEBUG(string msg)
		// {
		// 	if (Log.EnableDebugLog)
		// 		Log.d(LOG_TAG, m_Hand + ", " + msg, true);
		// }

		// [SerializeField]
		private HandManager.HandType m_Hand = HandManager.HandType.Left;
		// public HandManager.HandType Hand { get { return m_Hand; } set { m_Hand = value; } }

		// private Text m_Text = null;
        private Rigidbody physicsBody;
        private float turn;

		#region MonoBehaviour Overrides
		void Start()
		{
			physicsBody = gameObject.GetComponent<Rigidbody>();
		}
		void AddForce_(float turn){
			Vector3 force = Vector3.zero;
			force.x += 1000*turn;
			Debug.Log(force);
			physicsBody.AddRelativeForce(force);
		}
		void FixedUpdate()
		{
			// if (m_Text == null) { return; }

			string gesture = WXRGestureHand.GetSingleHandGesture(m_Hand == HandManager.HandType.Left ? true : false);
			// HandState hs = WXRGestureHand.GetState(m_Hand == HandManager.HandType.Left ? true : false);
			if (m_Hand == HandManager.HandType.Left){
				turn = -1;
			} else {
				turn = 1;
			}
			if (gesture.Contains("Five") || gesture.Contains("ThumbUp")){
				Debug.Log("True");
                AddForce_(turn);
			} 
		}

		// void AddForce_() {
		// 	Vector3 force = Vector3.zero;
		// 	force.x += 1000*turn;
		// 	Debug.Log(force);
		// 	physicsBody.AddRelativeForce(force);
		// }
		#endregion

		private GestureType m_HandGesture = GestureType.Unknown;
		public void OnStaticGesture(GestureType gesture)
		{
			// DEBUG("OnStaticGesture() " + gesture);
			m_HandGesture = gesture;
		}
	}
}
