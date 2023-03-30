using System.Collections.Generic;
using UnityEngine;


namespace Pathfinding
{
    public class AstarPath
    {
        public static int MOVEMENT_COST_TILE = 10;

        protected Vector3 m_startPosition;
        protected Vector3 m_endPosition;
        protected float m_rawDistance;
        protected float m_accumulatedDistance;

        private Waypoint startWaypoint;
        private Waypoint endWaypoint;
        public Waypoint StartWaypoint => startWaypoint;
        public Waypoint EndWaypoint => endWaypoint;

        public Path Path { get; protected set; }

        public AstarPath(Vector3 _startPosition, Vector3 _endPosition)
        {
            Debug.Log($"Creating Path from {_startPosition}, to {_endPosition}");
            m_startPosition = _startPosition;
            m_endPosition = _endPosition;
            startWaypoint = WaypointManager.Instance.GetNearestWaypoint(_startPosition);
            endWaypoint = WaypointManager.Instance.GetNearestWaypoint(_endPosition);
            this.Path = FindPath(startWaypoint, endWaypoint);
        }

        public AstarPath(Waypoint _startWaypoint, Waypoint _endWaypoint)
        {
            m_startPosition = _startWaypoint.Position;
            m_endPosition = _endWaypoint.Position;
            startWaypoint = _startWaypoint;
            endWaypoint = _endWaypoint;
            this.Path = FindPath(startWaypoint, endWaypoint);
        }

        protected Path FindPath(Waypoint startWaypoint, Waypoint endWaypoint)
        {
            // Debug.Log($"Starting Pathfinding from {startNode.ToString()} to {endNode.ToString()}, with PathMode {pM}");
            List<Waypoint> openList = new List<Waypoint> { startWaypoint };
            List<Waypoint> closedList = new List<Waypoint>();
            List<Waypoint> ListToSearch = WaypointManager.Instance.Waypoints;

            foreach (Waypoint Waypoint in ListToSearch)
            {
                Waypoint.gCost = int.MaxValue;
                Waypoint.CalculateFCost();
                Waypoint.cameFromWaypoint = null;
            }

            startWaypoint.gCost = 0;
            startWaypoint.hCost = CalculateDistanceCost(startWaypoint, endWaypoint);
            startWaypoint.CalculateFCost();

            while (openList.Count > 0)
            {
                Waypoint currentWaypoint = GetLowestFCostNode(openList);
                // Debug.Log("Current Node is: " + currentNode.ToString());
                if (currentWaypoint == endWaypoint)
                {
                    return new Path(CalculatedPath(currentWaypoint));
                }
                openList.Remove(currentWaypoint);
                closedList.Add(currentWaypoint);
                // Debug.Log("Removed Node: " + currentNode.ToString() + "from openList & added to Closedlist.");
                // Debug.Log("Starting to check the neighbors");
                List<Waypoint> listToSearch = currentWaypoint.ConnectedWaypoints;

                foreach (Waypoint neighborNode in listToSearch)
                {
                    // Debug.Log("Checking " + neighborNode.ToString());
                    if (closedList.Contains(neighborNode))
                    {
                        // Debug.Log("NeighborNode is already in closedList, starting new");
                        continue;
                    }

                    int tentativeGCost = currentWaypoint.gCost + CalculateDistanceCost(currentWaypoint, neighborNode);
                    // Debug.Log("TentaiveGCost = " + tentativeGCost + ", neighborNode.gCost = " + neighborNode.gCost);
                    if (tentativeGCost < neighborNode.gCost)
                    {
                        neighborNode.cameFromWaypoint = currentWaypoint;
                        neighborNode.gCost = tentativeGCost;
                        neighborNode.hCost = CalculateDistanceCost(neighborNode, endWaypoint);
                        neighborNode.CalculateFCost();

                        if (!openList.Contains(neighborNode))
                        {
                            openList.Add(neighborNode);
                        }
                    }
                }
                // Debug.Log("ForEach run, " + openList.Count + " Nodes left to search.");
            }
            Debug.LogError("No Path found!"); //TODO -> dont throw an error, make an PopUp or notification that no Path was found
            return null;
        }

        private Queue<Vector3> CalculatedPath(Waypoint _endNode)
        {
            List<Waypoint> _pathList = new List<Waypoint>();
            _pathList.Add(_endNode);
            Waypoint _currentNode = _endNode;
            while (_currentNode.cameFromWaypoint != null)
            {
                _pathList.Add(_currentNode.cameFromWaypoint);
                _currentNode = _currentNode.cameFromWaypoint;
            }
            _pathList.Reverse();
            Queue<Vector3> _path = new Queue<Vector3>();
            foreach (Waypoint wP in _pathList)
            {
                _path.Enqueue(wP.Position);
            }
            _path.Enqueue(m_endPosition);
            return _path;
        }

        private int CalculateDistanceCost(Waypoint startNode, Waypoint endNode)
        {
            return Mathf.FloorToInt(MOVEMENT_COST_TILE * Waypoint.DistanceToWaypoint(startNode, endNode));
        }

        private Waypoint GetLowestFCostNode(List<Waypoint> pathWaypoints)
        {
            Waypoint lowestFCostNode = pathWaypoints[0];
            foreach (Waypoint waypoint in pathWaypoints)
            {
                if (waypoint.fCost < lowestFCostNode.fCost)
                {
                    lowestFCostNode = waypoint;
                }
            }
            return lowestFCostNode;
        }
    }

}
