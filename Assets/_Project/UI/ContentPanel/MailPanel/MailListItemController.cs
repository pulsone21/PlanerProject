using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MailSystem;

namespace UISystem
{
    public class MailListItemController : ListItemController
    {
        private Mail _mail;
        [SerializeField] private TextMeshProUGUI mailSender;
        [SerializeField] private TextMeshProUGUI mailTopic;
        [SerializeField] private TextMeshProUGUI mailContrent;
        [SerializeField] private TextMeshProUGUI timeStamp;

        protected override void OnEnable() => button.onClick.AddListener(SetMail);
        protected override void OnDestroy() => button.onClick.RemoveListener(SetMail);
        private void SetMail() => MailViewer.Instance.SetMail(_mail);
        public void Initialize(Mail mail)
        {
            if (Initialized) return;
            Initialized = true;
            _mail = mail;
            mailSender.text = _mail.MailSender;
            mailTopic.text = _mail.MailTopic;
            mailContrent.text = _mail.Content.ToString();
            timeStamp.text = _mail.TimeStamp.ToString();
            gameObject.SetActive(true);
        }
    }
}
