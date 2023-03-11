using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsPuncher : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBodyU;
    [SerializeField] Rigidbody rigidBodyF;

    float timer = 10;
    int koef = 0;
    int updateCountF = 0;

    private void Update()
    {
        if (timer<Time.time) {
            koef = 1;
        }
        else {
            koef = -1;
        }
        rigidBodyU.AddForceAtPosition(koef*Vector3.forward*10, Vector3.zero, ForceMode.Force);
        Debug.Log(Time.time);
    }

    private void FixedUpdate()
    {
        rigidBodyF.AddForceAtPosition(Vector3.forward*10, Vector3.zero, ForceMode.Force);
        Debug.Log(Time.fixedTime);
    }

}
