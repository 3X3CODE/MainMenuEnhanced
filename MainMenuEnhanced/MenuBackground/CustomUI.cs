using HarmonyLib;
using UnityEngine;
using TMPro;

namespace MainPlugin.MenuBackground;

// Resharper disable once InconsistentNaming
[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
public class CustomUI
{
    [HarmonyPostfix]
    public static void ModUI(MainMenuManager __instance)
    {
        if (__instance == null) return;
        GameObject Text = GameObject.Find("ReactorVersion");
        if (Text != null)
        { 
            //GameObject ModText = GameObject.Instantiate(Text, __instance.transform.Find("MainUI"));
            GameObject ModText = GameObject.Instantiate(Text);
            ModText.name = "MMEtext";
            //if (ModText.TryGetComponent<TextTranslatorTMP>(out var trans)) trans.enabled = false;
            var Text_TMP = ModText.GetComponent<TextMeshPro>();
            Text_TMP.text = "Main Menu Enhanced";
            Text_TMP.fontSize = 3;
            var place = ModText.GetComponent<RectTransform>();
            place.anchoredPosition = new Vector3(9f, -0.1f, 8f);
        }
    }
}