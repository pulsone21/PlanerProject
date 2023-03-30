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
        [SerializeField] private int[] startingMoneys = { 150000, 100000, 90000, 75000, 50000 };
        private void Start()
        {
            startingMoney.text = startingMoneys[0].ToString();
            slider.onValueChanged.AddListener((newValue) =>
            {
                Debug.Log("Listener Fired");
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
