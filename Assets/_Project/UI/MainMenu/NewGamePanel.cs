using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Planer
{
    public class NewGamePanel : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI startingMoney;
        [SerializeField] private TMP_InputField inputCompanyName;
        private int money;
        private readonly int[] startingMoneys = { 150000, 100000, 80000, 50000 };
        private void Start()
        {
            slider.onValueChanged.AddListener((newValue) =>
            {
                startingMoney.text = startingMoneys[Mathf.FloorToInt(newValue)].ToString();
                money = startingMoneys[Mathf.FloorToInt(newValue)];
            });
        }
        public void OnNewGameClick()
        {
            // TODO Figure Out how we can start the game with the provided Information
        }
    }
}
