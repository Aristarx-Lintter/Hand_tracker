using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    [SerializeField] JResult j_result;
    [SerializeField] JSadlePoint j_Sadle_Point;
    [SerializeField] Text text;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        text.text = "Score: " + j_result.J_res.ToString("F2");
    }
}
