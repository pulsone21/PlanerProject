using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CompanySystem;
namespace UISystem
{
    public class MainPageController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI CompanyText;
        [SerializeField] private TextMeshProUGUI navBarCompanyText;

        private void Start()
        {
            PlayerCompanyController pS = PlayerCompanyController.Instance;
            CompanyText.text = pS.Company.Name;
            navBarCompanyText.text = pS.Company.Name;
        }

    }
}
