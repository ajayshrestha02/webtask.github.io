using EVJ.Infrastructure.Models;

namespace EVJ.Infrastructure.Interface
{
	public interface IEmailSender
	{
		void SendEmail(Message message);
		void SendEmailViaOldSMTP(string toEmail,string subject, string mailBody);
		Task SendEmailAsync(Message message);
	}
}
