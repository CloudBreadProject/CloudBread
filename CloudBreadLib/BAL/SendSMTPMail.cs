using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
//using System;

namespace CloudBreadLib.BAL.SendSMTPMail
{
    public static class SendSMTPMail
    {
        // gmail SMTP 처리 내용
        public static string SendEmail(string toAddress, string subject, string body)
        {
            string result = "메세지 전송 성공";
            string senderID = "보내는사람@gmail.com";  
            const string senderPassword = "보내는사람의 gmail 암호"; 
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // GMail SMTP 서버
                    Port = 587,     // gmail에서 필요시 포트 변경 가능성 있음. gmail 공식 사이트 참조   
                    // https://support.google.com/mail/answer/13287
                    // smtp.gmail.com
                    //Use Authentication: Yes
                    //Port for TLS/STARTTLS: 587
                    //Port for SSL: 465

                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(senderID, toAddress, subject, body);
                smtp.Send(message);
            }

            catch (Exception)
            {
                result = "메일 전송 실패";
                throw;
            }

            return result;
        }
    }
}
