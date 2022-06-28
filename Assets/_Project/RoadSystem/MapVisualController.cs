using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MapVisualController : MonoBehaviour
    {
        public enum MapMode { day, night }
        public static MapVisualController Instance;

        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        [SerializeField] private Sprite DayMap;
        [SerializeField] private Sprite NightMap;
        [SerializeField] private MapMode currentMode;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private System.Action<MapMode> OnVisualChange;
        public void RegisterForOnVisualChange(System.Action<MapMode> action) => OnVisualChange += action;
        public void UnregisterForOnVisualChange(System.Action<MapMode> action) => OnVisualChange -= action;

        public void ToogleMode()
        {
            switch (currentMode)
            {
                case MapMode.day:
                    SetMapMode(MapMode.night);
                    break;
                case MapMode.night:
                    SetMapMode(MapMode.day);
                    break;
            }
        }

        private void SetDayMap()
        {
            spriteRenderer.sprite = DayMap;
            currentMode = MapMode.day;
            OnVisualChange?.Invoke(currentMode);
        }

        private void SetNightMap()
        {
            spriteRenderer.sprite = NightMap;
            currentMode = MapMode.night;
            OnVisualChange?.Invoke(currentMode);
        }


        public void SetMapMode(MapMode newMode)
        {
            switch (newMode)
            {
                case MapMode.day:
                    SetDayMap();
                    break;
                case MapMode.night:
                    SetNightMap();
                    break;
            }
        }
    }
}
