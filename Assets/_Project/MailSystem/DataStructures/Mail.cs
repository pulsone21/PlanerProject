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
        private string _mailSender;
        private string _mailTopic;
        private MailContent _content;
        private TimeStamp _timeStamp;
        public Mail(string mailSender, string mailTopic, MailContent content, TimeStamp timeStamp)
        {
            _mailSender = mailSender;
            _mailTopic = mailTopic;
            _content = content;
            _timeStamp = timeStamp;
            SendMail();
        }

        public string MailSender => _mailSender;
        public string MailTopic => _mailTopic;
        public MailContent Content => _content;
        public TimeStamp TimeStamp => _timeStamp;

        private void SendMail()
        {
            throw new NotImplementedException();
        }
    }
}
