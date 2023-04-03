using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;
namespace EmployeeSystem
{
    public class JobListing
    {
        private int _canidateChance;
        public readonly JobRole Job;
        public readonly TimeStamp InsertedTime;

        public JobListing(JobRole job, TimeStamp inserted)
        {
            _canidateChance = 100;
            Job = job;
            InsertedTime = inserted;
        }

        public float GetCanidateChance()
        {
            float outFloat = _canidateChance / 100;
            _canidateChance -= 10;
            if (_canidateChance < 25) _canidateChance = 25;
            return outFloat;
        }


    }
}
