using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HyderabadService
{
    class SendEmail
    {
        static public void Send(string candidateEmail, string subject, string body)
        {
            try
            {
                string myGmailAddress = ConfigurationManager.AppSettings["mailId"];
                string appSpecificPassword = ConfigurationManager.AppSettings["appSpecificPassword"];
                //if candidate email is not specified, sent to the default mail id
                if (candidateEmail == null) { candidateEmail = ConfigurationManager.AppSettings["candidateEmail"]; }

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                //provide credentials to smtpclient
                smtp.Credentials = new NetworkCredential(myGmailAddress, appSpecificPassword);

                MailMessage message = new MailMessage(myGmailAddress, candidateEmail);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                //sends the message to the recipient
                smtp.Send(message);
                Logger.Log("Mail Sent to recipient"+candidateEmail);
            }
            catch (SmtpException exp)
            {
                Logger.Log("Unable to send email to the candidate:  Email: " + candidateEmail+"\nException is "+exp);

                         
            }
            catch (Exception exp)
            {
                Logger.Log("Some failure occured while sending email to candidate { Email: " + candidateEmail + " }" + "\nException is " + exp);
                
            }
        }
    }
}

