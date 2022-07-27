using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EmployeeSystem;
using System;

namespace UISystem
{
    public class HireEmployeeController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField employeeAmount;
        [SerializeField] private JobDropdownHandler dropdownHandler;




        public void PostVacancies()
        {
            string amountString = employeeAmount.text;
            Debug.Log(amountString);
            if (int.TryParse(amountString, out int amount))
            { // TODO Write validation script for that on the input field it self
                JobRole job = dropdownHandler.SelectedJob;
                List<JobListing> listings = new List<JobListing>();

                for (int i = 0; i < amount; i++)
                {
                    listings.Add(new JobListing(job));
                }
                CanidateSearcher.AddJobListing(listings);
                ClearInputs();
                return;
            }
            Debug.LogError("HireEmployeeController - PostVacancies - Try Parse -> Amount is not an int.");
        }

        private void ClearInputs()
        {
            employeeAmount.text = null;
        }
    }
}
