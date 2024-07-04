using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Interface.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailResponeToJoinTrip(MessageRequest request);
    }

    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }

    public class MessageRequest
    {
        public string ToEmail { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
    }
}