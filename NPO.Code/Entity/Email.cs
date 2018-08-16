
namespace NPO.Code.Entity

{
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

        public bool IsMain { get; set; }

        public string DateTimeReceived { get; set; }



    }
}
