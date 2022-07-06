using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;


namespace Planer
{


    [CustomEditor(typeof(TestScene))]
    public class TestSceneEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            TestScene tS = (TestScene)target;
            Transform targetA = tS.targetA;
            Transform targetB = tS.targetB;

            Debug.Log(Vector3.Distance(targetA.position, targetB.position));
        }
    }
}
