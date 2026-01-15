using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Reactor.Utilities;
using BepInEx.Logging;

namespace HarPatch;

// Developed by 3X3C | 2026.01.12 | My First Mod:MainMenuEnhanced

[BepInProcess("Among Us.exe")]
[BepInPlugin("com.3x3c.MainMenuEnhanced", "Main Menu Enhanced", "0.1.2")]
[BepInDependency("gg.reactor.api")]
public class MainMenuEnhancedPlugin : BasePlugin
{
    
    public static new ManualLogSource LogSource = null!;
    public Harmony Harmony { get; } = new("com.3x3c.MainMenuEnhanced");
    //public string OptionsTitleText => "Menu Enhanced";
    //public ConfigFile GetConfigFile() => Config;
    
    
    public override void Load()
    {
        LogSource = base.Log;
        //ExampleEventHandlers.Initialize();
        ReactorCredits.Register("MainMenuEnhanced", "0.1.2", false, null);
        Harmony.PatchAll();
        LogSource.LogInfo("MenuLoaded");
    }
    
}


