using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MailSystem;
using CompanySystem;
using TMPro;

namespace UISystem
{
    public class MailListController : ListController
    {
        protected override void GenerateList()
        {
            List<Mail> mails = PlayerCompanyController.Instance.company.MailManager.GetAllMails();
            if (mails.Count > 0)
            {
                foreach (Mail mail in mails)
                {
                    GameObject go = Instantiate(ListItemPrefab, Vector3.zero, Quaternion.identity);
                    go.transform.SetParent(ListItemContainer);
                    go.GetComponent<MailListItemController>().Initialize(mail);
                }
            }
            else
            {
                GenerateDefaultText();
            }
            ListItemContainer.gameObject.SetActive(true);
        }
    }
}
