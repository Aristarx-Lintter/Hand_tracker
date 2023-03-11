using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SomeScript : MonoBehaviour
{
    [SerializeField] TextAsset ourText;
    [SerializeField] string ourFileText;
    // Start is called before the first frame update
    void Start()
    {
        string InternalText = ourText.text;
        string[] InternalStrings = InternalText.Split('\n');

        for (int i=0; i < InternalStrings.Length; i++){
            Debug.Log(InternalStrings[i]);
            string[] values = InternalStrings[i].Split(' ');

            float numbers = 0.0f;

            foreach(var curStr in values){
                // Debug.Log(curStr);
                if (float.TryParse(curStr, out float param)){
                    numbers += param;
                }
                
            }
            Debug.Log(numbers);
        }
        
        string path = Application.dataPath + '/';
        StreamReader streamReader = new StreamReader(path + ourFileText);
        
        Debug.Log("Iterator run!");
        while (streamReader.EndOfStream == false){
            Debug.Log(streamReader.ReadLine());
        }
                       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
