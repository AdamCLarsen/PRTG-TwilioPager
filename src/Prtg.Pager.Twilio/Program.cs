using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Twilio;

namespace Prtg.Pager.Twilio
{
	class Program
	{
		static void Main(string[] args)
		{
			var trc = new TwilioRestClient(ConfigurationManager.AppSettings["TwilioAccountSid"],
										   ConfigurationManager.AppSettings["TwilioAuthToken"]);
			var fromNumber = ConfigurationManager.AppSettings["TwilioNumber"];
			var to = CheckAndFormatPhone(args[0]);
			var message = string.Join(" ", args.Skip(1));

			Console.WriteLine("From: {0}\r\nTo: {1}\r\nMessage: \"{2}\"", fromNumber ?? "NULL", to ?? "NULL", message ?? "NULL");

			if (string.IsNullOrEmpty(fromNumber) || string.IsNullOrEmpty(to) || string.IsNullOrEmpty(message))
			{
				Console.WriteLine("Skipping, argument missing.");
				return;
			}

			var messageResponse = trc.SendSmsMessage(fromNumber, to, message);
			if (messageResponse == null)
			{
				throw new ApplicationException("Didn't get a response from SMS provider.");
			}

			Console.WriteLine("Message sent: {0} - {1}", messageResponse.Status, messageResponse.Sid);
		}

		public static string CheckAndFormatPhone(string phone)
		{
			var checkAndFormatPhoneRegex = new Regex(@"^(\+?1[ -.]?)?\(?([0-9]{3})\)?[ -.]?([0-9]{3})[ -.]?([0-9]{4})$", RegexOptions.Compiled);
			if (string.IsNullOrEmpty(phone))
			{
				throw new InvalidDataException("Phone number is not in a valid format.");
			}

			var match = checkAndFormatPhoneRegex.Match(phone);
			if (!match.Success)
			{
				throw new InvalidDataException("Phone number is not in a valid format.");
			}

			return string.Join(string.Empty, "+1", match.Groups[2], match.Groups[3], match.Groups[4]);
		}
	}
}
