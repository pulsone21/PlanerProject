using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Pathfinding;
namespace RoadSystem
{
    public class RoadNetwork : WaypointManager
    {
        private List<Road> Roads;

        private void Awake()
        {
            // for (int i = 0; i < transform.childCount; i++) Roads.Add(transform.GetChild(i).GetComponent<Road>());
        }



    }
}
