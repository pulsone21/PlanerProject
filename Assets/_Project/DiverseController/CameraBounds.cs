using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planer
{
    public class CameraBounds : MonoBehaviour
    {
        public float Bottom;
        public float Top;
        public float Left;
        public float Right;


        private void Update()
        {
            Vector3 currentPos = transform.position;
            if (transform.position.y > Top) transform.position = new Vector3(currentPos.x, Top, 0);
            if (transform.position.y < Bottom) transform.position = new Vector3(currentPos.x, Bottom, 0);
            if (transform.position.x > Right) transform.position = new Vector3(Right, currentPos.y, 0);
            if (transform.position.x < Left) transform.position = new Vector3(Left, currentPos.y, 0);
        }
    }
}
