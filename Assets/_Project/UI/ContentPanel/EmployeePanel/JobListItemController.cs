using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using TMPro;
using UnityEngine.UI;
using Unity.VectorGraphics;

namespace UISystem
{
    public class JobListItemController : ListItemController<JobListing>
    {
        [SerializeField] private TextMeshProUGUI jobRole, TimeStamp;
        [SerializeField] private SVGImage image;
        public override void Initialize(JobListing item)
        {
            if (Initialized) return;
            Initialized = true;
            this.item = item;
            jobRole.text = item.Job.Name;
            TimeStamp.text = item.InsertedTime.ToDateString();
            image.sprite = item.Job.Icon;
            gameObject.SetActive(true);
        }
        public override void SetContent()
        {
            //! Used to clear the Listing;
            CanidateSearcher.RemoveJobListing(item);
            Destroy(gameObject);
        }
    }
}
