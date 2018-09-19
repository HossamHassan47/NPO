using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPO.Code.FilterEntity
{
    public class EmailFilter
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }

        public string dateTimeReceived { get; set; }

        public string EmailRef { get; set; }

        public int Status { get; set; }

        public string dateTimeReceivedOperater { get; set; }


    }
}
