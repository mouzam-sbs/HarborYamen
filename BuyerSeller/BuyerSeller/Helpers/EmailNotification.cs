using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BuyerSeller.Helpers
{
    public class EmailNotification
    {

        public string SendEmail(string Email, string SellerName, String OrderId)
        {
            MailMessage mailMessage = new MailMessage("contact@ahlehadeeshyd.com", Email);
            mailMessage.Subject = "Harbor Seller Response Notification";
            mailMessage.Body =SellerName+ "has responded to your request "+OrderId+".Please contact the seller and close the Deal."+"\n Thank you";
            SmtpClient smptClient = new SmtpClient("mail.ahlehadeeshyd.com", 26);
            smptClient.Credentials = new NetworkCredential()
            {
                UserName = "contact@ahlehadeeshyd.com",
                Password = "Contact@123"
            };
            // smptClient.EnableSsl = true;
            smptClient.Send(mailMessage);
            return mailMessage.Body;
        }
        
    }
}