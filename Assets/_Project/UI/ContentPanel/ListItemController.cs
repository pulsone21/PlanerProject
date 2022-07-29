using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public abstract class ListItemController<T> : MonoBehaviour
    {
        protected T item;
        protected bool Initialized = false;
        [SerializeField] protected Button button;
        protected void Awake()
        {
            if (!Initialized) gameObject.SetActive(false);
        }
        public abstract void SetContent();
        public abstract void Initialize(T item);
        protected virtual void OnEnable() => button.onClick.AddListener(SetContent);
        protected virtual void OnDestroy() => button.onClick.RemoveListener(SetContent);
    }
}
