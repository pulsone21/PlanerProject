using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChartSystem
{
    public class Testing : MonoBehaviour
    {
        private float timer = 0f;

        private const float maxTimer = 1.5f;
        // Start is called before the first frame update
        void Start()
        {
            List<int> values = new List<int>();
            for (int i = 0; i < 31; i++)
            {
                values.Add(Random.Range(0, 2501));
            }
            ChartController.GenerateChart(values, "$", "Day");
        }

        private void Update()
        {

        }
    }
}
