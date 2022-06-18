using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;
using UnityEngine.UI;

namespace Planer
{
    public class IconButtonController : MonoBehaviour
    {
        [SerializeField] private Color activeIconColor;
        [SerializeField] private Color passivIconColor;
        [SerializeField] private Color activeBackGroundColor;
        [SerializeField] private Color passivBackGroundColor;
        [SerializeField] private SVGImage Icon;
        [SerializeField] private Image Image;
        private bool IsActive;

        public void ToogleIconColor()
        {
            IsActive = !IsActive;
            if (IsActive)
            {
                Icon.color = activeIconColor;
                Image.color = activeBackGroundColor;
                return;
            }
            Icon.color = passivIconColor;
            Image.color = passivBackGroundColor;
        }

    }
}
