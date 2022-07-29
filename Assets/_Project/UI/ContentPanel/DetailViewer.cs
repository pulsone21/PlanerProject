using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UISystem
{
    public abstract class DetailViewer<T1, T2> : MonoBehaviour where T1 : DetailViewer<T1, T2>, new()
    {
        public static T1 Instance;
        [SerializeField] protected GameObject defaultText;
        [SerializeField] protected GameObject detailContainer;

        protected virtual void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this as T1;
            }
        }
        protected virtual void ShowDetails(bool state)
        {
            defaultText.SetActive(!state);
            detailContainer.SetActive(state);
        }
        protected virtual void Start()
        {
            defaultText.SetActive(true);
            detailContainer.SetActive(false);
        }
        protected T2 currentContent;
        public abstract void SetContent(T2 item);
    }
}
