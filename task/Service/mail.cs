using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace task.Service
{
    public class Mail
    {
        public string Sender(string Email, string Otp)
        {
            try
            {
                // Here we need to write the Code for sending the mail             
                //create a new mime message object which we are going to use to fill the message data.
                MimeMessage message = new MimeMessage();
                
                // add the sender info that will appear 1n the email messsage
                message.From.Add(new MailboxAddress("Tester", "testinggk007@gmail.com"));
                
                // add the recevier email address
                message.To.Add(MailboxAddress.Parse(Email));
                message.Subject = "Account Creation";

            //add subject
            /* message.Body = new TextPart
             {
                 Text = @"
                                   Dear User, 

                                   Your account has been created successfully.

                                   Regards,
                                   XYZ Team
                               "
             };
*/
            // Get HTML page using file path
                string html = File.ReadAllText("C://Users//BE HAPPY//source/repos/task/ShopViewWebAPI//Service//msg.html");
                html = html.Replace("otp", Otp);
                // HTML body
                var builder = new BodyBuilder
                {
                    HtmlBody = html
                };

                message.Body = builder.ToMessageBody();
                // ask the user to enter the email             
                // ask the user to enter the password
                string emailAddress = "testinggk007@gmail.com";
                string emailPassword = "vfyhloidsoguczma";              /*smtp start*/
                SmtpClient smtp = new SmtpClient();
                try
                {
                    //connect to the gmail smtp server using port 465 with ssL enabled
                    smtp.Connect("smtp.gmail.com", 465, true);
                    smtp.Authenticate(emailAddress, emailPassword);
                    smtp.Send(message);
                }
                catch (Exception)
                {
                    /*here  set the mailer log*/
                    return "error";
                }
                finally
                {
                    smtp.Disconnect(true); smtp.Dispose();
                }              /*smtp end*/
                return "success";
            }

            catch (Exception e)
            {
                return "error";
            }
        }
    }
}