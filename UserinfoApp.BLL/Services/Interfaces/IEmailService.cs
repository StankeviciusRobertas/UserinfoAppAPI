using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserinfoApp.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(string? to, string body);
    }
}
