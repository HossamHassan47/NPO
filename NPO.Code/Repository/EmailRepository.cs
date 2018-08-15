using NPO.Code.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPO.Code.Repository
{
    public class EmailRepository
    {
        public List<Email> GetAllEmails()
        {
            return new List<Email>();
        }

        public int InsertNewEmail(Email email)
        {
            // Insert email into database then return the ID
            return 0;
        }

        public int UpdateEmail(Email email)
        {
            // Update email then return the ID
            return 0;
        }

        public bool DeleteEmail(Email email)
        {
            // Delete email
            return true;
        }
    }
}
