using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;

namespace UISystem
{
    public class JobListController : ListController
    {
        protected override void GenerateList()
        {
            List<JobListing> listings = CanidateSearcher.Instance.JobListings;
            if (listings.Count > 0)
            {
                foreach (JobListing listing in CanidateSearcher.Instance.JobListings)
                {
                    GameObject newObj = Instantiate(ListItemPrefab);
                    newObj.transform.SetParent(ListItemContainer);
                    newObj.GetComponent<JobListItemController>().Initialize(listing);
                }
                return;
            }
            GenerateDefaultText();
        }
    }
}
