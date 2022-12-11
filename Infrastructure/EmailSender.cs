using EVJ.Configs;
using EVJ.Infrastructure.Interface;
using EVJ.Infrastructure.Models;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net;

namespace EVJ.Infrastructure
{
	public class EmailSender:IEmailSender
	{
		private readonly EmailConfig _emailConfig;
		public EmailSender(EmailConfig emailConfig)
		{
			_emailConfig = emailConfig;
		}
		public void SendEmail(Message message)
		{
			var emailMessage = CreateEmailMessage(message);
			Send(emailMessage);
		}
		public async Task SendEmailAsync(Message message)
		{
			var mailMessage = CreateEmailMessage(message);
			await SendAsync(mailMessage);
		}
		private async Task SendAsync(MimeMessage mailMessage)
		{
			using (var client = new SmtpClient())
			{
				try
				{
					await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
					client.AuthenticationMechanisms.Remove("XOAUTH2");
					await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

					await client.SendAsync(mailMessage);
				}
				catch
				{
					//log an error message or throw an exception, or both.
					throw;
				}
				finally
				{
					await client.DisconnectAsync(true);
					client.Dispose();
				}
			}
		}
		private MimeMessage CreateEmailMessage(Message message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
			emailMessage.To.AddRange(message.To);
			emailMessage.Subject = message.Subject;
			emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

			return emailMessage;
		}

		private void Send(MimeMessage mailMessage)
		{
			using (var client = new SmtpClient())
			{
				try
				{
					client.CheckCertificateRevocation = false;
					client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
					client.AuthenticationMechanisms.Remove("XOAUTH2");
					client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
					var success=client.Send(mailMessage);
				}
				catch(Exception ex)
				{
					//log an error message or throw an exception or both.
					throw ex;
				}
				finally
				{
					client.Disconnect(true);
					client.Dispose();
				}
			}
		}

		public void SendEmailViaOldSMTP(string toEmail, string subject, string mailBody)
		{
			using (var client = new System.Net.Mail.SmtpClient(_emailConfig.SmtpServer, _emailConfig.Port))
			{
				try
				{
					client.EnableSsl= true;
					client.Timeout = 20000;
					client.UseDefaultCredentials = false;
					client.Credentials = new NetworkCredential(_emailConfig.From, _emailConfig.Password);
					var msg = new System.Net.Mail.MailMessage();
					msg.From=new System.Net.Mail.MailAddress(_emailConfig.From);
					msg.To.Add(toEmail);
					msg.Subject = "Message to Executive Voice";
					msg.Body = mailBody;
					msg.IsBodyHtml= true;
					client.Send(msg);
				}
				catch (Exception ex)
				{
					//log an error message or throw an exception or both.
					throw ex;
				}
	
			}
		}
	}
}
