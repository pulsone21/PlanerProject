using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class Road : MonoBehaviour
    {
        public List<RoadNode> roadNodes = new List<RoadNode>();
        public int maxDriveSpeed;



    }
}
