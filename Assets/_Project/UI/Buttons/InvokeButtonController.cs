using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UISystem
{
    public class InvokeButtonController : MonoBehaviour
    {
        [SerializeField] private List<Button> buttons = new List<Button>();
        public void Invoke(int index)
        {
            if (buttons.Count < 1)
            {
                Debug.Log($"No buttons in the list");
                return;
            }
            if (index > buttons.Count)
            {
                Debug.Log($"you tried to access an index put of range, Index:{index} ListCount: {buttons.Count}");
                return;
            }
            buttons[index].onClick?.Invoke();
        }
    }
}

