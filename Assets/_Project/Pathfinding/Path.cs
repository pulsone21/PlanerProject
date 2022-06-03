using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    [System.Serializable]
    public class Path
    {
        private Queue<Vector3> waypoints;
        public Vector3 currentWaypoint;
        public Vector3 nextWaypoint;
        public List<Vector3> Waypoints = new List<Vector3>(); //! only for debugging...

        public Path(Queue<Vector3> _waypoints)
        {
            this.waypoints = _waypoints;
            // Debug.Log("Creating MoveOrder, Waypoints: " + waypoints.Count);
            foreach (Vector3 wp in _waypoints)
            {
                Waypoints.Add(wp);
            }
            currentWaypoint = waypoints.Dequeue();
            // Debug.Log("CurrentWaypoint = " + currentWaypoint);
            Waypoints.Remove(currentWaypoint);
            if (waypoints.Count > 1) nextWaypoint = waypoints.Peek();
        }

        public bool GetNextPosition(out Vector3 outPos)
        {
            if (waypoints.Count > 1)
            {
                Waypoints.Remove(currentWaypoint);
                currentWaypoint = waypoints.Dequeue();
                nextWaypoint = waypoints.Peek();
                outPos = currentWaypoint;
                return true;
            }
            else if (waypoints.Count > 0)
            {
                Waypoints.Remove(currentWaypoint);
                currentWaypoint = waypoints.Dequeue();
                nextWaypoint = currentWaypoint;
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
    }
}
