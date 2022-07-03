using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MailSystem;
using Utilities;

namespace UISystem
{
    public class MailViewer : MonoBehaviour
    {
        public static MailViewer Instance;
        [SerializeField] private GameObject _hintMsg;
        [SerializeField] private TextMeshProUGUI _senderVal;
        [SerializeField] private TextMeshProUGUI _topicVal;
        [SerializeField] private TextMeshProUGUI _Content;

        private Mail currentMail;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void SetMail(Mail mail)
        {
            if (currentMail == mail) return;
            currentMail = mail;
            _senderVal.text = currentMail.MailSender;
            _topicVal.text = currentMail.MailTopic;
            _Content.text = currentMail.Content.ToString();
            transform.SetActiveAllChildren(true);
            _hintMsg.SetActive(false);
        }


    }
}
