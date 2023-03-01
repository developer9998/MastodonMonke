using Mastonet;
using System.IO;
using BepInEx.Configuration;
using BepInEx;

namespace MastonetMonke.MastNet
{
    public class Manager
    {
        public MastodonClient PublicMastodonClient { get; set; }

        public Manager()
        {
            var customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "MastonetMonke.cfg"), true);
            var instance = customFile.Bind("Login", "Instance", "https://tech.lgbt", "Mastodon instance to use");
            var accessToken = customFile.Bind("Login", "Token", "PutYourTokenHere", "Access token to use. Get this from the Development category in your Mastodon settings. Don't share this with anyone, anybody with this token can freely access your application and usually full write access in turn!!!!!");
            var authClient = new AuthenticationClient(instance.Value);
            PublicMastodonClient = new MastodonClient(instance.Value, accessToken.Value);
        }
    }
}
