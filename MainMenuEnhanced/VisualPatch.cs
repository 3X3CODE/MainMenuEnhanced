using HarmonyLib;
using UnityEngine;
using TMPro;
using Object = UnityEngine.Object;

namespace HarPatch;

// Resharper disable once InconsistentNaming
[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
public static class RemoveBg
{
    [HarmonyPostfix]
    public static void Removebg(MainMenuManager __instance)
    {
        if (__instance == null) return;

            
            #region ScreenMask

            // Disable mask on MainMenu
            var bg = GameObject.Find("BackgroundTexture").GetComponent<SpriteRenderer>();
            
            if (bg != null)
            {
                bg.maskInteraction = SpriteMaskInteraction.None;
                bg.sprite = AssetLoader.LoadExternalSprite();

                /*Sprite CustomBG = AssetLoader.LoadEmbeddedSprite("HarPatch.Resources.BG.jpeg");
                bg.sprite = CustomBG;*/
                // bg.sprite = Assets.bg;
                // SignalButton.LogSource.LogInfo("BG changed");
            }

            #endregion

            #region DisableObjects

            DisableObject("WindowShine");

            #endregion

            #region Remove SRenderers

            DisableComponent("RightPanel");
            DisableComponent("MaskedBlackScreen");

            #endregion

            #region change text

            // rename playbutton
            var playbuttonText = GameObject.Find("PlayButton").GetComponentInChildren<TextMeshPro>();
            var playbuttonTrans = GameObject.Find("PlayButton").GetComponentInChildren<TextTranslatorTMP>();
            if (playbuttonText != null && playbuttonTrans != null)
            {
                Object.Destroy(playbuttonTrans);
                playbuttonText.text = "MainMenuEnhanced";
            }
            else MainMenuEnhancedPlugin.LogSource.LogInfo("Translator or Text not found");

            var butText = GameObject.Find("PlayButton").GetComponentInChildren<TextMeshPro>();
            var butTrans = GameObject.Find("PlayButton").GetComponentInChildren<TextTranslatorTMP>();
            if (butText != null && butTrans != null)
            {
                Object.Destroy(butTrans);
                butText.text = "Start";
            }
            else MainMenuEnhancedPlugin.LogSource.LogInfo("Button Text or Translator not found");

            #endregion


            #region Methods
            
            static void DisableObject(string name)
            {
                var obj = GameObject.Find(name);
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }

            static void DisableComponent(string name)
            {
                var obj = GameObject.Find(name);
                if (obj != null)
                {
                    if (obj.TryGetComponent<SpriteRenderer>(out var renderer))
                    {
                        Object.Destroy(renderer);
                        MainMenuEnhancedPlugin.LogSource.LogInfo(name + " Renderer destroyed");
                    }
                }
            }
            #endregion
        
       
    }
}