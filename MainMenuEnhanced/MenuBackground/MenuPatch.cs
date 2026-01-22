using HarmonyLib;
using UnityEngine;
using TMPro;

namespace MainPlugin.MenuBackground;

// Resharper disable once InconsistentNaming
[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]

public class MenuPatch : MonoBehaviour
{
    [HarmonyPostfix]
    public static void Patch(MainMenuManager __instance)
    {
        if (__instance == null) return;
        Transform divider = __instance.transform.Find("MainUI/AspectScaler/RightPanel/MaskedBlackScreen/GameModeButtons/Divider"); 
        Transform headerTrans = __instance.transform.Find("MainUI/AspectScaler/RightPanel/MaskedBlackScreen/GameModeButtons/Header/Text_TMP"); 
        if (divider != null) divider.gameObject.SetActive(false);
                    
        if (headerTrans != null)
        {
            var HeaderObject = headerTrans.gameObject;
            if (HeaderObject != null)
            {
                if (HeaderObject.TryGetComponent<TextTranslatorTMP>(out var tmp))
                {
                    tmp.enabled = false;
                }
                if (HeaderObject.TryGetComponent<TextMeshPro>(out var text))
                {
                    text.text = "Begin";
                }
            }
        }
    }
}