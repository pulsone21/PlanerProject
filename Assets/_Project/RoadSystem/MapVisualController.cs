using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MapVisualController : MonoBehaviour
    {

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
        [SerializeField] private bool NightModeOn;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private System.Action<bool> OnVisualChange;
        public void RegisterForOnVisualChange(System.Action<bool> action) => OnVisualChange += action;
        public void UnregisterForOnVisualChange(System.Action<bool> action) => OnVisualChange -= action;

        public void ToogleMode()
        {
            NightModeOn = !NightModeOn;
            if (!NightModeOn)
            {
                SetDayMap();
            }
            else
            {
                SetNightMap();
            }
        }

        private void SetDayMap()
        {
            spriteRenderer.sprite = DayMap;
            NightModeOn = false;
            OnVisualChange?.Invoke(NightModeOn);
        }

        private void SetNightMap()
        {
            spriteRenderer.sprite = NightMap;
            NightModeOn = true;
            OnVisualChange?.Invoke(NightModeOn);
        }
    }
}
