﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;

namespace B1Library.EnviaEmail
{
    public class EnviaEmailAutomatico
    {
        public string Provedor { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool SmtpAuth { get; private set; }
        public bool Ssl { get; set; }
        public int Porta { get; private set; }

        public EnviaEmailAutomatico(string provedor, string username, string password, bool smtpAuth, int porta, bool ssl)
        {
            Provedor = provedor ?? throw new ArgumentNullException(nameof(provedor));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            SmtpAuth = smtpAuth;
            Porta = porta;
            Ssl = ssl;
        }

        public void SendEmail(List<string> emailsTo, string subject, string body, List<string> attachments, string emailOculto)
        {
            var message = PrepareteMessage(emailsTo, subject, body, attachments, emailOculto);

            SendEmailBySmtp(message);
        }

        private MailMessage PrepareteMessage(List<string> emailsTo, string subject, string body, List<string> attachments, string emailOculto)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(Username);

            foreach (var email in emailsTo)
            {
                if (ValidateEmail(email))
                {
                    mail.To.Add(email);
                }
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            foreach (var file in attachments)
            {
                var data = new Attachment(file, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

                mail.Attachments.Add(data);
            }

            return mail;
        }

        private bool ValidateEmail(string email)
        {
            Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            if (expression.IsMatch(email))
                return true;

            return false;
        }

        private void SendEmailBySmtp(MailMessage message)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(Provedor);
                smtpClient.Host = Provedor;
                smtpClient.Port = Porta;
                smtpClient.EnableSsl = SmtpAuth;
                smtpClient.Timeout = 50000;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(Username, Password);
                smtpClient.EnableSsl = Ssl;
                smtpClient.Send(message);
                smtpClient.Dispose();
            }
            catch(Exception ex)
            {
                string teste = ex.Message;
            }
    
        }
    }
  

}
