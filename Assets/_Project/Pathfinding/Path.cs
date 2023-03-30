using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    [Serializable]
    public class Path
    {
        private Queue<Vector3> waypoints;
        public Vector3 currentWaypoint;
        public Vector3 nextWaypoint;
        // public List<Vector3> Waypoints = new List<Vector3>(); //! only for debugging...
        public Path(Queue<Vector3> _waypoints)
        {
            this.waypoints = _waypoints;
            // Debug.Log("Creating MoveOrder, Waypoints: " + waypoints.Count);
            // foreach (Vector3 wp in _waypoints) Waypoints.Add(wp); // ! only for debugging
            currentWaypoint = waypoints.Dequeue();
            // Debug.Log("CurrentWaypoint = " + currentWaypoint);
            // Waypoints.Remove(currentWaypoint); // ! only for debuging
            if (waypoints.Count > 1) nextWaypoint = waypoints.Peek();
        }
        public Vector3[] Waypoints => waypoints.ToArray();
        public bool GetNextPosition(out Vector3 outPos)
        {
            if (waypoints.Count > 1)
            {
                // Waypoints.Remove(currentWaypoint); //! Only for debugging
                currentWaypoint = waypoints.Dequeue();
                nextWaypoint = waypoints.Peek();
                outPos = currentWaypoint;
                return true;
            }
            else if (waypoints.Count > 0)
            {
                // Waypoints.Remove(currentWaypoint); //! only for debugging
                currentWaypoint = waypoints.Dequeue();
                nextWaypoint = default;
                outPos = currentWaypoint;
                return true;
            }
            else
            {
                currentWaypoint = Vector3.zero;
                nextWaypoint = Vector3.zero;
                outPos = Vector3.zero;
                return false;
            }
        }
        public float CalculateDistance()
        {
            float calcDist = 0f;
            Vector3[] wps = waypoints.ToArray();
            for (int i = 1; i < wps.Length; i++)
            {
                calcDist += Vector3.Distance(wps[i - 1], wps[i]);
            }
            return calcDist;
        }
    }
}
