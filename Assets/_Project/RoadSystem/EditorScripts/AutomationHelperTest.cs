using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Utilities;
using System;

namespace RoadSystem
{
    public class AutomationHelperTest : EditorWindow
    {
        List<RoadNode> nodeToLookAt;
        List<RoadNode> nodeLookedAt;
        string resultText;
        public GameObject roadPrefab;

        private MapVisualController.MapMode mapMode = MapVisualController.MapMode.day;

        public GameObject RoadNodeParent;
        public GameObject RoadSegmentParent;


        [MenuItem("PlanerProject/AutomationHelperTest")]
        private static void ShowWindow()
        {
            var window = GetWindow<AutomationHelperTest>();
            window.titleContent = new GUIContent("AutomationHelperTest");
            window.Show();
        }

        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);
            EditorGUILayout.PropertyField(obj.FindProperty("roadPrefab"));
            EditorGUILayout.PropertyField(obj.FindProperty("RoadNodeParent"));
            EditorGUILayout.PropertyField(obj.FindProperty("RoadSegmentParent"));


            if (GUILayout.Button("Generate Segments")) FindRoadSegments();

            if (GUILayout.Button("Fix Connections")) FixRoadConnections();

            if (GUILayout.Button("Reassign RoadNodes")) ReassignRoadNodes();

            if (GUILayout.Button("Rework RoadNodes into Waypoint List")) FixWaypointLists();

            // if (GUILayout.Button("SnapCityToNode")) SnapCityToNode();

            if (GUILayout.Button("ToogleSegmentsMode")) ToogleSegmentsMode();
            if (GUILayout.Button("BakeMesh")) BakeMeshFromLineRenderer();
            if (GUILayout.Button("CleanUp LRs")) CleanUpLineRenderer();




            GUILayout.Space(25f);
            GUILayout.Label("Automation Feedback");
            GUILayout.Space(10f);
            GUILayout.TextArea(resultText);

            obj.ApplyModifiedProperties();
        }

        private void ReassignRoadNodes()
        {
            RoadNode[] nodes = FindObjectsOfType<RoadNode>();
            foreach (RoadNode node in nodes)
            {
                node.transform.SetParent(RoadNodeParent.transform);
            }
        }

        private void FixRoadConnections()
        {
            int fixedConnections = 0;
            RoadNode[] nodes = FindObjectsOfType<RoadNode>();
            foreach (RoadNode node in nodes)
            {
                List<RoadNode> newList = new List<RoadNode>();
                foreach (RoadNode connectedNode in node.ConnectedNodes)
                {
                    if (connectedNode == null) continue;
                    if (!connectedNode.ConnectedNodes.Contains(node))
                    {
                        connectedNode.ConnectedNodes.Add(node);
                        fixedConnections++;
                    }
                    newList.Add(connectedNode);
                }
                node.ConnectedNodes = newList;
                node.ConnectedWaypoints.Clear();
                node.ConnectedWaypoints.AddRange(newList);
            }
            SetFeedbackText($"Fixed {fixedConnections}");
        }


        private void BakeMeshFromLineRenderer()
        {
            LineRenderer[] lrS = RoadSegmentParent.GetComponentsInChildren<LineRenderer>();

            CombineInstance[] ciS = new CombineInstance[lrS.Length];
            for (int i = 0; i < lrS.Length; i++)
            {
                Mesh mesh = new Mesh();
                lrS[i].BakeMesh(mesh);
                ciS[i].mesh = mesh;
                ciS[i].transform = lrS[i].transform.localToWorldMatrix;
                lrS[i].gameObject.SetActive(false);
            }

            MeshFilter mf = RoadSegmentParent.GetComponent<MeshFilter>();
            Mesh roadSegments = new Mesh();
            roadSegments.name = "RadSegements";
            roadSegments.CombineMeshes(ciS, true, true);
            mf.sharedMesh = roadSegments;
            AssetDatabase.CreateAsset(roadSegments, "Assets/RoadSegments.asset");
            AssetDatabase.SaveAssets();
        }

        private void CleanUpLineRenderer()
        {
            Debug.Log("CleanUP Line Renderer");
            int childCount = RoadSegmentParent.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform currChild = RoadSegmentParent.transform.GetChild(i);
                currChild.gameObject.SetActive(true);
                DestroyImmediate(currChild.GetComponent<LineRenderer>());
                DestroyImmediate(currChild.GetComponent<MeshRenderer>());
            }
            Debug.Log("LineRenderer Cleaned Up");
        }
        private void FindRoadSegments()
        {
            RoadNode[] nodes = FindObjectsOfType<RoadNode>();
            nodeToLookAt = new List<RoadNode>();
            nodeLookedAt = new List<RoadNode>();
            List<RoadSegment> segments = new List<RoadSegment>();

            foreach (RoadNode node in nodes)
            {
                if (node.ConnectedNodes.Count > 2) nodeToLookAt.Add(node);
            }

            while (nodeToLookAt.Count > 0)
            {
                RoadNode startNode = nodeToLookAt[0];
                nodeToLookAt.Remove(startNode);
                segments.AddRange(CreateRoadSegments(startNode));
                nodeLookedAt.Add(startNode);
            }
            int i = 0;
            foreach (RoadSegment segment in segments)
            {
                int segmentLength = segment.RoadNodes.Length;
                GameObject go = Instantiate(roadPrefab, Vector3.zero, Quaternion.identity);
                go.name = $"RoadSegment_{i}_{segment.RoadNodes[0].transform.position}-to-{segment.RoadNodes[segmentLength - 1].transform.position}";
                go.transform.SetParent(RoadSegmentParent.transform);
                LineRenderer lR = go.GetComponent<LineRenderer>();
                lR.positionCount = segmentLength;
                lR.SetPositions(segment.Positions);
                i++;
            }
        }

        private List<RoadSegment> CreateRoadSegments(RoadNode startNode)
        {

            List<RoadSegment> segments = new List<RoadSegment>();
            foreach (RoadNode currNode in startNode.ConnectedNodes)
            {
                if (nodeLookedAt.Contains(currNode)) continue;


                List<RoadNode> positions = new List<RoadNode>();
                positions.Add(startNode);
                positions.Add(currNode);

                //if 2 junktions are connected directly we want to stop imediately
                if (currNode.ConnectedNodes.Count <= 2)
                {
                    nodeLookedAt.Add(currNode);
                    GetConnectedRecursivly(currNode, ref positions);
                }
                segments.Add(new RoadSegment(positions.ToArray()));
            }
            return segments;
        }

        private void GetConnectedRecursivly(RoadNode node, ref List<RoadNode> positions)
        {
            foreach (RoadNode nextNode in node.ConnectedNodes)
            {
                if (nextNode == node) continue;
                if (nodeLookedAt.Contains(nextNode)) continue;
                if (positions.Contains(nextNode)) continue;
                if (nextNode.ConnectedNodes.Count > 2)
                {
                    positions.Add(nextNode);
                    return;
                }
                else
                {
                    positions.Add(nextNode);
                    nodeLookedAt.Add(nextNode);
                    GetConnectedRecursivly(nextNode, ref positions);
                }
            }
        }


        private void FixWaypointLists()
        {
            RoadNode[] nodes = FindObjectsOfType<RoadNode>();
            foreach (RoadNode node in nodes)
            {
                node.ConnectedWaypoints.AddRange(node.ConnectedNodes);
            }
        }

        /*
        private void SnapCityToNode()
        {
            CityController[] cities = FindObjectsOfType<CityController>();
            RoadNode[] nodes = FindObjectsOfType<RoadNode>();

            foreach (CityController cC in cities)
            {
                cC.City.ClearConnection();
                RoadNode currNode = nodes[0];
                float currentDistance = Vector3.Distance(currNode.Position, cC.transform.position);
                foreach (RoadNode node in nodes)
                {
                    float newDist = Vector3.Distance(node.Position, cC.transform.position);
                    if (newDist < currentDistance)
                    {
                        currentDistance = newDist;
                        currNode = node;
                    }
                }
                cC.City.AddConnection(currNode);
                cC.transform.position = currNode.Position;
            }
        }
        */

        private void ToogleSegmentsMode()
        {
            RoadVisualController[] rvcS = FindObjectsOfType<RoadVisualController>();
            if (mapMode == MapVisualController.MapMode.day)
            {
                mapMode = MapVisualController.MapMode.night;
            }
            else
            {
                mapMode = MapVisualController.MapMode.day;
            }
            foreach (RoadVisualController rvc in rvcS)
            {
                rvc.ToogleMode(mapMode);
            }
        }

        private void ClearAutomationText() => resultText = "";
        private void SetFeedbackText(string text) => resultText = text;
    }
}
