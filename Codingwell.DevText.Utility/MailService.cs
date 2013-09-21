using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Codingwell.DevText.Utility {
	public class MailService {

		readonly static string SmtpHost = ConfigUtility.GetAppSettingValue("SmtpHost");
		readonly static string SmtpUser = ConfigUtility.GetAppSettingValue("SmtpUser");
		readonly static string SmtpPassword = ConfigUtility.GetAppSettingValue("SmtpPassword");
		//readonly static string SmtpUserFrom = ConfigUtility.GetAppSettingValue("SmtpUserFrom");
		readonly static bool IsEnableSsl = ConfigUtility.GetAppSettingValue("EnableSsl").ToLower() == "true";
		readonly static int SslPort = Convert.ToInt32(ConfigUtility.GetAppSettingValue("SslPort"));

		/// <summary>
		/// 发送邮件
		/// </summary>
		/// <param name="subject"></param>
		/// <param name="mailbody"></param>
		/// <param name="to"></param>
		/// <param name="cc"></param>
		/// <param name="bcc"></param>
		public static void SendMail ( string subject, string mailbody, string to, string cc, string bcc ) {
			if (string.IsNullOrEmpty(to))
				return;
			MailMessage message = new MailMessage();
			message.From = new MailAddress(SmtpUser, "Devtext.com");
			message.Subject = subject;
			message.Body = mailbody;
			message.BodyEncoding = Encoding.UTF8;
			message.IsBodyHtml = true;
			message.To.Add(new MailAddress(to));
			if (!string.IsNullOrEmpty(cc)) {
				message.CC.Add(new MailAddress(cc));
			}
			if (!string.IsNullOrEmpty(bcc)) {
				message.CC.Add(new MailAddress(bcc));
			}

			SmtpClient client = new SmtpClient() {
				Host = SmtpHost,
				Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPassword),
				DeliveryMethod = SmtpDeliveryMethod.Network,
				EnableSsl = IsEnableSsl,
				Port = SslPort
			};
			try {
				client.Send(message);
			} catch (Exception ex) {
				string dd = ex.Message;
			}
		}
	}
}
