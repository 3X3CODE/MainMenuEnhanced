using HarmonyLib;
using MainPlugin.Assets;
using MainPlugin.InteractiveMenu;
using MainPlugin.Settings;
using UnityEngine;
using TMPro;

namespace MainPlugin.MenuBackground;

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
        var bg = GameObject.Find("BackgroundTexture");
        ObjectCheck(bg);
        if (bg != null)
        {
            if (bg.TryGetComponent<SpriteRenderer>(out var renderer))
            {
                RendCheck(renderer);
                renderer.maskInteraction = SpriteMaskInteraction.None;
                renderer.sprite = AssetLoader.LoadExternalSprite();
            }
        }
        
        Transform tintTrans = __instance.transform.Find("MainUI/Tint");
        var tint = tintTrans.gameObject;
        ObjectCheck(tint);
        if (tint != null)
        {
            tint.SetActive(false);
        }
        //MiraAPI code if needed in the future

        /*Sprite CustomBG = AssetLoader.LoadEmbeddedSprite("HarPatch.Resources.BG.jpeg");
         bg.sprite = CustomBG;
          bg.sprite = Assets.bg;
          SignalButton.LogSource.LogInfo("BG changed");
         */

        #endregion
        
        #region DisableObjects

            DisableObject("WindowShine");

            #endregion

        #region Remove SRenderers

            DisableComponent("RightPanel");
            DisableComponent("MaskedBlackScreen");

            #endregion

        #region change text

            Transform playTransform = __instance.transform.Find("MainUI/AspectScaler/LeftPanel/Main Buttons/PlayButton/FontPlacer/Text_TMP");
            if (playTransform != null) 
            {
                var playbutton = playTransform.gameObject;
                ObjectCheck(playbutton);
                if (playbutton != null)
                {
                    if (playbutton.TryGetComponent<TextTranslatorTMP>(out var tmp))
                    {
                        tmp.enabled = false;
                    }
                    if (playbutton.TryGetComponent<TextMeshPro>(out var text))
                    {
                        text.text = "Start";
                    }
                }
            }
            #endregion

        #region Methods
            
            static void DisableObject(string name)
            {
                var obj = GameObject.Find(name);
                ObjectCheck(obj);
                if (obj != null)
                {
                    obj.SetActive(false);
                    MainMenuEnhancedPlugin.LogSource.LogInfo(name + " Disabled");
                }
            }

            static void DisableComponent(string name)
            {
                var obj = GameObject.Find(name);
                ObjectCheck(obj);
                if (obj != null)
                {
                    if (obj.TryGetComponent<SpriteRenderer>(out var renderer))
                    {
                        RendCheck(renderer);
                        renderer.enabled = false;
                        MainMenuEnhancedPlugin.LogSource.LogInfo(name + " Renderer disabled");
                    }
                }
            }
            
            
            static void ObjectCheck(GameObject go)
            { 
                if (go == null) 
                {
                    MainMenuEnhancedPlugin.LogSource.LogInfo(go.name + " was null");
                    return;
                }       
            } 
            static void RendCheck(SpriteRenderer go)
            { 
                if (go == null) 
                {
                    MainMenuEnhancedPlugin.LogSource.LogInfo(go.gameObject.name + " was null");
                    return;
                }       
            } 
        #endregion
        
        //GameObject host = new GameObject("particleManager");
        GameObject amb = GameObject.Find("PlayerParticles");
        //host.transform.SetParent(amb.transform);
        amb.AddComponent<ParticleController>();
        
        //GameObject settings = new GameObject("settings");
        //settings.AddComponent<SettingsButton>();
    }
    
}