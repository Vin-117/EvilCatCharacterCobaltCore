using Nanoray.PluginManager;
using Nickel;

namespace EvilCat;

internal interface IRegisterable
{
    static abstract void Register(IPluginPackage<IModManifest> package, IModHelper helper);
}