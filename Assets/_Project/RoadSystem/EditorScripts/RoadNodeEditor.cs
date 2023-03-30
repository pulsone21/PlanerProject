using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Utilities;

namespace RoadSystem
{
    [CustomEditor(typeof(RoadNode))]
    public class RoadNodeEditor : Editor
    {
        private RoadNode currNode;
        public List<RoadNode> closeByNodes = new List<RoadNode>();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            currNode = (RoadNode)target;


            if (GUILayout.Button("Find Nodes CloseBy"))
            {
                closeByNodes = FindCloseNodes();
            }

            if (closeByNodes.Count > 0)
            {

                if (GUILayout.Button("Merge Nodes"))
                {
                    MergeNodes();
                }

                GUILayout.BeginVertical("box");
                foreach (RoadNode node in closeByNodes)
                {
                    EditorGUILayout.LabelField(node.name + ", " + node.transform.position + ", SiblingCount:  " + node.transform.parent.childCount);
                }
                GUILayout.EndVertical();
            }

            if (GUILayout.Button("Show WorldPos")) Debug.Log(currNode.transform.position);
            if (GUILayout.Button("Show LocalPos")) Debug.Log(currNode.transform.localPosition);

        }

        private List<RoadNode> FindCloseNodes()
        {
            RoadNode[] gOs = FindObjectsOfType<RoadNode>();
            List<RoadNode> nodes = new List<RoadNode>();

            foreach (RoadNode node in gOs)
            {
                float distance = Vector3.Distance(node.transform.position, currNode.transform.position);
                if (node != currNode && distance <= 0.05f)
                {
                    nodes.Add(node);
                }
            }
            return nodes;
        }

        private void MergeNodes()
        {
            /*
                CurrNode bleibt stehen
                currNode gehört NICHT zur anderen Road
            */
            foreach (RoadNode toDelNode in closeByNodes)
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
                    DestroyImmediate(toDelNode.gameObject);
                }
            }
            closeByNodes.Clear();
        }
    }
}
