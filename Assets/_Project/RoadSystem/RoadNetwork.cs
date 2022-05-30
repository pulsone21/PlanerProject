using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RoadSystem
{
    public class RoadNetwork : MonoBehaviour
    {
        public bool ShowNodeConnections = false;
        public bool ShowRoadConnections = false;
        private List<Road> Roads;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++) Roads.Add(transform.GetChild(i).GetComponent<Road>());
        }







    }
}
