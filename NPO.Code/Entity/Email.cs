﻿
using System;

namespace NPO.Code.Entity

{

    // 1. order mails
    // recived date, html body
    // 2. Object data source custom pageing
    public class Email
    {
        public int EmailId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string BodyHtml { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string CC { get; set; }

        public string Path { get; set; }

        public int TicketId { get; set; }

        public string EmailRef { get; set; }

        public bool IsMain { get; set; }

        public DateTime DateTimeReceived { get; set; }



    }
}
