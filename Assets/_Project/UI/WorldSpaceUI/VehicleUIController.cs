using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VehicleSystem;

namespace UISystem
{
    public class VehicleUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI plateText;
        private VehicleController vehicleController;
        public void SetVehicle(VehicleController VehicleController)
        {
            vehicleController = VehicleController;
            plateText.text = vehicleController.Vehicle.PlateText;
        }




    }
}
