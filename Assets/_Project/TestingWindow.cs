using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using CompanySystem;
using ContractSystem;


namespace Planer
{
    public class TestingWindow : EditorWindow
    {

        [MenuItem("PlanerProject/Testing")]
        private static void ShowWindow()
        {
            var window = GetWindow<TestingWindow>();
            window.titleContent = new GUIContent("Testing");
            window.Show();
        }

        public List<TransportGood> Goods;
        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);
            EditorGUILayout.PropertyField(obj.FindProperty("Goods"));

            if (GUILayout.Button("Serilize"))
            {
                Serialize();
            }

            obj.ApplyModifiedProperties();
        }

        private void Serialize()
        {
            foreach (TransportGood item in Goods)
            {
                Debug.Log(JsonUtility.ToJson(item));
            }
        }
    }
}
