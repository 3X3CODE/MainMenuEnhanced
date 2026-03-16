using System;
using MainMenuEnhanced.Assets;
using UnityEngine;

namespace MainMenuEnhanced;

public static class CustomAssets
{
    public static readonly string BundleName = OperatingSystem.IsAndroid() ? "menu-android" : "menu";
    public static GameObject SettingsButton = AssetLoader.LoadAsset(BundleName, "SettingsButton");
    public static GameObject SettingsMenu = AssetLoader.LoadAsset(BundleName, "SettingsMenu");

    public static void ReAssign()
    {
        if(SettingsButton == null || SettingsMenu == null)
        {
            SettingsButton = AssetLoader.LoadAsset(BundleName, "SettingsButton");
            SettingsMenu = AssetLoader.LoadAsset(BundleName, "SettingsMenu");
        }
    }
}