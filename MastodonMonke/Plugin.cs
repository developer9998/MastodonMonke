using BepInEx;
using Bepinject;
using MastodonMonke.MastNet;
using MastodonMonke.MastNet_CI;

namespace MastodonMonke
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }
        public Manager MastManager { get; private set; }

        public void Awake()
        {
            Instance = this;
            MastManager = new Manager();

            Zenjector.Install<MainInstaller>().OnProject();
        }
    }
}
