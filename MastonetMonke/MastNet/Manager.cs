using Mastonet;
using System.IO;
using System.Reflection;

namespace MastonetMonke.MastNet
{
    public class Manager
    {
        public MastodonClient PublicMastodonClient { get; set; }

        public Manager()
        {
            var instance = "https://tech.lgbt/";
            var accessToken = GetToken();
            var authClient = new AuthenticationClient(instance);
            PublicMastodonClient = new MastodonClient(instance, accessToken);
        }

        public string GetToken()
        {
            Stream initialStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MastonetMonke.MastNet.Token.txt");
            StreamReader streamReader = new StreamReader(initialStream);
            string result = streamReader.ReadToEnd();

            streamReader.Dispose();
            streamReader.Close();
            initialStream.Dispose();
            initialStream.Close();

            return result;
        }
    }
}
