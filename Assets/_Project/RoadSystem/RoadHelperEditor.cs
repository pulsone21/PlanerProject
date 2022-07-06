using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace RoadSystem
{
    public class RoadHelperEditor : EditorWindow
    {
        public RoadNetwork roadNetwork;
        private Road[] roads = new Road[0];
        private int missingRefs = 0;
        public List<RoadNode> NotMergedNodes = new List<RoadNode>();
        private Vector2 scrollPos;


        [MenuItem("PlanerProject/RoadSystem/RoadHelper")]
        private static void ShowWindow()
        {
            var window = GetWindow<RoadHelperEditor>();
            window.titleContent = new GUIContent("RoadHelper");
            window.Show();
        }

        private void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            SerializedObject obj = new SerializedObject(this);

            if (GUILayout.Button("Find all Roads"))
            {
                roads = FindObjectsOfType<Road>();
            }
            GUILayout.Space(5f);

            if (roads != null && roads.Length > 0)
            {
                GUILayout.Label("Found " + roads.Length + " Roads");
                if (GUILayout.Button("Remap Nodes to Roads")) RemapRoadNodesToRoads();
                GUILayout.Space(5f);
                if (GUILayout.Button("Fix Road Indexes")) FixRoadIndexName();
                GUILayout.Space(5f);
                if (GUILayout.Button("Clear Missing Node Link")) ClearMissingLink();
                GUILayout.Label("Cleared " + missingRefs + " missing reference");
                GUILayout.Space(5f);
                if (GUILayout.Button("Reset RoadNodes Connection")) ResetNodeConnections();
                GUILayout.Space(5f);
                if (GUILayout.Button("Fix RoadNode Connections")) FixRoadNodes();
                GUILayout.Space(5f);
                if (GUILayout.Button("Toogle Road Lines")) ToogleRoadLines();
                GUILayout.Space(5f);
                if (GUILayout.Button("GenerateRoadLines")) GenerateRoadLines();
                GUILayout.Space(5f);
                if (GUILayout.Button("Auto Merge Nodes")) MergeNodes();
                if (NotMergedNodes.Count > 0)
                {
                    EditorGUILayout.PropertyField(obj.FindProperty("NotMergedNodes"));
                }
                GUILayout.Space(5f);
                if (GUILayout.Button("Fix Empty Node Connections")) FixEmptyNodes();
            }
            obj.ApplyModifiedProperties();
            EditorGUILayout.EndScrollView();
        }

        private void FixEmptyNodes()
        {
            RoadNode[] nodes = FindObjectsOfType<RoadNode>();
            foreach (RoadNode currNode in nodes)
            {
                List<RoadNode> newNodes = new List<RoadNode>();

                foreach (RoadNode node in currNode.ConnectedNodes)
                {
                    if (node != null) newNodes.Add(node);
                }
                currNode.ConnectedNodes = newNodes;
            }
        }

        private void MergeNodes()
        {
            NotMergedNodes.Clear();
            RoadNode[] nodes = FindObjectsOfType<RoadNode>();
            List<RoadNode> nodesToDelete = new List<RoadNode>();
            foreach (RoadNode currNode in nodes)
            {
                if (!nodesToDelete.Contains(currNode))
                {
                    List<RoadNode> nearbyNodes = FindCloseNodes(currNode);
                    nodesToDelete.AddRange(MergeNodes(nearbyNodes, currNode));
                }
            }
            Debug.Log(nodesToDelete.Count);
            foreach (RoadNode node in nodesToDelete)
            {
                DestroyImmediate(node.gameObject);
            }
        }

        private List<RoadNode> FindCloseNodes(RoadNode currNode)
        {
            RoadNode[] nodes = FindObjectsOfType<RoadNode>();
            List<RoadNode> newNodes = new List<RoadNode>();

            foreach (RoadNode node in nodes)
            {
                float distance = Vector3.Distance(node.transform.position, currNode.transform.position);
                if (node != currNode && distance <= 0.05f)
                {
                    newNodes.Add(node);
                }
            }
            return newNodes;
        }

        private List<RoadNode> MergeNodes(List<RoadNode> nearbyNodes, RoadNode currNode)
        {
            List<RoadNode> nodesToDelete = new List<RoadNode>();
            /*
                CurrNode bleibt stehen
                currNode gehört NICHT zur anderen Road
            */
            foreach (RoadNode toDelNode in nearbyNodes)
            {
                if (toDelNode.transform.parent.childCount > 2)
                {
                    //currNode wird mit der neuen letzten bzw. ersten Node der Road verlinkt.
                    //Node muss aus den Hashsets der anderen Nodes gelöscht werden
                    foreach (RoadNode connectedNode in toDelNode.ConnectedNodes)
                    {
                        connectedNode.ConnectedNodes.Remove(toDelNode);
                        connectedNode.ConnectedNodes.Add(currNode);
                        currNode.ConnectedNodes.Add(connectedNode);
                    }

                    //toDelNode aus Road entfernen
                    Road road = toDelNode.GetComponentInParent<Road>();
                    road.roadNodes.Remove(toDelNode);

                    //jede Node wird gelöscht
                    nodesToDelete.Add(toDelNode);
                }
                else
                {
                    NotMergedNodes.Add(toDelNode);
                }
            }
            return nodesToDelete;
        }

        private void ToogleRoadLines()
        {
            foreach (Road road in roads)
            {
                LineRenderer lR = road.GetComponent<LineRenderer>();
                lR.enabled = !lR.enabled;
            }
        }

        private void GenerateRoadLines()
        {
            Debug.Log(roads.Length);
            foreach (Road road in roads)
            {
                LineRenderer lR = road.GetComponent<LineRenderer>();
                Debug.Log("found LineRenderer");
                List<Vector3> linePoss = new List<Vector3>();

                for (int i = 0; i < road.roadNodes.Count; i++)
                {
                    Debug.Log(road.roadNodes[i].transform.position);
                    linePoss.Add(road.roadNodes[i].transform.position);
                }
                Debug.Log("linePos count: " + linePoss.Count);
                lR.startColor = Color.red;
                lR.endColor = Color.red;
                lR.startWidth = 0.1f;
                lR.endWidth = 0.1f;
                lR.positionCount = linePoss.Count;
                lR.SetPositions(linePoss.ToArray());
            }
        }

        private void FixRoadIndexName()
        {
            RoadNetwork rN = FindObjectOfType<RoadNetwork>();

            for (int i = 0; i < rN.transform.childCount; i++)
            {
                GameObject road = rN.transform.GetChild(i).gameObject;
                road.name = "Road " + i;
            }
        }

        private void RemapRoadNodesToRoads()
        {
            foreach (Road road in roads)
            {
                List<RoadNode> newNodes = new List<RoadNode>();

                for (int i = 0; i < road.transform.childCount; i++)
                {
                    RoadNode node = road.transform.GetChild(i).GetComponent<RoadNode>();
                    node.name = "RoadNode " + i;
                    newNodes.Add(node);
                }
                road.roadNodes = newNodes;
            }
        }

        private void ClearMissingLink()
        {
            foreach (Road road in roads)
            {
                List<RoadNode> newNodes = new List<RoadNode>();

                for (int i = 0; i < road.roadNodes.Count - 1; i++)
                {
                    RoadNode node = road.roadNodes[i];
                    if (node != null)
                    {
                        newNodes.Add(node);
                    }
                    else
                    {
                        missingRefs++;
                    }
                }
                road.roadNodes = newNodes;
            }
        }

        private void ResetNodeConnections()
        {
            foreach (Road road in roads)
            {
                foreach (RoadNode node in road.roadNodes)
                {
                    node.ConnectedNodes = new List<RoadNode>();
                }
            }
        }

        private void FixRoadNodes()
        {
            foreach (Road road in roads)
            {
                Debug.Log(road.name);
                for (int i = road.roadNodes.Count; i >= 2; i--)
                {
                    RoadNode currNode = road.roadNodes[i - 1];
                    RoadNode prevNode = road.roadNodes[i - 2];
                    currNode.ConnectedNodes.Add(prevNode);
                    prevNode.ConnectedNodes.Add(currNode);
                }
            }
        }
    }
}
