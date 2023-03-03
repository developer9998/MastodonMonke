using BepInEx;
using Bepinject;
using MastonetMonke.MastNet;
using MastonetMonke.MastNet_CI;

namespace MastonetMonke
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
