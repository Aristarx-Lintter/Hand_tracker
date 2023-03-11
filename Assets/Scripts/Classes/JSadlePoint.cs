using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "FeatureJZero", menuName = "FeatureJZero", order = 1)]
    public class JSadlePoint : ScriptableObject
    {
        public float JZero = 0.0f;
        public int kDisturb;
        public Vector3 JZeroPoint = new Vector3(0, 0, 0);
    }