using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RoadSystem
{
    public class RoadNetwork : MonoBehaviour
    {
        private List<RoadNode> RoadNodes;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++) RoadNodes.Add(transform.GetChild(i).GetComponent<RoadNode>());
        }


      

        private void OnDrawGizmos()
        {

            foreach (Transform t in transform)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(t.position, 5f);
                RoadNode currNode = t.GetComponent<RoadNode>();
                foreach (RoadNode OutGoinNode in currNode.OutGoingNodes)
                {
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawLine(currNode.transform.position, OutGoinNode.transform.position);
                }
            }
        }

        
    }
}
