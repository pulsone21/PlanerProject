using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    public class JobListing
    {
        private int _canidateChance;
        public readonly JobRole Job;

        public JobListing(JobRole job)
        {
            _canidateChance = 100;
            Job = job;
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
