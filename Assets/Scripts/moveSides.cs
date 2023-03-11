using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class moveSides : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody physicsBody;
    float turn = 0.0f;
    
    void Start()
    {
        turn = 0.0f;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        turn = context.ReadValue<float>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 force = Vector3.zero;
        force.x += 1000*turn;
        Debug.Log(force);
        physicsBody.AddForce(force);
    }
}
