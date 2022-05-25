using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace RoadSystem
{
    public class RoadCreator : EditorWindow
    {
        public static RoadCreator Instance;
        [MenuItem("PlanerProject/RoadCreator")]
        private static void ShowWindow()
        {
            if (Instance != null) return;
            Instance = GetWindow<RoadCreator>();
            Instance.titleContent = new GUIContent("RoadCreator");
            Instance.Show();
        }

        public Transform roadNetworkRoot;


        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);
            EditorGUILayout.PropertyField(obj.FindProperty("roadNetworkRoot"));

            if (roadNetworkRoot == null)
            {
                EditorGUILayout.HelpBox("Root GameObject not assigned, please assaign.", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.BeginVertical("box");
                DrawButtons();
                ShowNodeList();
                EditorGUILayout.EndVertical();
            }

            obj.ApplyModifiedProperties();
        }

        private void ShowNodeList()
        {
            for (int i = 0; i < roadNetworkRoot.childCount; i++)
            {
                GameObject currGo = roadNetworkRoot.GetChild(i).gameObject;
                EditorGUILayout.BeginHorizontal("box");
                GUILayout.Label(currGo.name);

                if (GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                {
                    Selection.activeGameObject = currGo;
                }

                if (GUILayout.Button("-"))
                {
                    RemoveNode(currGo.GetComponent<RoadNode>());
                }
                EditorGUILayout.EndHorizontal();
            }
        }



        private void RemoveNode(RoadNode node, bool reconect = true)
        {
            foreach (RoadNode inComing in node.InComingNodes)
            {
                inComing.OutGoingNodes.Remove(node);
            }

            foreach (RoadNode outGoing in node.OutGoingNodes)
            {
                outGoing.InComingNodes.Remove(node);
            }
            DestroyImmediate(node.gameObject);
            RenameNodes();
            if (reconect) ReConnectNodes();
        }

        private void ReConnectNodes()
        {
            RoadNode prevNode = roadNetworkRoot.GetChild(0).GetComponent<RoadNode>();
            prevNode.InComingNodes.Clear();
            prevNode.OutGoingNodes.Clear();
            for (int i = 1; i < roadNetworkRoot.childCount; i++)
            {
                RoadNode currNode = roadNetworkRoot.GetChild(i).GetComponent<RoadNode>();
                currNode.InComingNodes.Clear();
                currNode.OutGoingNodes.Clear();
                currNode.InComingNodes.Add(prevNode);
                prevNode.OutGoingNodes.Add(currNode);
                prevNode = currNode;
            }
        }

        private void RenameNodes()
        {
            for (int i = 0; i < roadNetworkRoot.childCount; i++)
            {
                GameObject go = roadNetworkRoot.GetChild(i).gameObject;
                go.name = "Node " + i;
            }
        }

        private void DrawButtons()
        {
            EditorGUILayout.BeginVertical("box");
            if (GUILayout.Button("Create new Node"))
            {
                CreateNode(Vector3.zero);
            }

            if (GUILayout.Button("DeleteList"))
            {
                int i = roadNetworkRoot.childCount;

                while (i > 0)
                {
                    GameObject go = roadNetworkRoot.GetChild(i - 1).gameObject;
                    RemoveNode(go.GetComponent<RoadNode>(), false);
                    DestroyImmediate(go);
                    i = roadNetworkRoot.childCount;
                }
            }

            if (GUILayout.Button("Reconnect Waypoints"))
            {
                ReConnectNodes();
            }

            EditorGUILayout.EndHorizontal();
        }

        public void CreateNode(Vector3 postion)
        {
            GameObject go = new GameObject("Node " + roadNetworkRoot.childCount, typeof(RoadNode));
            go.transform.SetParent(roadNetworkRoot, false);
            go.transform.position = postion;
            var iconContet = EditorGUIUtility.IconContent("d_orangeLight");

            EditorGUIUtility.SetIconForObject(go, (Texture2D)iconContet.image);
            RoadNode currNode = go.GetComponent<RoadNode>();
            if (roadNetworkRoot.childCount > 1)
            {
                RoadNode prevNode = roadNetworkRoot.GetChild((roadNetworkRoot.childCount - 2)).GetComponent<RoadNode>();
                currNode.InComingNodes.Add(prevNode);
                prevNode.OutGoingNodes.Add(currNode);
            }
            Selection.activeGameObject = currNode.gameObject;
        }
    }
}
