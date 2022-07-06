using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
namespace RoadSystem
{
    public class RoadSegment
    {
        public readonly RoadNode[] RoadNodes;
        public readonly Vector3[] Positions;

        public RoadSegment(RoadNode[] positions)
        {
            this.RoadNodes = positions;
            this.Positions = new Vector3[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                RoadNode node = positions[i];
                Positions[i] = node.transform.position;
            }
        }
    }
}
