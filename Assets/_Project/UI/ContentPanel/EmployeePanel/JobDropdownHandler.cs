using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EmployeeSystem;

namespace UISystem
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class JobDropdownHandler : MonoBehaviour
    {
        private List<JobRole> jobs;
        private TMP_Dropdown dropdown;
        private JobRole selectedJob;
        public JobRole SelectedJob => selectedJob;

        private void Start()
        {
            dropdown = GetComponent<TMP_Dropdown>();
            jobs = JobRoleManager.JobRoles;
            List<string> dropdownList = new List<string>();
            foreach (JobRole job in jobs)
            {
                dropdownList.Add(job.Name);
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(dropdownList);
            selectedJob = jobs[0];
        }
        public void HandleDropDownChange(int item) => selectedJob = jobs[item];

    }
}
