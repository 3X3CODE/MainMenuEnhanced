using HarmonyLib;
using UnityEngine;
using TMPro;
using Object = UnityEngine.Object;
using MiraAPI.Utilities.Assets;

namespace HarPatch;

// Resharper disable once InconsistentNaming
[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
public class CustomUI
{
    [HarmonyPostfix]
    public static void ModUI(MainMenuManager __instance)
    {
        GameObject Text = GameObject.Find("Text_TMP");
        if (Text != null)
        {
            GameObject ModText = GameObject.Instantiate(Text, __instance.transform.Find("MainUI"));
            if (ModText.TryGetComponent<TextTranslatorTMP>(out var trans)) Object.Destroy(trans);
            var Text_TMP = ModText.GetComponent<TextMeshPro>();
            Text_TMP.text = "Main Menu Enhanced";
            Text_TMP.fontSize = 20;
            ModText.transform.localPosition = new Vector3(0.44f, 0f, -2f);
        }
    }
}