using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planer;
using TMPro;

namespace UISystem
{
    public class MainPageController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI CompanyText;
        [SerializeField] private TextMeshProUGUI navBarCompanyText;

        private void Awake()
        {
            PlayerSettings pS = PlayerSettings.LoadFromFile();
            CompanyText.text = pS.CompanyName;
            navBarCompanyText.text = pS.CompanyName;
        }

    }
}
