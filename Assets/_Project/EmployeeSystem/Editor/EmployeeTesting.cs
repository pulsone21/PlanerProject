using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Utilities;
using MailSystem;
using TimeSystem;

namespace EmployeeSystem
{

    public class EmployeeTesting : EditorWindow
    {
        public Employee employee;
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
            EditorGUILayout.PropertyField(obj.FindProperty("employee"));

            if (GUILayout.Button("Generate Application Form")) GenerateApplicationForm();

            GUILayout.Space(5);
            GUILayout.Label(outputText);

            obj.ApplyModifiedProperties();
        }

        private void GenerateApplicationForm()
        {
            // ApplicationMailContent cont = new ApplicationMailContent(employee);
            // outputText = cont.ToString();
        }

        private void GenerateRndAmountEmployees()
        {
            //employee = new Employee("Hans", "Müller", TimeManager.Instance.INITIAL_TIMESTAMP);
            //outputText = "Generated an Emplyoee";
        }
    }
}
