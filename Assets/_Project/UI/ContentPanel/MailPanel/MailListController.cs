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
            List<Mail> mails = PlayerCompanyController.Company.MailManager.GetAllMails();
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
                GameObject go = Instantiate(defaultItemPrefab);
                go.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "You currently don't have any emails.";
                go.transform.SetParent(ListItemContainer);
            }
            ListItemContainer.gameObject.SetActive(true);
        }
    }
}
