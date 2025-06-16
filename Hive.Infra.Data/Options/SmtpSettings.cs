﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Infra.Data.Options
{
    public class SmtpSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
    }
}
