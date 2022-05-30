using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
namespace RoadSystem
{
    public class RoadNode : MonoBehaviour
    {
        public List<RoadNode> ConnectedNodes = new List<RoadNode>();
        public bool ShowNodeConnections = false;
        private void Awake()
        {

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (RoadNode node in ConnectedNodes)
            {
                Gizmos.DrawLine(node.transform.position, transform.position);
            }

        }


    }
}
