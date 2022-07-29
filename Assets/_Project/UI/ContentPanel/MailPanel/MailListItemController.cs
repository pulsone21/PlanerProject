using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MailSystem;

namespace UISystem
{
    public class MailListItemController : ListItemController<Mail>
    {
        [SerializeField] private TextMeshProUGUI mailSender;
        [SerializeField] private TextMeshProUGUI mailTopic;
        [SerializeField] private TextMeshProUGUI mailContrent;
        [SerializeField] private TextMeshProUGUI timeStamp;
        public override void Initialize(Mail mail)
        {
            if (Initialized) return;
            Initialized = true;
            item = mail;
            mailSender.text = item.MailSender;
            mailTopic.text = item.MailTopic;
            mailContrent.text = item.Content.ToString();
            timeStamp.text = item.TimeStamp.ToString();
            gameObject.SetActive(true);
        }
        public override void SetContent() => MailViewer.Instance.SetContent(item);
    }
}
