using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Networking.Types;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Mover : MonoBehaviour
{
    
    [SerializeField] Rigidbody physicsBody;
    [SerializeField] float FrontalForce = 20f;
    [SerializeField] JSadlePoint j_Sadle_Point;
    [SerializeField] JResult j_result;
    [SerializeField] Text text1, xyz, status, timer;

  
    float[] AlllateralForce = new float[3] {-4f, 0f, 4f}; 

    float[] AllFirstStepDeltaMoment = new float[4] { 0.004f, 0.004f, 0.0042f, 0.0041f };
    float[] allLastStepDeltaMoment = new float[4] { -0.005f, 0, 0, 0 };

    float[] AllFirstStepDeltaFrontalForce = new float[4] { 0, 0, -0.03f, 0 };
    float[] allLastStepDeltaFrontalForce = new float[4] { 0.2f, -0.5f, 0, 0.2f };
    float time = 0;
    int k;

    void Start()
    {
        physicsBody.inertiaTensor = new Vector3(12.5f, 12.5f, 12.5f);
        physicsBody.position = new Vector3(-20.0f, 0, -71);
        int k = j_Sadle_Point.kDisturb;
        time = 0;
        status.text = "STATUS: TESTING";
        physicsBody.isKinematic = false;

    }

    void changeJ()
    {
        float J = MathF.Sqrt(MathF.Pow(physicsBody.position.z, 2) + MathF.Pow(physicsBody.position.x, 2));
        j_result.J_res = 100 * j_Sadle_Point.JZero / J < 100 ? 100 * j_Sadle_Point.JZero / J : 100;
        text1.text += "\tJ REAL\t" + J.ToString("F2");
        xyz.text = ("X: " + physicsBody.position.x.ToString("F2") + 
                    "\nY: " + physicsBody.position.y.ToString("F2") + 
                    "\nZ: " + physicsBody.position.z.ToString("F2"));
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        timer.text = "TIME: " + time.ToString("F3") + " seconds";
        if (time < 20) {
            MathSystemUpdate(transform.up * AllFirstStepDeltaMoment[k], transform.forward * (20f + AllFirstStepDeltaFrontalForce[k]));
        } else if (time < 40) {
            MathSystemUpdate(Vector3.zero, transform.right * AlllateralForce[1]);
        } else if (time < 60) {
            MathSystemUpdate(transform.up * allLastStepDeltaMoment[k], transform.forward * (-20f + allLastStepDeltaFrontalForce[k]));
        } else if (time < 65) {
            physicsBody.isKinematic = true;
            status.text = "STATUS: WAITING";
        } else {
            physicsBody.isKinematic = false;
            status.text = "STATUS: ENJOY!";
        }       
    }

    void MathSystemUpdate(Vector3 torque, Vector3 force){
        physicsBody.AddTorque(torque);
        physicsBody.AddForce(force);
        changeJ();
    }
        
}
