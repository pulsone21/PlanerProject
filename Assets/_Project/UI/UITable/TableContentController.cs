using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public abstract class TableContentController : MonoBehaviour
    {
        [SerializeField] protected TableController table;

        public abstract void SetTableContent(string content);
    }
}
