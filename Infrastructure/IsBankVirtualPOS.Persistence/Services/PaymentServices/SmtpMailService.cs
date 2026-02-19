using IsBankVirtualPOS.Application.Interfaces.PaymentInterfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Persistence.Services.PaymentServices
{
    public class SmtpMailService : ISmtpMailService
    {
        public void SendOtpMail(string to, string otp)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("İş Bankası Doğrulama Kodu", "projectsdotnet1@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", to);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
        .container {{ max-width: 600px; margin: 40px auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }}
        .header {{ background-color: #003087; padding: 24px; text-align: center; }}
        .header h1 {{ color: #ffffff; margin: 0; font-size: 22px; }}
        .body {{ padding: 32px; color: #333333; }}
        .otp-box {{ background-color: #f0f4ff; border: 2px dashed #003087; border-radius: 8px; text-align: center; padding: 20px; margin: 24px 0; }}
        .otp-code {{ font-size: 36px; font-weight: bold; color: #003087; letter-spacing: 8px; }}
        .warning {{ background-color: #fff8e1; border-left: 4px solid #ffc107; padding: 12px 16px; font-size: 13px; color: #555; margin-top: 24px; }}
        .footer {{ background-color: #f9f9f9; padding: 16px; text-align: center; font-size: 12px; color: #999; border-top: 1px solid #eeeeee; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🔐 3D Güvenli Ödeme Doğrulama</h1>
        </div>
        <div class='body'>
            <p>Sayın Müşterimiz,</p>
            <p>Gerçekleştirmekte olduğunuz <strong>3D Secure</strong> ödemesini tamamlamak için aşağıdaki tek kullanımlık şifreyi (OTP) giriniz.</p>
            <div class='otp-box'>
                <p style='margin: 0 0 8px; font-size: 14px; color: #666;'>Tek Kullanımlık Şifreniz</p>
                <div class='otp-code'>{otp}</div>
                <p style='margin: 8px 0 0; font-size: 13px; color: #e53935;'>⏱ Bu kod <strong>5 dakika</strong> içinde geçerliliğini yitirecektir.</p>
            </div>
            <p>Bu işlemi siz başlatmadıysanız lütfen derhal müşteri hizmetlerimizi arayınız.</p>
            <div class='warning'>
                ⚠️ <strong>Güvenlik Uyarısı:</strong> İş Bankası hiçbir zaman telefon veya e-posta yoluyla şifrenizi talep etmez. Bu kodu kimseyle paylaşmayınız.
            </div>
        </div>
        <div class='footer'>
            © {DateTime.Now.Year} Türkiye İş Bankası A.Ş. — Bu e-posta otomatik olarak gönderilmiştir, lütfen yanıtlamayınız.
        </div>
    </div>
</body>
</html>";

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "3D Güvenli Ödeme Doğrulama Kodu";

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("projectsdotnet1@gmail.com", "ivcg jedd rbzr epnm");

            client.Send(mimeMessage);
            client.Disconnect(true);
        }
    }
}
