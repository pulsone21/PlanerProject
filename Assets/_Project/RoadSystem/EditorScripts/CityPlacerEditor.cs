using UnityEngine;
using UnityEditor;


namespace RoadSystem
{
    [CustomEditor(typeof(CityPlacer))]
    public class CityPlacerEditor : Editor
    {

        private CityManager cityManager;
        private CityPlacer cP;



        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            cP = (CityPlacer)target;
            cityManager = cP.CityManager;
        }

        private void OnSceneGUI()
        {
            Input();
        }

        private void Input()
        {
            if (!cP.gameObject.activeSelf) return;
            if (cityManager == null)
            {
                Debug.LogError("CityManager not declared, please hook up the CityManager");
                return;
            }

            Event guiEvent = Event.current;
            Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
            if (guiEvent.type == EventType.MouseDown && guiEvent.shift)
            {
                var iconContet = EditorGUIUtility.IconContent("d_orangeLight");
                string cityName = EditorInputDialog.Show("Question", "Please enter the City Name", "");
                GameObject go = new GameObject(cityName, typeof(City));
                go.transform.SetParent(cityManager.transform, false);
                go.transform.localPosition = mousePos;
                City city = new City(cityName, 0);
                go.GetComponent<CityController>().SETUP_AddCity(city);
                EditorGUIUtility.SetIconForObject(go, (Texture2D)iconContet.image);
            }
        }


    }
}