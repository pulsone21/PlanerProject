using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;


namespace UISystem
{
    public class TrainingListController : ListController
    {
        protected override void GenerateList()
        {
            List<SkillTraining> trainings = TrainingCenter.Trainings;
            if (trainings.Count > 1)
            {
                foreach (SkillTraining training in trainings)
                {
                    GameObject go = Instantiate(ListItemPrefab, Vector3.zero, Quaternion.identity);
                    go.transform.SetParent(ListItemContainer);
                    go.GetComponent<TrainingItemController>().Initialize(training);
                }
            }
            else
            {
                GenerateDefaultText();
            }
            ListItemContainer.gameObject.SetActive(true);
        }
    }
}
