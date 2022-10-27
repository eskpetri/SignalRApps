using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppClient.Platforms.Android;
internal class BypassCertificate
{
    // This method must be in a class in a platform project, even if
    // the HttpClient object is constructed in a shared project.
    public HttpClientHandler GetInsecureHandler()           //this works only in HttpClient not in SignalR as far I know it. Maybe there could be a way.
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (cert.Issuer.Equals("CN=localhost"))
                return true;
            return errors == System.Net.Security.SslPolicyErrors.None;
        };
        return handler;
    }
}
