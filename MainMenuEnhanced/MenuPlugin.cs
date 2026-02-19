using System.IO;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Reactor.Utilities;
using BepInEx.Logging;
using BepInEx.Configuration;
using MainMenuEnhanced.Settings;

namespace MainPlugin;

// Developed by 3X3C | 2026.01.12 | My First Mod:MainMenuEnhanced

[BepInProcess("Among Us.exe")]
[BepInAutoPlugin]
[BepInDependency("gg.reactor.api")]
public partial class MainMenuEnhancedPlugin : BasePlugin
{
    
    public static new ManualLogSource LogSource = null!;
    public Harmony Harmony { get; } = new(Id);
    public string OptionsTitleText => "Menu Enhanced";
    public ConfigFile GetConfigFile() => Config;

    public static ConfigFile config;

    public static ConfigEntry<CustomSettings> BackgroundMode;
    public static ConfigEntry<CustomSettings> WindowMode;

    private string folderPath = Path.Combine(Paths.PluginPath, "MainMenuEnhanced");
    
    public override void Load()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        config = Config;
        
        BackgroundMode = Config.Bind("Background", "background", CustomSettings.BackgroundDefault,
            "What you want your menu background to be");
        WindowMode = Config.Bind("Window", "window", CustomSettings.WindowActive,
            "Whether you want the menu window to be active");

        LogSource = base.Log;
   
        ReactorCredits.Register("MainMenuEnhanced", "0.4.0", false, null);
        Harmony.PatchAll();
        LogSource.LogInfo("MainMenuEnhanced Loaded");
        
    }

    
}




