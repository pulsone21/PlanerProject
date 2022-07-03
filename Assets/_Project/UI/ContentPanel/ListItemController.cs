using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public abstract class ListItemController : MonoBehaviour
    {
        protected bool Initialized = false;
        [SerializeField] protected Button button;

        protected void Awake()
        {
            if (!Initialized) gameObject.SetActive(false);
        }
        protected abstract void OnEnable();
        protected abstract void OnDestroy();
    }
}
