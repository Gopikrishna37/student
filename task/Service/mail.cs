using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task.Service
{
    public class Mail
    {
        public string Sender(string Email)
        {             // Here we need to write the Code for sending the mail             //create a new mime message object which we are going to use to fill the message data.
            MimeMessage message = new MimeMessage();
            // add the sender info that will appear 1n the email messsage
            message.From.Add(new MailboxAddress("Tester", "testinggk007@gmail.com"));
            // add the recevier email address
            message.To.Add(MailboxAddress.Parse(Email));
            //add subject
            message.Subject = "Account Creation"; message.Body = new TextPart
            {
                Text = @"
                            Dear User, 
                            
                            Your account has been created successfully.
                            
                            Regards,
                            XYZ Team
                        "
            };
            // ask the user to enter the email             
            // ask the user to enter the password
            string emailAddress = "testinggk007@gmail.com";
            string emailPassword = "vfyhloidsoguczma";              /*smtp start*/
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
    }
}