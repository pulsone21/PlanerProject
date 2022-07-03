using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UISystem
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class VerticalAutoFit : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float ChildHeight;
        private VerticalLayoutGroup vlg;
        private float spacing => vlg.spacing;
        private float padding => vlg.padding.top + vlg.padding.bottom;
        private int lastChildCount;

        private void Awake()
        {
            vlg = GetComponent<VerticalLayoutGroup>();
        }
        // Start is called before the first frame update
        void Start()
        {
            lastChildCount = transform.childCount;
            CalcNewHight();
        }

        private void CalcNewHight()
        {
            float newHeight = (lastChildCount * ChildHeight) + (spacing * lastChildCount) + padding;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newHeight);
        }

        // Update is called once per frame
        void Update()
        {
            if (lastChildCount != transform.childCount)
            {
                lastChildCount = transform.childCount;
                CalcNewHight();
            }
        }
    }
}
