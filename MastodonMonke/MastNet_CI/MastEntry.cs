using System;
using ComputerInterface.Interfaces;

namespace MastodonMonke.MastNet_CI
{
    public class MastEntry : IComputerModEntry
    {
        public string EntryName => PluginInfo.Name;
        public Type EntryViewType => typeof(MastView);
    }
}