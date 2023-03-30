using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RoadSystem;
using UISystem;
using Sirenix.OdinInspector;
namespace Planer
{

    public class AutomationHelper : EditorWindow
    {
        string resultText;
        private const float COMPANY_RATIO = 0.000003f;
        public GameObject UIPrefab;
        public GameObject CapitalPrefab;
        public Transform UIContainer;
        public PlayerSettings settings;

        public Sprite capitalSprite;

        [MenuItem("PlanerProject/AutomationHelper")]
        private static void ShowWindow()
        {
            var window = GetWindow<AutomationHelper>();
            window.titleContent = new GUIContent("AutomationHelper");
            window.Show();
        }

        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);
            EditorGUILayout.PropertyField(obj.FindProperty("UIPrefab"));
            EditorGUILayout.PropertyField(obj.FindProperty("CapitalPrefab"));
            EditorGUILayout.PropertyField(obj.FindProperty("UIContainer"));
            EditorGUILayout.PropertyField(obj.FindProperty("capitalSprite"));
            EditorGUILayout.PropertyField(obj.FindProperty("settings"));

            if (GUILayout.Button("Generate UI")) GenerateUI();
            if (GUILayout.Button("Set Capital Sprites")) SetCapitalSprites();
            GUILayout.Space(25f);
            GUILayout.Label("Automation Feedback");
            GUILayout.Space(10f);
            GUILayout.TextArea(resultText);

            obj.ApplyModifiedProperties();
        }


        private void SetCapitalSprites()
        {
            CityUICntroller[] cCs = FindObjectsOfType<CityUICntroller>();
            Debug.Log($"found {cCs.Length} CityUIs");
            int count = 0;
            foreach (CityUICntroller cC in cCs)
            {
                if (cC.City.IsCaptial)
                {
                    cC.circleBackgorund.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    cC.circleBackgorund.transform.parent.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.875F);
                    cC.circleBoarder.sprite = capitalSprite;
                    cC.circleBackgorund.sprite = capitalSprite;
                    cC.circleBackgorund.rectTransform.sizeDelta = new Vector2(-20, -18);
                    cC.circleBackgorund.rectTransform.anchoredPosition = new Vector2(0, -1);
                    count++;
                }
            }
            SetFeedbackText($"Looped over {cCs.Length} UIs and found {count} Capitals and setting star sprite");
        }
        private void GenerateUI()
        {
            CityController[] cities = FindObjectsOfType<CityController>();
            Debug.Log($"found {cities.Length} CityUIs");
            foreach (CityController city in cities)
            {
                GameObject prefab = city.City.IsCaptial ? CapitalPrefab : UIPrefab;
                GameObject newUI = (GameObject)PrefabUtility.InstantiatePrefab(prefab, UIContainer);
                // newUI.GetComponent<RectTransform>().localScale = new Vector3(0.05f, 0.05f, 1f);
                newUI.name = $"CityUI_{city.name}";

                newUI.transform.position = city.transform.position;
                newUI.GetComponent<CityUICntroller>().SetName(city.name);
                newUI.GetComponent<CityUICntroller>().SetCity(city);
            }
        }
        private void ClearAutomationText() => resultText = "";
        private void SetFeedbackText(string text) => resultText = text;

        //Old Stuff
        private void FindEmptyCities()
        {
            List<City> cities = new List<City>();
            CityController[] cityController = FindObjectsOfType<CityController>();
            foreach (CityController cC in cityController)
            {
                if (cC.City.Citizen < 1) cities.Add(cC.City);
            }
            SetFeedbackText("found " + cities.Count + " Cities with no citizien, " + cities.ToString());
        }
        private void FindEmptyCityController()
        {
            CityController[] cityController = FindObjectsOfType<CityController>();
            foreach (CityController cC in cityController)
            {
                if (cC.City == null)
                {
                    Debug.Log("FoundEmpty City on " + cC.name);
                }
            }
        }
        private void CalcAvarageCitizen()
        {
            int maxCitizien = 0;
            CityController[] cityController = FindObjectsOfType<CityController>();
            foreach (CityController cC in cityController)
            {
                maxCitizien += cC.City.Citizen;
            }
            int avarage = maxCitizien / cityController.Length;
            SetFeedbackText("Avarge Citizen: " + avarage + " on " + cityController.Length + " Cities");
        }
    }
}
