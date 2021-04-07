using System;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Security;

namespace MongoIdent.Intrastructure
{

  //public interface IEmailSender
  //{
  //  Task SendEmailAsync(
  //    string email, string subject, string message);
  //}


  /* Implement below using MailKit https://github.com/jstedfast/MailKit */


  public class EmailSender : IEmailSender
{
  public Task SendEmailAsync(string email, string subject, string message)
  {
      var msg = new MimeMessage();
      msg.From.Add(new MailboxAddress("devprowork@live.com", "devprowork@live.com"));
      msg.To.Add(new MailboxAddress("devprohome@live.com", "devprohome@live.com"));
      msg.Subject = subject;

      msg.Body = new TextPart("html")
      {
        Text = message
      };

      using (var client = new SmtpClient())
      {
        client.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);

        // Note: only needed if the SMTP server requires authentication
        client.Authenticate("devprowork@live.com", "de@dinside$70");

        client.Send(msg);
        client.Disconnect(true);
      }







      return Task.CompletedTask;
  }
}



////////public class EmailSender : IEmailSender
////////{

////////  // Our private configuration variables
////////  private string host;
////////  private int port;
////////  private bool enableSSL;
////////  private string userName;
////////  private string password;

////////  // Get our parameterized configuration
////////  public EmailSender(string host, int port, bool enableSSL, string userName, string password)
////////  {
////////    this.host = host;
////////    this.port = port;
////////    this.enableSSL = enableSSL;
////////    this.userName = userName;
////////    this.password = password;
////////  }

////////  // Use our configuration to send the email by using SmtpClient
////////  public Task SendEmailAsync(string email, string subject, string htmlMessage)
////////  {


////////    var client = new SmtpClient(host, port)
////////    {
////////      Credentials = new NetworkCredential(userName, password),
////////      EnableSsl = enableSSL
////////    };
////////    return client.SendMailAsync(
////////        new MailMessage(userName, email, subject, htmlMessage) { IsBodyHtml = true }
////////    );
////////  }
////////}
}



/*

"EmailSender": {
  "Host": "smtp.office365.com",
  "Port": 587,
  "EnableSSL": true,
  "UserName": "your@username.com",
  "Password":  "Y0urP4ssw0rd!!!"
},


  https://support.office.com/en-us/article/pop-imap-and-smtp-settings-for-outlook-com-d088b986-291d-42b8-9564-9c414e2aa040

*/


/* https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-5.0&tabs=visual-studio */