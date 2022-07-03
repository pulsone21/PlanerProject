using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MailSystem
{
    public class MailManager
    {
        private List<Mail> mails;

        public MailManager()
        {
            this.mails = new List<Mail>();
        }

        public List<Mail> GetAllMails() => mails;

        public void AddMail(Mail mail) => mails.Add(mail);
        public void AddMails(List<Mail> mails) => this.mails.AddRange(mails);

    }
}