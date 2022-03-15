using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Mailgun;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic
{
    public class MailService
    {
        public async Task<bool> SendPass(User user)
        {
            var sender = new MailgunSender(
               "nemanja-vukmirovic.me", // Mailgun Domain
               "key-dfc4d2aeba51b66d92969ef0828e7096",
               MailGunRegion.EU // Mailgun API Key
            );
            Email.DefaultSender = sender;

            string body = "Welcome "+user.Username+", "+"your password is "+user.Password;

            var email = Email
            .From("no-replay@nemanja-vukmirovic.me")
            .To(user.Email)
            .Subject("Password")
            .Body(body);

            SendResponse response = await email.SendAsync();

            if(response.Successful)
            {
                return true;
            } 
            else
            {
                return false;
            }        
        }
    }
}
