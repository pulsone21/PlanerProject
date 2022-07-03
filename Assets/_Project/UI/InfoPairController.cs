using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UISystem
{
    public class InfoPairController : MonoBehaviour
    {
        public enum TextType { label, value }
        [SerializeField] private TextMeshProUGUI LabelText;
        [SerializeField] private TextMeshProUGUI ValueText;

        public void SetText(TextType type, string text)
        {
            switch (type)
            {
                case TextType.label:
                    LabelText.text = text;
                    break;
                case TextType.value:
                    ValueText.text = text;
                    break;
            }
        }

    }
}
