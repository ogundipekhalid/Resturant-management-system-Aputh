using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDMS.Interface.Service;
using Microsoft.Extensions.Configuration;
using RDMS.Models.Dtos;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace RDMS.Implimentation.Service
{
    public class MailServices : IMailServices
    {
        private readonly IConfiguration _configuration;
        public string _mailApikey;

        public MailServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailApikey = _configuration.GetSection("MailConfig")["mailApikey"];
        }

        public void SendEMailAsync(MailRequestDto mailRequest)
        {
            if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
            {
                Configuration.Default.ApiKey.Add("api-key", _mailApikey);
            }
             var apiInstance = new TransactionalEmailsApi();
            string SenderName = "Lasix Resturant";
            string SenderEmail = "Ogundipekhalid265@gmail.com";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(mailRequest.ToEmail, mailRequest.ToName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>
            {
                smtpEmailTo
            };
             string BccName = "Don khal";
            string BccEmail = "example2@example2.com";
            SendSmtpEmailBcc BccData = new SendSmtpEmailBcc(BccEmail, BccName);
            List<SendSmtpEmailBcc> Bcc = new List<SendSmtpEmailBcc>
            {
                BccData
            };
            string CcName = "Lasix";
            string CcEmail = "example3@example2.com";
            SendSmtpEmailCc CcData = new SendSmtpEmailCc(CcEmail, CcName);
            List<SendSmtpEmailCc> Cc = new List<SendSmtpEmailCc>
            {
                CcData
            };
            string TextContent = null;
            string ReplyToName = "Lasix Resturant";
            string ReplyToEmail = "Ogundipekhalid265@gmail.com";
            SendSmtpEmailReplyTo ReplyTo = new SendSmtpEmailReplyTo(ReplyToEmail, ReplyToName);
            string stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
            string AttachmentUrl = null;
            string AttachmentName = mailRequest.AttachmentName ?? "Welcome.txt";
            byte[] Content = System.Convert.FromBase64String(stringInBase64);
            SendSmtpEmailAttachment AttachmentContent = new SendSmtpEmailAttachment(AttachmentUrl, Content, AttachmentName);
            List<SendSmtpEmailAttachment> Attachment = new List<SendSmtpEmailAttachment>
            {
                AttachmentContent
            };
            JObject Headers = new JObject
            {
                { "Some-Custom-Name", "unique-id-1234" }
            };
             long? TemplateId = null;
            JObject Params = new JObject
            {
                { "parameter", "My param value" },
                { "subject", "Lasix" }
            };
            List<string> Tags = new List<string>
            {
                "mytag"
            };
            SendSmtpEmailTo1 smtpEmailTo1 = new SendSmtpEmailTo1(mailRequest.ToEmail, mailRequest.ToName);
            List<SendSmtpEmailTo1> To1 = new List<SendSmtpEmailTo1>
            {
                smtpEmailTo1
            };
            Dictionary<string, object> _parmas = new Dictionary<string, object>
            {
                { "params", Params }
            };
            SendSmtpEmailReplyTo1 ReplyTo1 = new SendSmtpEmailReplyTo1(ReplyToEmail, ReplyToName);
            SendSmtpEmailMessageVersions messageVersion = new SendSmtpEmailMessageVersions(To1, _parmas, Bcc, Cc, ReplyTo1, mailRequest.Subject);
            List<SendSmtpEmailMessageVersions> messageVersiopns = new List<SendSmtpEmailMessageVersions>
            {
                messageVersion
            };
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, Bcc, Cc, mailRequest.HtmlContent, TextContent, mailRequest.Subject, ReplyTo, Attachment, Headers, TemplateId, Params, messageVersiopns, Tags);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Debug.WriteLine(result.ToJson());
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            
        }
    }
}
