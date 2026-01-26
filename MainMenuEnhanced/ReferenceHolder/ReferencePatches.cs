using HarmonyLib;
using MainPlugin.JSONreader;
using UnityEngine;

namespace MainPlugin.Settings;

[HarmonyPatch(typeof(MainMenuManager) , nameof(MainMenuManager.Start))]
public class ReferencePatches
{
    [HarmonyPostfix]
    public static void Patch(MainMenuManager __instance)
    {
        GameObject Ref = new GameObject("ReferenceHolder");
        Ref.AddComponent<ReferenceHolder>();
    }
}