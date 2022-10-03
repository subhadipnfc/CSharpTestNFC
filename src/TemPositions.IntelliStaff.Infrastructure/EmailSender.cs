using System.Net.Mail;
using TemPositions.IntelliStaff.Core.Interfaces;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace TemPositions.IntelliStaff.Infrastructure;

public class EmailSender : IEmailSender
{
  private readonly EmailConfiguration _emailConfig;
  private readonly ILogger<EmailSender> _logger;

  public EmailSender(ILogger<EmailSender> logger, EmailConfiguration emailConfig)
  {
    _logger = logger;
    this._emailConfig=emailConfig;
  }

  public async Task SendEmailAsync(string to, string from, string subject, string body)
  {
    var server = _emailConfig.SmtpServer;
    var port = _emailConfig.Port;
    var fromEmail = _emailConfig.From;

    if (server == "none")
      return;

    var mailServer = new SmtpClient(server, Convert.ToInt32(port));
    var msg = new MailMessage();

    var Defaulcredential = false;

    if (Defaulcredential)
      mailServer.UseDefaultCredentials = true;
    else
    {
      var username = _emailConfig.UserName;
      var password = _emailConfig.Password;
      mailServer.Credentials = new System.Net.NetworkCredential(username, password);
    }


    msg.From = new MailAddress(fromEmail);

    msg.IsBodyHtml = true;

    msg.Subject = subject;
    msg.Body = body;

    var bcc = string.Empty;

    //var seps = new char[] { ',', ';' };
    //foreach (var ss in _JobPostEmailConfiguration.BccEmailAddress.Split(seps))
    //{
    //  if (ss.Length != 0)
    //  {
    //    bcc += ss[0];
    //    bcc += (ss.Substring(1, ss.Length - 1));
    //    msg.Bcc.Add(bcc);
    //    bcc = string.Empty;
    //  }
    //}

    //foreach (var ss in _JobPostEmailConfiguration.CCEmailAddress.Split(seps))
    //{
    //  if (ss.Length != 0)
    //  {
    //    bcc += ss[0];
    //    bcc += (ss.Substring(1, ss.Length - 1));
    //    msg.Bcc.Add(bcc);
    //    bcc = string.Empty;
    //  }
    //}

    var toadd = string.Empty;
    //foreach (var ss in _JobPostEmailConfiguration.ToEmailAddress.Split(seps))
    //{
    //  if (ss.Length != 0)
    //  {
    //    toadd += ss[0];
    //    toadd += (ss.Substring(1, ss.Length - 1));
    //    msg.To.Add(toadd);
    //    toadd = string.Empty;
    //  }
    //}
    var disableemail = false;
    if (!disableemail)
    {
      mailServer.Send(msg);
    }

    mailServer = null;
    _logger.LogWarning($"Sending email to {to} from {from} with subject {subject}.");
  }
 
}
