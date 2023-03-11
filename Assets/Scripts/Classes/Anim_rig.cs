using UnityEngine.Animations.Rigging;
using UnityEngine;

public class AnimRig : MonoBehaviour
{
    public Rig mobRig;
    public float weightSpeed = 3;
   

    private void FixedUpdate()
    {
         
     mobRig.weight = Mathf.MoveTowards(mobRig.weight*10, 10, weightSpeed * Time.deltaTime);
           

            
    }
}
