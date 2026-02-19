using System.IO;
using BepInEx;
using UnityEngine;

namespace MainPlugin;

public static class CustomPaths
{
    public static string pluginPath = Paths.PluginPath;
    public static string winFolderPath = Path.Combine(Paths.PluginPath, "MainMenuEnhanced");
    public static string androidFolderPath = Path.Combine(Application.persistentDataPath, "MainMenuEnhanced");
    public static string winJsonPath = Path.Combine(winFolderPath, "MMEconfig.json");
    public static string androidJsonPath = Path.Combine(androidFolderPath, "MMEconfig.json");
    public static string winXmlPath = Path.Combine(winFolderPath, "config.xml");
    public static string androidXmlPath = Path.Combine(androidFolderPath, "config.xml");
}