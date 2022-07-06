using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pathfinding
{
    public class Waypoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Waypoint cameFromWaypoint;

        public List<Waypoint> ConnectedWaypoints = new List<Waypoint>();

        public int gCost;
        public int hCost;
        public int fCost;

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }

        public static float DistanceToWaypoint(Waypoint a, Waypoint b)
        {
            int dQ = Mathf.RoundToInt(Mathf.Abs(a.Position.x - b.Position.x));
            int dR = Mathf.RoundToInt(Mathf.Abs(a.Position.z - b.Position.z));
            return
                Mathf.Max(
                    dQ,
                    dR,
                    Mathf.Abs(a.Position.y - b.Position.y)
                );
        }

        // public bool ContainsWaypoint(Waypoint _waypoint);

        public override string ToString()
        {
            return $"Waypoint_{Position.x}_{Position.z}";
        }
    }
}
