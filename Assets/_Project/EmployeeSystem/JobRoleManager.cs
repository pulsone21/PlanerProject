using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace EmployeeSystem
{
    //TODO Implement Loading JobRoles from Config
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

        public static JobRole GetJobRoleByName(string Name)
        {
            List<JobRole> roles = Instance.jobRoles;
            foreach (JobRole role in roles)
            {
                Debug.Log($"looking for {Name}, current Role {role.Name}");
                if (role.name.ToLower() == Name.ToLower()) return role;
            }
            Debug.LogError($"Type of {Name} - Dont have a JobRole in the manager present");
            return default;
        }
    }
}
