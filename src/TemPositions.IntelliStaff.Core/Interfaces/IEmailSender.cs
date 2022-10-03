using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.Interfaces;

public interface IEmailSender
{
  Task SendEmailAsync(string to, string from, string subject, string body);
}
