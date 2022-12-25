using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;
using MailSystem;
using CompanySystem;
using SLSystem;

namespace EmployeeSystem
{
    public class CanidateSearcher : MonoBehaviour, IPersistenceData
    {
        public static CanidateSearcher Instance;
        [SerializeField] private List<JobListing> jobListings = new List<JobListing>();
        private string _className;
        public GameObject This => gameObject;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            _className = GetType().Name;
        }

        private void Start() => TimeManager.Instance.RegisterForTimeUpdate(LookForCanidates, TimeManager.SubscriptionType.Day);
        private void OnDestroy() => TimeManager.Instance.UnregisterForTimeUpdate(LookForCanidates, TimeManager.SubscriptionType.Day);

        public static void AddJobListing(JobListing listing) => Instance.jobListings.Add(listing);
        public static void AddJobListing(List<JobListing> listings) => Instance.jobListings.AddRange(listings);
        public static void RemoveJobListing(JobListing listing)
        {
            if (!Instance.jobListings.Remove(listing))
            {
                Debug.LogError("Coudlnt remove Joblisting, maybe not found");
            }
        }
        private void LookForCanidates()
        {
            if (jobListings.Count < 1) return;
            const float OVERALL_CHANCE_FOR_CANIDATE = 0.30f;

            foreach (JobListing listing in jobListings)
            {
                int chance = UnityEngine.Random.Range(1, 6);
                int canidates = Mathf.CeilToInt(chance * (OVERALL_CHANCE_FOR_CANIDATE * listing.GetCanidateChance()));
                List<Employee> canidatesList = EmplyoeeGenerator.GenerateEmployees(canidates);
                foreach (Employee canidate in canidatesList)
                {
                    ApplicationMailContent content = new ApplicationMailContent(canidate, listing);
                    Mail mail = new Mail("Job Center", $"Application from: {canidate.Name.ToString()}", content.ToString(), TimeManager.Instance.CurrentTimeStamp);
                    PlayerCompanyController.Instance.company.MailManager.AddMail(mail);
                }
            }
        }

        public void Load(GameData gameData)
        {
            if (gameData.Data.ContainsKey(_className))
            {
                jobListings = JsonUtility.FromJson<PersistenceData>(gameData.Data[_className]).Listings;
            }
        }

        public void Save(ref GameData gameData)
        {
            gameData.Data[_className] = new PersistenceData(jobListings).ToString();
        }

        private class PersistenceData
        {
            public List<JobListing> Listings;

            public PersistenceData(List<JobListing> listings)
            {
                Listings = listings;
            }
            public override string ToString() => JsonUtility.ToJson(this);

        }
    }
}
