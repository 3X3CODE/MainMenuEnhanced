using HarmonyLib;
using UnityEngine;

namespace MainPlugin.Settings;

[HarmonyPatch(typeof(MainMenuManager) , nameof(MainMenuManager.Start))]
public class SettingsPatch
{
    [HarmonyPostfix]
    public static void Patch(MainMenuManager __instance)
    {
        GameObject Settings = new GameObject("settings");
        Settings.AddComponent<SettingsButton>();
    }
}