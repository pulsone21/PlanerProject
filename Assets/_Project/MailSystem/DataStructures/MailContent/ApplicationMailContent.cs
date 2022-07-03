using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmployeeSystem;
using Utilities;

namespace MailSystem
{
    public class ApplicationMailContent : MailContent
    {
        private Employee employee;
        private JobListing listing;
        public ApplicationMailContent(Employee employee, JobListing listing)
        {
            this.employee = employee;
            this.listing = listing;
            GenerateContent();
        }

        protected override void GenerateContent()
        {
            string rawContent = DataHandler.LoadFromData("MailContent/applications_de.cont");
            string[] contents = rawContent.Split("///NEWCONTENT");
            int rndApplication = Random.Range(0, contents.Length);
            content = ReplacePlaceholder(contents[rndApplication]);
        }

        private string ReplacePlaceholder(string content)
        {
            string outString = content;
            outString = outString.Replace("${employeeName}", employee.Name.ToString());
            outString = outString.Replace("${JobRole}", listing.Job.Name);
            //TODO figure out which parts to repalce.....
            return outString;
        }
    }
}
