using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MailSystem;

namespace UISystem
{
    public class MailListController : ListController
    {
        [SerializeField] private MailManager mailManager;
        protected override void GenerateList()
        {
            foreach (Mail mail in mailManager.GetAllMails())
            {
                GameObject go = Instantiate(ListItemPrefab, Vector3.zero, Quaternion.identity);
                go.transform.SetParent(ListItemContainer);
                go.GetComponent<MailListItemController>().Initialize(mail);
            }
        }
    }
}
