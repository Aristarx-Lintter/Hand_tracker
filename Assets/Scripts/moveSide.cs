using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class moveSide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
        Debug.Log(context.ReadValue<float>());

    }
}