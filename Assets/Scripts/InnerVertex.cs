using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [Serializable]
    public class InnerVertex
    {
        [FormerlySerializedAs("InnerTop")] public GameObject innerTop;
        [FormerlySerializedAs("Path")] public List<GameObject> path;
    }
}