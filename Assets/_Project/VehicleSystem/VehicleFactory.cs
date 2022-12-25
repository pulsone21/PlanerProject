using System.Collections.Generic;
using UnityEngine;
using Planer;
using CompanySystem;
using SLSystem;

namespace VehicleSystem
{
    public class VehicleFactory : MonoBehaviour
    {
        public static VehicleFactory Instance;
        private static string possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //TODO change this to get loaded somewhere
        [SerializeField] private List<VehicleSO> Vehicles = new List<VehicleSO>();
        [SerializeField] private List<TrailerSO> Trailers = new List<TrailerSO>();
        [SerializeField] private GameObject vehicleControllerPrefab;

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
        public static string GeneratePlateText()
        {
            int number = Random.Range(1, 10000);
            string city = PlayerCompanyController.Instance.company.City.Name;
            string middleSection = "";
            for (int i = 0; i < 2; i++)
            {
                middleSection += possibleChars[Random.Range(0, possibleChars.Length)];
            }
            return city[1] + middleSection + number.ToString();

        }
        public static List<VehicleSO> GetVehicles() => Instance.Vehicles;
        public static List<TrailerSO> GetTrailers() => Instance.Trailers;
    }
}
