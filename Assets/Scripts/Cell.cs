using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [Serializable]
    public class Cell
    {
        [FormerlySerializedAs("StartVertex")] public GameObject startVertex;
        [FormerlySerializedAs("InnerVertices")] public List<InnerVertex> innerVertices;
    }
}