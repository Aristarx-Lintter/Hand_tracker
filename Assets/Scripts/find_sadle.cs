using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Networking.Types;

public class find_sadle : MonoBehaviour
{
    [SerializeField] Rigidbody physicsBody;
    [SerializeField] float FrontalForce = 20f, time = 0f, scale = 10.0f;
    [SerializeField] int k = 0;
    [SerializeField] JSadlePoint sadlePoint_;

    AllElementsEventVector eventVector = new AllElementsEventVector();
    
    private void Start()
    {
        physicsBody.inertiaTensor = new Vector3(12.5f, 12.5f, 12.5f);
        physicsBody.position = new Vector3(eventVector.x0, 0, eventVector.z0);
    }

    private void FixedUpdate() {
        physicsBody.isKinematic = false;
        time += Time.deltaTime;
        Time.timeScale = scale;
        if (k != eventVector.allFirstStepDeltaMoment.Length){
            if (time < 20) {
                FrontalForce = 20f;
                Vector3 total_Force = transform.forward * FrontalForce + transform.forward * eventVector.allFirstStepDeltaFrontalForce[k];
                physicsBody.AddTorque(transform.up * eventVector.allFirstStepDeltaMoment[k]);
                Debug.Log((total_Force).ToString());
                physicsBody.AddForce(total_Force);
            }
            else if (time < 40) {
                
            }
            else if (time < 60) {
                FrontalForce = -20f;
                Vector3 total_Force = transform.forward * FrontalForce + transform.forward * eventVector.allLastStepDeltaFrontalForce[k];
                physicsBody.AddForce(total_Force);
                physicsBody.AddTorque(transform.up * eventVector.allLastStepDeltaMoment[k]);
            }
            else {
                physicsBody.isKinematic = true;                
                eventVector.position[k] = physicsBody.position;
                eventVector.allPoints_z[k] = physicsBody.position.z;

                physicsBody.position = new Vector3(eventVector.x0, 0, eventVector.z0);
                physicsBody.rotation = Quaternion.Euler(0, 0, 0);
                time = 0f;
                k += 1;
#if UNITY_EDITOR //only just for use it in a VR helmet
            if (k == 4) {
                UnityEditor.EditorApplication.isPlaying = false; 
            }
#endif
            }
        }
    }
    private void OnDestroy()
    {
        
        float J_zero = 0;
        Vector3 sadlePoint = Vector3.positiveInfinity;
        float maxLen = 0f;
        float newMaxLen;
        float radius = 0f;
        k = 0;

        foreach (Vector3 positionVector in eventVector.position) {
            Debug.Log("PositionVector    x: " + positionVector.x.ToString("F8") + "       z: " + positionVector.z.ToString("F8"));
            Debug.Log("Sadle    x: " + sadlePoint.x.ToString("F8") + "       z: " + sadlePoint.z.ToString("F8"));
            if (MathF.Abs(positionVector.x) < 32) {
                if (maxLen < MathF.Abs(positionVector.z)) {
                    maxLen = MathF.Abs(positionVector.z);
                    sadlePoint = positionVector;
                    k++;
                  }
            }
            else {
                newMaxLen = MathF.Pow(Math.Abs(positionVector.x) - 32f, 2) + MathF.Pow(Math.Abs(positionVector.z), 2);
                if (maxLen < newMaxLen) {
                    maxLen = newMaxLen;
                    sadlePoint = positionVector;
                    k++;
                }
            }
            radius = maxLen;
        }
        if (sadlePoint == Vector3.positiveInfinity){
            Debug.Log("Point doesn't exist");
        }
        int m = 0;
        foreach (Vector3 positionVector in eventVector.position) {
            float line;
            if (MathF.Abs(sadlePoint.x) < 32){
                
                line = MathF.Pow((positionVector.x - sadlePoint.x), 2) + MathF.Pow((positionVector.z), 2);
            }
            else{
                line = MathF.Pow((Math.Abs(positionVector.x) - 32), 2) + MathF.Pow((positionVector.z), 2);
            }
            if (line <= MathF.Pow(radius, 2) && positionVector != sadlePoint){
                m += 1;
            }
            if (m == eventVector.position.Length - 1) { // The count of points in circle need to be without null and sadle.
                J_zero = radius;
            }
        }
        sadlePoint_.JZero = J_zero;
        sadlePoint_.JZeroPoint = sadlePoint;
        sadlePoint_.kDisturb = k;
    }

    public class AllElementsEventVector {
        public float[] allFirstStepDeltaMoment, allLastStepDeltaMoment;
        public float[] allFirstStepDeltaFrontalForce, allLastStepDeltaFrontalForce;
        public float[] allPoints_x, allPoints_z, allPoints_len;
        public float x0, z0;
        public Vector3[] position;

        public AllElementsEventVector(){
            allFirstStepDeltaMoment = new float[4] { 0.004f, 0.004f, 0.0042f, 0.0041f };
            allLastStepDeltaMoment = new float[4] { -0.005f, 0, 0, 0 };

            allFirstStepDeltaFrontalForce = new float[4] { 0, 0, -0.03f, 0 };
            allLastStepDeltaFrontalForce = new float[4] { 0.2f, -0.5f, 0, 0.2f };
            position = new Vector3[4]{  new Vector3(0, 0, 0), 
                                        new Vector3(0, 0, 0), 
                                        new Vector3(0, 0, 0), 
                                        new Vector3(0, 0, 0)};
            allPoints_x = new float[4] {0,0,0,0};
            allPoints_z = new float[4] {0,0,0,0};
            allPoints_len = new float[4] {0,0,0,0};
            x0 = -20f;
            z0 = -71f;
        }
    }
}

