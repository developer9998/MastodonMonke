using Mastonet;
using System.IO;
using BepInEx.Configuration;
using BepInEx;

namespace MastodonMonke.MastNet
{
    public class Manager
    {
        public MastodonClient PublicMastodonClient { get; set; }

        public Manager()
        {
            var customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "MastodonMonke.cfg"), true);
            var instance = customFile.Bind("Login", "Instance", "https://tech.lgbt", "Mastodon instance to use");
            var accessToken = customFile.Bind("Login", "Token", "PutYourTokenHere", "Access token used to post messages to your account. Get this from the Development category in your Mastodon settings. Don't share this with anyone no matter what they say.");

            new AuthenticationClient(instance.Value);
            PublicMastodonClient = new MastodonClient(instance.Value, accessToken.Value);
        }
    }
}
