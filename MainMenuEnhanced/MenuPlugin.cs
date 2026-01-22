using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Reactor.Utilities;
using BepInEx.Logging;
using MiraAPI.PluginLoading;
using BepInEx.Configuration;
using UnityEngine.SceneManagement;

namespace MainPlugin;

// Developed by 3X3C | 2026.01.12 | My First Mod:MainMenuEnhanced

[BepInProcess("Among Us.exe")]
[BepInPlugin("com.3x3c.MainMenuEnhanced", "Main Menu Enhanced", "0.1.2")]
[BepInDependency("gg.reactor.api")]
[BepInDependency("mira.api")]
public class MainMenuEnhancedPlugin : BasePlugin, IMiraPlugin
{
    public static MainMenuEnhancedPlugin Instance;
    
    public static new ManualLogSource LogSource = null!;
    public Harmony Harmony { get; } = new("com.3x3c.MainMenuEnhanced");
    public string OptionsTitleText => "Menu Enhanced";
    public ConfigFile GetConfigFile() => Config;

    //public static AssetBundle MyBundle;
    //public GameObject particle;
    //public GameObject particleManager;
    
    
    public override void Load()
    {
        LogSource = base.Log;
        //ExampleEventHandlers.Initialize();
        ReactorCredits.Register("MainMenuEnhanced", "0.1.2", false, null);
        Harmony.PatchAll();
        LogSource.LogInfo("MenuLoaded");
        //Il2CppInterop.Runtime.Injection.ClassInjector.RegisterTypeInIl2Cpp<ParticleManager>();
        SceneManager.add_sceneLoaded(new System.Action<Scene, LoadSceneMode>(OnSceneLoaded));
       

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            /*GameObject host = new GameObject("particleManager");
            GameObject amb = GameObject.Find("Ambience");
            host.transform.SetParent(amb.transform);
            host.AddComponent<ParticleManager>();*/
            //host.AddComponent<BaseParticle>();
            //UnityEngine.Object.DontDestroyOnLoad(host);
            //particle.StartCoroutine(SpawnParticle());
        }
    }
}


