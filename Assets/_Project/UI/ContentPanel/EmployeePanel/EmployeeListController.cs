using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class EmployeeListController : MonoBehaviour
    {
        private bool Initialized = false;
        private void Awake()
        {
            if (!Initialized) gameObject.SetActive(false);
        }

        public void Initialize()
        {

        }
    }
}
