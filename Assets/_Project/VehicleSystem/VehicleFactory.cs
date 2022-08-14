using System.Collections.Generic;
using UnityEngine;
using Planer;
using CompanySystem;

namespace VehicleSystem
{
    public class VehicleFactory : MonoBehaviour
    {
        public static VehicleFactory Instance;
        private static string possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
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
        }

        [SerializeField] private List<VehicleSO> Vehicles = new List<VehicleSO>();
        [SerializeField] private List<TrailerSO> Trailers = new List<TrailerSO>();
        [SerializeField] private GameObject vehicleControllerPrefab;

        public static string GeneratePlateText()
        {
            int number = Random.Range(1, 10000);
            string city = PlayerCompanyController.Company.City.Name;
            string middleSection = "";
            for (int i = 0; i < 2; i++)
            {
                middleSection += possibleChars[Random.Range(0, possibleChars.Length)];
            }
            return city[1] + middleSection + number.ToString();

        }

        public List<VehicleSO> GetVehicles() => Vehicles;
        public List<TrailerSO> GetTrailers() => Trailers;
    }
}
