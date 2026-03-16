using HarmonyLib;
using UnityEngine;

namespace MainMenuEnhanced.ReferenceHolder;

[HarmonyPatch(typeof(MainMenuManager) , nameof(MainMenuManager.Start))]
public class ReferencePatches
{
    [HarmonyPostfix]
    public static void Patch(MainMenuManager __instance)
    {
        CustomAssets.ReAssign();
        
        GameObject Ref = new GameObject("ReferenceHolder");
        Ref.AddComponent<ReferenceHolder>();
    }
}