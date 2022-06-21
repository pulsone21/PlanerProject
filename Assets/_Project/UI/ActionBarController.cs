using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace UISystem
{
    public class ActionBarController : MonoBehaviour
    {
        [SerializeField] private ToogleBtnVisualController currentBtnController;

        public void HighlightButtonInAction(ToogleBtnVisualController newBtn)
        {
            if (currentBtnController != null) currentBtnController.SetBtnPassive();
            currentBtnController = newBtn;
            currentBtnController.SetBtnActive();
        }


    }
}
