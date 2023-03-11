using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getHandVelocity : MonoBehaviour
{
    [SerializeField] Rigidbody hand;
    [SerializeField] Text xyz;

    private Vector3 initPosition;
    private Vector3 deltaPos;

    void Start() {
        initPosition = hand.position;
    }
    // void FixedUpdate() {
    //     PrintVector((hand.position - initPosition) / Time.fixedDeltaTime);
    //     initPosition = hand.position;
    // }
    // void PrintVector(Vector3 vector) {
    //     xyz.text = ("X : " + vector.x.ToString("F6") + 
    //                 // "\n X Velocity: " + hand.velocity.x.ToString("F6") +
    //                 // "\nX: " + vector.x.ToString("F6") +
    //                 "\nY : " + vector.y.ToString("F6") + 
    //                 // "\n Y Velocity: " + hand.velocity.y.ToString("F6") + 
    //                 "\nZ : " + vector.z.ToString("F6") 
    //                 // "\n Z Velocity: " + hand.velocity.z.ToString("F6")
    //                 // "\nZ: " + physicsBody.position.z.ToString("F4") + " " +  initPosition.z.ToString("F4")
    //                 );
    // }
}
