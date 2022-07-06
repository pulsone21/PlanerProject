using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
namespace UISystem
{
    public class NavBarController : MonoBehaviour
    {
        private bool IsExpanded = false;
        [SerializeField] private AnimationCurve Curve;
        private const int EXPANDED_WIDTH = 245;
        private const int SHRINKED_WIDTH = 75;
        private IExpandable[] expandables;

        private RectTransform ownRect;

        private void Awake()
        {
            ownRect = GetComponent<RectTransform>();
        }
        private void Start()
        {
            expandables = transform.GetComponentsInChildren<MonoBehaviour>().OfType<IExpandable>().ToArray();
        }


        public bool ToggleExpand()
        {
            Debug.Log("Expanding NavBar");
            IsExpanded = !IsExpanded;
            if (IsExpanded)
            {
                ExpandNavBar();
                return IsExpanded;
            }
            else
            {
                ShrinkNavBar();
                return IsExpanded;
            }
        }


        public void ShrinkNavBar()
        {
            IsExpanded = false;
            foreach (IExpandable item in expandables)
            {
                item.Shrink();
            }
            ownRect.DOSizeDelta(new Vector2(SHRINKED_WIDTH, ownRect.sizeDelta.y), 0.4f).SetDelay(0.2f);
        }

        public void ExpandNavBar()
        {
            IsExpanded = true;
            foreach (IExpandable item in expandables)
            {
                item.Expand();
            }
            ownRect.DOSizeDelta(new Vector2(EXPANDED_WIDTH, ownRect.sizeDelta.y), 0.4f);

        }
        private void ExpandAllExpandables()
        {
            foreach (IExpandable item in expandables)
            {
                item.Expand();
            }

        }

        // IEnumerator ExnpantionAnimation(bool expanding)
        // {
        //     float currWidth = ownRect.sizeDelta.x;
        //     if (IsExpanded)
        //     {
        //         for (float i = 0; i < Curve[Curve.length - 1].value; i += Time.deltaTime)
        //         {
        //             float currentWidth = (Curve.Evaluate(i) * (EXPANDED_WIDTH - SHRINKED_WIDTH) + SHRINKED_WIDTH);
        //             ownRect.sizeDelta = new Vector2(currentWidth, ownRect.sizeDelta.y);
        //             yield return null;
        //         }

        //     }
        //     else
        //     {
        //         for (float i = Curve[Curve.length - 1].value; i > 0; i -= Time.deltaTime)
        //         {
        //             float currentWidth = (Curve.Evaluate(i) * (EXPANDED_WIDTH - SHRINKED_WIDTH) + SHRINKED_WIDTH);
        //             ownRect.sizeDelta = new Vector2(currentWidth, ownRect.sizeDelta.y);
        //             yield return true;
        //         }

        //     }


        // }

    }
}
