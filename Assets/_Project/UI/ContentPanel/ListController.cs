using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using TMPro;
namespace UISystem
{
    public abstract class ListController : MonoBehaviour
    {
        [SerializeField] protected GameObject ListItemPrefab;
        [SerializeField] protected GameObject defaultItemPrefab;
        [SerializeField] protected Transform ListItemContainer;
        [SerializeField] protected string defaultText;

        protected virtual void OnEnable()
        {
            ClearList();
            GenerateList();
        }
        protected virtual void ClearList() => ListItemContainer.ClearAllChildren();
        protected abstract void GenerateList();
        protected virtual void GenerateDefaultText()
        {
            GameObject go = Instantiate(defaultItemPrefab);
            go.GetComponentsInChildren<TextMeshProUGUI>()[0].text = defaultText;
            go.transform.SetParent(ListItemContainer);
        }
    }
}
