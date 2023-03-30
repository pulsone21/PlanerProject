using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;
using System;

namespace MailSystem
{
    [Serializable]
    public class Mail
    {
        [SerializeField] private string _mailSender;
        [SerializeField] private string _mailTopic;
        [SerializeField] private string _content;
        [SerializeField] private TimeStamp _timeStamp;
        public Mail(string mailSender, string mailTopic, string content, TimeStamp timeStamp)
        {
            _mailSender = mailSender;
            _mailTopic = mailTopic;
            _content = content;
            _timeStamp = timeStamp;
            SendMail();
        }

        public string MailSender => _mailSender;
        public string MailTopic => _mailTopic;
        public string Content => _content;
        public TimeStamp TimeStamp => _timeStamp;

        private void SendMail()
        {
            throw new NotImplementedException();
        }
    }
}
