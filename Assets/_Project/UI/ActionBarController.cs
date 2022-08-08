using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utilities;

namespace UISystem
{
    public class ActionBarController : MonoBehaviour
    {
        [SerializeField] private List<ToogleBtnVisualController> myButtons = new List<ToogleBtnVisualController>();
        private ToogleBtnVisualController currentBtnController;
        private void Awake()
        {
            myButtons = GetComponentsInChildren<ToogleBtnVisualController>().ToList();
        }
        private void SetButtonsPassiv()
        {
            foreach (ToogleBtnVisualController myButton in myButtons)
            {
                if (myButton == currentBtnController) continue;
                myButton.SetBtnPassive(true);
            }
        }
        public void HighlightButtonInAction(ToogleBtnVisualController newBtn)
        {
            currentBtnController = newBtn;
            SetButtonsPassiv();
            currentBtnController.SetBtnActive();
        }
    }
}
