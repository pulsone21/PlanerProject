using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadSystem
{
    public class Road : MonoBehaviour
    {

        public List<RoadNode> roadNodes = new List<RoadNode>();
        [SerializeField] private RoadSegment _roadSegment;
        [SerializeField] private int _maxDriveSpeed;

        public RoadSegment RoadSegment => _roadSegment;
        public int MaxDriveSpeed => _maxDriveSpeed;

        public void SetSegment(RoadSegment segment) => _roadSegment = segment;




    }
}
