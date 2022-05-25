using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadSystem
{
    public class RoadNode : MonoBehaviour
    {
        public HashSet<RoadNode> InComingNodes = new HashSet<RoadNode>();
        public HashSet<RoadNode> OutGoingNodes = new HashSet<RoadNode>();
    }
}
