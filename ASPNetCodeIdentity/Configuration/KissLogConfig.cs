using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetCodeIdentity.Configuration
{

    /// <summary>
    /// KissLog Configuration
    /// </summary>
    public class KissLogConfig
    {

        private readonly IConfiguration _configuration;

        public KissLogConfig(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureKissLog(IOptionsBuilder options)
        {
            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            // register logs output
            RegisterKissLogListeners(options);
        }

        public void RegisterKissLogListeners(IOptionsBuilder options)
        {
            // multiple listeners can be registered using options.Listeners.Add() method

            // register KissLog.net cloud listener
            options.Listeners.Add(new RequestLogsApiListener(new Application(
                _configuration["KissLog.OrganizationId"],    //  "0b68ad5a-c55f-46fb-8aa1-772c6f3acb1c"
                _configuration["KissLog.ApplicationId"])     //  "e0f9c967-186c-48fe-8a40-8ae2887b2126"
            )
            {
                ApiUrl = _configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
            });
        }

    }
}
