using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPO.Code.Entity
{
   public class User
    {
        public string FullName { get; set; }
        public string NokiaUserName { get; set; }
        public string EmailAddress { get; set; }
        public int UserID { get; set; }
        public string Password { get; set; }
        public string IsAdmin { get; set; }
        public string IsActive { get; set; }
    }
}
