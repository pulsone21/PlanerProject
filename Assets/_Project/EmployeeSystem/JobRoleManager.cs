using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace EmployeeSystem
{
    public class JobRoleManager : MonoBehaviour
    {
        public static JobRoleManager Instance;
        [SerializeField] private List<JobRole> jobRoles = new List<JobRole>();
        public static List<JobRole> JobRoles => Instance.jobRoles;
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
    }
}
