using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace UISystem
{
    public abstract class ListController : MonoBehaviour
    {
        [SerializeField] protected GameObject ListItemPrefab;
        [SerializeField] protected GameObject defaultItemPrefab;
        [SerializeField] protected Transform ListItemContainer;
        protected virtual void OnEnable()
        {
            ClearList();
            GenerateList();
        }

        protected virtual void ClearList() => ListItemContainer.ClearAllChildren();

        protected abstract void GenerateList();
    }
}
