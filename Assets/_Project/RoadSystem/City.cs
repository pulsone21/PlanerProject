using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadSystem
{
    public class City : MonoBehaviour
    {
        public string Name;
        public HashSet<RoadNode> Connections;

    }
}
