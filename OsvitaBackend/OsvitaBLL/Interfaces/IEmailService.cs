using System;
using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface IEmailService
	{
        public Task SendEmailAsync(string email, string subject, string message, IEnumerable<EmailAttachmentModel> attachments = null);
    }
}

