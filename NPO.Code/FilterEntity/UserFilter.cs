using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPO.Code.FilterEntity
{
  public  class UserFilter
    {
        public string FullName{ get; set; }
        public string NokiaUserName { get; set; }
        public string EmailAddress { get; set; }
        public int UserID { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}
