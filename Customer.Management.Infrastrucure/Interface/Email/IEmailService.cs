using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Infrastrucure.Interface.Email
{
    public interface IEmailService
    {

        Task SendEmail(string receipintEmail, string emailSubject, string emailBody);

    }
}
