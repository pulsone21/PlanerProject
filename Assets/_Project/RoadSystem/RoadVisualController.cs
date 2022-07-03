using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class RoadVisualController : MonoBehaviour
    {
        [SerializeField] private Color DayRoadColor;
        [SerializeField] private Color NightRoadColor;
        [SerializeField] private bool NightModeOn;
        [SerializeField] private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        private void Start() => MapVisualController.Instance.RegisterForOnVisualChange(ToogleMode);
        private void OnDestroy() => MapVisualController.Instance.UnregisterForOnVisualChange(ToogleMode);


        public void ToogleMode(MapVisualController.MapMode mapMode)
        {
            NightModeOn = true;
            if (mapMode == MapVisualController.MapMode.day)
            {
                NightModeOn = false;
            }
            if (!NightModeOn)
            {
                lineRenderer.startColor = DayRoadColor;
                lineRenderer.endColor = DayRoadColor;
            }
            else
            {
                lineRenderer.startColor = NightRoadColor;
                lineRenderer.endColor = NightRoadColor;
            }
        }

    }
}
