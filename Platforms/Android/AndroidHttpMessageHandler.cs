using AndroidX.ConstraintLayout.Core.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;

namespace FinanceMAUI
{
    public class AndroidHttpMessageHandler : IPlatformHttpMessageHandler
    {
        public HttpMessageHandler GetHttpMessageHandler() => new AndroidMessageHandler
        {
            ServerCertificateCustomValidationCallback = (HttpRequestMessage, certificate, Chain, sslPolicyErrors) =>
            certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None
        };
    }
}
