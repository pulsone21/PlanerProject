using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadSystem;

namespace UISystem
{
    public abstract class UIColorChanger : MonoBehaviour
    {
        [SerializeField] protected Color dayBorder, dayBackground, nightBorder, nightBackground;
        protected virtual void Start() => MapVisualController.Instance.RegisterForOnVisualChange(ChangeUIColors);
        protected void ChangeUIColors(MapVisualController.MapMode mode)
        {
            switch (mode)
            {
                case MapVisualController.MapMode.day:
                    SetColorShema(dayBorder, dayBackground);
                    break;
                case MapVisualController.MapMode.night:
                    SetColorShema(nightBorder, nightBackground);
                    break;
            }
        }
        protected abstract void SetColorShema(Color borderColor, Color bgCol);
    }
}
