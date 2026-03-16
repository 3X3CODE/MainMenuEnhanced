using System;
using System.IO;
using BepInEx;
using UnityEngine;

namespace MainMenuEnhanced;

public static class CustomPaths
{
    public static string winFolderPath = Path.Combine(Paths.PluginPath, "MainMenuEnhanced");
    public static string androidFolderPath = Path.Combine(Application.persistentDataPath, "MainMenuEnhanced");
    public static string winXmlPath = Path.Combine(winFolderPath, "config.xml");
    public static string androidXmlPath = Path.Combine(androidFolderPath, "config.xml");

    public static string ModFolder = OperatingSystem.IsAndroid() ? androidFolderPath : winFolderPath;
    public static string XmlPath = OperatingSystem.IsAndroid() ? androidXmlPath : winXmlPath;
}