using System.Collections.Generic;
using UnityEngine;
using Planer;
using CompanySystem;
using SLSystem;
using Sirenix.OdinInspector;
namespace VehicleSystem
{
    public class VehicleFactory : MonoBehaviour
    {
        public static VehicleFactory Instance;
        private static string possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //TODO change this to get loaded somewhere
        [SerializeField] private List<VehicleSO> Vehicles = new List<VehicleSO>();
        [SerializeField] private List<TrailerSO> Trailers = new List<TrailerSO>();
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
#if !UNITY_EDITOR
            string city = PlayerCompanyController.Instance.Company.City.Name;
#endif
            string middleSection = "";
            for (int i = 0; i < 2; i++)
            {
                middleSection += possibleChars[Random.Range(0, possibleChars.Length)];
            }
#if !UNITY_EDITOR
            return city[1] + middleSection + number.ToString();
#else
            return middleSection + number.ToString();
#endif

        }
        public static List<VehicleSO> GetVehicles() => Instance.Vehicles;
        public static List<TrailerSO> GetTrailers() => Instance.Trailers;

        [Button("Serialze VehicleSO")]
        private void SerializeVehicleSO() => Debug.Log(JsonUtility.ToJson(Vehicles[0]));

        [Button("Serialze TrailerSO")]
        private void SerialzeTrailerSO() => Debug.Log(JsonUtility.ToJson(Trailers[0]));


    }
}
