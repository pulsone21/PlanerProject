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
                return;
            }
            if (index > buttons.Count)
            {
                return;
            }
            buttons[index].onClick?.Invoke();
        }
    }
}

