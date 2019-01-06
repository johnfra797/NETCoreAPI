using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityAPINETCore.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = Startup.Configuracion["mailSettings:mailToAddress"];
        private string _mailFrom = Startup.Configuracion["mailSettings:mailFromAddress"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"CLOUDMAILSERVICE Email enviado subject{subject} message {message}");
        }
    }
}
