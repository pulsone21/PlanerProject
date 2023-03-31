using UnityEngine;
using TMPro;
using VehicleSystem;
using RoadSystem;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace UISystem
{
    [RequireComponent(typeof(VehicleController))]
    public class VehicleUIController : UIColorChanger, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI plateText;
        [SerializeField] private Image Border, Background;
        [SerializeField] private VehicleController vehicleController;
        protected override void Start()
        {
            base.Start();
            ChangeUIColors(MapVisualController.CurrentMode);
            vehicleController = GetComponent<VehicleController>();
            plateText.text = vehicleController.Vehicle.PlateText;
        }
        protected override void SetColorShema(Color borderColor, Color bgCol)
        {
            plateText.color = borderColor;
            Border.color = borderColor;
            Background.color = bgCol;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}
