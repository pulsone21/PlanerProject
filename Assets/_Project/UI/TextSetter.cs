using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UISystem
{
    public class TextSetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        public void SetText(string text) => textField.text = text;
    }
}
