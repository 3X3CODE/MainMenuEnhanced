using HarmonyLib;
using UnityEngine;

namespace MainMenuEnhanced.Settings;

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