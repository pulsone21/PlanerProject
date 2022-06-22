using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Utilities;

namespace EmployeeSystem
{

    public class EmployeeTesting : EditorWindow
    {
        public List<Employee> employees = new List<Employee>();
        string outputText;
        [MenuItem("PlanerProject/EmployeeSystem/EmployeeTesting")]
        private static void ShowWindow()
        {
            var window = GetWindow<EmployeeTesting>();
            window.titleContent = new GUIContent("EmployeeTesting");
            window.Show();
        }

        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);
            if (GUILayout.Button("Generate Employees")) GenerateRndAmountEmployees();

            if (employees.Count > 0)
            {
                EditorGUILayout.PropertyField(obj.FindProperty("employees"));
            }

            GUILayout.Space(5);
            GUILayout.Label(outputText);

            obj.ApplyModifiedProperties();
        }

        private void GenerateRndAmountEmployees()
        {
            employees = EmplyoeeGenerator.GenerateEmployees(10);
            outputText = "Generated 10 Emplyoees";
        }
    }
}
