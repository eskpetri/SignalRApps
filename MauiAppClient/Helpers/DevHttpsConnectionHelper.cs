using System.Net.Security;

namespace MauiAppClient.Helpers;
//This is only for debug. bypasses Android emulator and Android device ssl certificate verify test
public class DevHttpsConnectionHelper
{
    public DevHttpsConnectionHelper(int sslPort)
    {
        SslPort = sslPort;
        //DevServerRootUrl = FormattableString.Invariant($"https://{DevServerName}:{SslPort}");
        DevServerRootUrl = FormattableString.Invariant($"https://{DevServerName}");              //Heroku don't use port
        LazyHttpClient = new Lazy<HttpClient>(() => new HttpClient(GetPlatformMessageHandler()));
    }
    public DevHttpsConnectionHelper(int sslPort, string hostname)
    {
        SslPort = sslPort;
        //DevServerRootUrl = FormattableString.Invariant($"https://{hostname}:{SslPort}");
        DevServerRootUrl = FormattableString.Invariant($"https://{hostname}");                //Heroku don't use port
        LazyHttpClient = new Lazy<HttpClient>(() => new HttpClient(GetPlatformMessageHandler()));
    }
    public int SslPort { get; }

    public string DevServerName =>
#if WINDOWS
        "pete-signalr.herokuapp.com";           // Use strings to make a twitch in one place
#elif ANDROID
        "pete-signalr.herokuapp.com";         //Here check if virtual or actual device and make it better
#else
        throw new PlatformNotSupportedException("Only Windows and Android currently supported.");
#endif

    public string DevServerRootUrl { get; }

    private Lazy<HttpClient> LazyHttpClient;
    public HttpClient HttpClient => LazyHttpClient.Value;

    public HttpMessageHandler GetPlatformMessageHandler()
    {
#if WINDOWS
        return null;
#elif ANDROID
        var handler = new CustomAndroidMessageHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            Console.WriteLine("GetPlatformMessageHandler message="+message+" cert"+cert+" chain"+chain+" errors"+errors);
            if (cert != null && cert.Issuer.Equals("CN=localhost"))
                return true;
            return errors == SslPolicyErrors.None;
        };
        return handler;

#else
        throw new PlatformNotSupportedException("Only Windows and Android currently supported.");
#endif
    }

#if ANDROID
    internal sealed class CustomAndroidMessageHandler : Xamarin.Android.Net.AndroidMessageHandler
    {
        protected override Javax.Net.Ssl.IHostnameVerifier GetSSLHostnameVerifier(Javax.Net.Ssl.HttpsURLConnection connection)
            => new CustomHostnameVerifier();

        private sealed class CustomHostnameVerifier : Java.Lang.Object, Javax.Net.Ssl.IHostnameVerifier
        {
            public bool Verify(string hostname, Javax.Net.Ssl.ISSLSession session)
            {
                Console.WriteLine("AndroidVerifySSL hostname=" + hostname+" session="+session.ToString+" Peer=" +session.PeerPrincipal?.Name);
                return
                    Javax.Net.Ssl.HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session)
                    || hostname == "10.0.2.2" && session.PeerPrincipal?.Name == "CN=localhost" || hostname == "127.0.0.1" && session.PeerPrincipal?.Name == "CN=localhost" 
                    || hostname == "pete-signalr.herokuapp.com" ;
            }
        }
    }
#endif
}