using RModel;
using RDATA.Entities;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace RRepo.Utilities
{
    public interface IMailClient
    {
        

        bool SendMail(string to, string subject, string body);
        bool SendMulpMail(string to, string subject, string body);
        

    }
}