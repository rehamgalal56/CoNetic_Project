using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoNetic.Services.Helper
{
    public class SMTP
    {

        public string SmtpServer { set; get; }
        public int SmtpPort { set; get; }
        public string SenderEmail { set; get; }
        public string SenderPassword { set; get; }

    }
}
