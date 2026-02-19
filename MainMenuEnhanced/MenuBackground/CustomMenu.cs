using MainMenuEnhanced.Assets;
using MainMenuEnhanced.JSONreader;
using MainMenuEnhanced.Settings;
using MainPlugin;
using Reactor.Utilities.Attributes;
using TMPro;
using UnityEngine;

namespace MainMenuEnhanced.MenuBackground;

[RegisterInIl2Cpp]
public class CustomMenu : MonoBehaviour
{
    private static GameObject bg;
    private static SpriteRenderer BGrend;
    private static GameObject manager;
    private static GameObject tint;
    private static GameObject windowShine;
    private static SpriteRenderer rightPanel;
    private static SpriteRenderer maskedScreen;
    private static Sprite bgSprite;
    private static Sprite customSprite;
    
    private void Start()
    {
        manager = GameObject.Find("MainMenuManager");
        tint = manager.transform.Find("MainUI/Tint").gameObject;
        SpriteRenderer tintrend = tint.GetComponent<SpriteRenderer>();
        tintrend.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        tintrend.sortingOrder = -2;
        windowShine = GameObject.Find("WindowShine");
        rightPanel = GameObject.Find("RightPanel").GetComponent<SpriteRenderer>();
        rightPanel.sortingOrder = -2;
        maskedScreen = GameObject.Find("MaskedBlackScreen").GetComponent<SpriteRenderer>();
        maskedScreen.sortingOrder = -2;
        
        bg = FindObjectOfType<MainMenuManager>().transform.Find("MainUI/AspectScaler/BackgroundTexture").gameObject;
        bg.transform.SetParent(transform);
        BGrend = bg.GetComponent<SpriteRenderer>();
        BGrend.sortingOrder = -3;
        bgSprite = BGrend.sprite;
        customSprite = AssetLoader.LoadExternalSprite();
        
        ApplyBGSettings();
        
        GameObject playTransform = manager.transform.Find("MainUI/AspectScaler/LeftPanel/Main Buttons/PlayButton/FontPlacer/Text_TMP").gameObject;
        if (playTransform != null) 
        {
            if (playTransform.TryGetComponent<TextTranslatorTMP>(out var tmp))
            {
                tmp.enabled = false;
            }
            if (playTransform.TryGetComponent<TextMeshPro>(out var text))
            {
                text.text = "Start";
            }
        }

        MeshRenderer stars = GameObject.Find("starfield").GetComponent<MeshRenderer>();
        stars.sortingOrder = -5;

    }
    
    public static void ApplyBGSettings()
    {
        switch (MainMenuEnhancedPlugin.BackgroundMode.Value)
        {
            case CustomSettings.BackgroundDefault:
                BGrend.enabled = true;
                BGrend.sprite = bgSprite;
                break;
            case CustomSettings.BackgroundCustom:
                BGrend.enabled = true;
                if (customSprite == null) customSprite = AssetLoader.LoadExternalSprite();
                BGrend.sprite = customSprite;
                break;
            case CustomSettings.BackgroundNone:
                BGrend.enabled = false;
                break;
        }
        
        switch (MainMenuEnhancedPlugin.WindowMode.Value)
        {
            case CustomSettings.WindowActive:
                BGrend.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                tint.SetActive(true);
                
                windowShine.SetActive(true);
                rightPanel.enabled = true;
                maskedScreen.enabled = true;
                
                break;
            case CustomSettings.WindowInactive:
                BGrend.maskInteraction = SpriteMaskInteraction.None;
                tint.SetActive(false);
                
                windowShine.SetActive(false);
                rightPanel.enabled = false;
                maskedScreen.enabled = false;
                
                break;
            default:
                BGrend.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                break;
        }
    }
}