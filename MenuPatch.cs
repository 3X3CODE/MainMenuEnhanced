using HarmonyLib;
using UnityEngine;
using TMPro;
using Object = UnityEngine.Object;

namespace HarPatch;

// Resharper disable once InconsistentNaming
[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]

public class MenuPatch : MonoBehaviour
{
    [HarmonyPostfix]
    public static void Patch(MainMenuManager __instance)
    {
        Transform divider = __instance.transform.Find("MainUI/AspectScaler/RightPanel/MaskedBlackScreen/GameModeButtons/Divider"); 
        Transform header = __instance.transform.Find("MainUI/AspectScaler/RightPanel/MaskedBlackScreen/GameModeButtons/Header/Text_TMP"); 
        if (divider != null) divider.gameObject.SetActive(false);
                    
        if (header != null) 
        {
            var trans = header.gameObject.GetComponent<TextTranslatorTMP>();
            var text = header.gameObject.GetComponent<TextMeshPro>();
            if (trans != null)
            {
                Object.Destroy(trans);
            }
            if (text != null)
            {
                text.text = "Main Menu Enhanced";
            }
        }

    }
}