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
        public bool ShowRoadConnections = true;
        private LineRenderer lR;

        private void Awake()
        {
            lR = GetComponent<LineRenderer>();
        }

        private void OnDrawGizmosSelected()
        {
            // Gizmos.color = Color.red;
            // for (int i = roadNodes.Count; i >= 2; i--)
            // {
            //     RoadNode currNode = roadNodes[i - 1];
            //     RoadNode prevNode = roadNodes[i - 2];
            //     Gizmos.DrawLine(currNode.transform.position, prevNode.transform.position);
            // }
        }

    }
}
