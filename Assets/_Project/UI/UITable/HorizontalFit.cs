using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planer
{
    public class HorizontalFit : MonoBehaviour
    {
        private void Update()
        {
            RectTransform rect = GetComponent<RectTransform>();
            Vector2 oldV2 = rect.sizeDelta;
            float width = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
            rect.sizeDelta = new Vector2(width, oldV2.y);
        }
    }
}
