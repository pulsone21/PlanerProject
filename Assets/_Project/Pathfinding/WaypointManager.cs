using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Pathfinding
{
    public class WaypointManager : MonoBehaviour
    {
        public static WaypointManager Instance { get; protected set; }
        [SerializeField] public List<Waypoint> Waypoints = new List<Waypoint>();

        void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }
        }

        protected void Start()
        {
            if (Waypoints.Count == 0) Waypoints = FindObjectsOfType<Waypoint>().ToList();
        }

        public Waypoint GetNearestWaypoint(Vector3 position)
        {
            Waypoint outWaypoint = Waypoints[0];
            float currentDist = float.MaxValue;
            foreach (Waypoint waypoint in Waypoints)
            {
                float newDistance = Vector3.Distance(position, waypoint.Position);
                if (currentDist > newDistance)
                {
                    outWaypoint = waypoint;
                    currentDist = newDistance;
                }
            }
            return outWaypoint;
        }

    }
}
