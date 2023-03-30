using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Pathfinding;
using Sirenix.OdinInspector;
namespace RoadSystem
{
    public class RoadNode : Waypoint
    {

        public List<RoadNode> ConnectedNodes = new List<RoadNode>();

        private void Awake()
        {

        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (RoadNode node in ConnectedNodes)
            {
                Gizmos.DrawLine(node.transform.position, transform.position);
            }
        }

        [Button("Show WorldPos")]
        private void DebugWorldPos()
        {
            Debug.Log(transform.position);
        }

        [Button("Show LocalPos")]
        private void DebugLocalPos()
        {
            Debug.Log(transform.localPosition);
        }


    }
}
