using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadSystem
{
    [RequireComponent(typeof(MeshRenderer))]
    public class RoadVisualController : MonoBehaviour
    {
        [SerializeField] private Color DayRoadColor;
        [SerializeField] private Color NightRoadColor;
        [SerializeField] private bool NightModeOn;
        [SerializeField] private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        private void Start() => MapVisualController.Instance.RegisterForOnVisualChange(ToogleMode);
        private void OnDestroy() => MapVisualController.Instance.UnregisterForOnVisualChange(ToogleMode);


        public void ToogleMode(MapVisualController.MapMode mapMode)
        {
            NightModeOn = mapMode != MapVisualController.MapMode.day;
            if (!NightModeOn)
            {
                meshRenderer.sharedMaterial.color = DayRoadColor;
            }
            else
            {
                meshRenderer.sharedMaterial.color = NightRoadColor;
            }
        }

    }
}
