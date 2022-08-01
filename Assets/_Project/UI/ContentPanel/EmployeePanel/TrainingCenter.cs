using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;

namespace UISystem
{
    public class TrainingCenter : MonoBehaviour
    {
        public static TrainingCenter Instance;
        private Employee currentEmployee;
        private TrainingItemController selectedTraining;
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

        [SerializeField] private TrainingListController listController;
        [SerializeField] private List<SkillTraining> trainings;

        public static List<SkillTraining> Trainings => Instance.trainings;

        public static void ShowTrainingCenter(Employee employee) => Instance.ShowTrainings(employee);

        private void ShowTrainings(Employee employee)
        {
            currentEmployee = employee;
            gameObject.SetActive(true);
        }
        public static void SelectTraining(TrainingItemController training) => Instance.SelectTrain(training);
        private void SelectTrain(TrainingItemController training)
        {
            if (selectedTraining != null) selectedTraining.SetActive(false);
            selectedTraining = training;
            selectedTraining.SetActive(true);
        }

        public void Confirm()
        {

        }

        public void Cancle() => gameObject.SetActive(false);
    }
}
