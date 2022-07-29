using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MailSystem;
using Utilities;

namespace UISystem
{
    public class MailViewer : DetailViewer<MailViewer, Mail>
    {
        [SerializeField] private TextMeshProUGUI _senderVal;
        [SerializeField] private TextMeshProUGUI _topicVal;
        [SerializeField] private TextMeshProUGUI _Content;
        public override void SetContent(Mail mail)
        {
            if (currentContent == mail) return;
            currentContent = mail;
            _senderVal.text = currentContent.MailSender;
            _topicVal.text = currentContent.MailTopic;
            _Content.text = currentContent.Content.ToString();
            ShowDetails(true);
        }
    }
}
