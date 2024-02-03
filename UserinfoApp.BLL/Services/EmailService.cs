using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserinfoApp.BLL.Services.Interfaces;

namespace UserinfoApp.BLL.Services
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(string? to, string body)
        {
            if (to != null)
            {
                // send email implementation
                return true;
            }
            return false;
        }
    }
}
