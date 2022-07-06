using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmployeeSystem
{
    [RequireComponent(typeof(EmployeeController))]
    public class EmployeeLearnController : MonoBehaviour
    {
        private EmployeeController _controller;
        private void Start() => _controller = GetComponent<EmployeeController>();

    }
}
