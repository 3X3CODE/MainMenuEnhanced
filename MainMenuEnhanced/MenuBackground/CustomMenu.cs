using MainMenuEnhanced.Assets;
using MainMenuEnhanced.Settings;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace MainMenuEnhanced.MenuBackground;

[RegisterInIl2Cpp]
public class CustomMenu : MonoBehaviour
{
    private GameObject bg;
    private static SpriteRenderer BGrend;
    private GameObject manager;
    private static GameObject tint;
    private static GameObject windowShine;
    private static SpriteRenderer rightPanel;
    private static SpriteRenderer maskedScreen;
    private static Sprite bgSprite;
    private static Sprite customSprite;
    
    private void Start()
    {
        tint = GameObject.Find("/MainMenuManager/MainUI/Tint").gameObject;
        SpriteRenderer tintRend = tint.GetComponent<SpriteRenderer>();
        tintRend.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        tintRend.sortingOrder = -2;
        
        windowShine = GameObject.Find("WindowShine");
        
        rightPanel = GameObject.Find("RightPanel").GetComponent<SpriteRenderer>();
        rightPanel.sortingOrder = -2;
        maskedScreen = GameObject.Find("MaskedBlackScreen").GetComponent<SpriteRenderer>();
        maskedScreen.sortingOrder = -2;
        
        bg = GameObject.Find("/MainMenuManager/MainUI/AspectScaler/BackgroundTexture").gameObject;
        BGrend = bg.GetComponent<SpriteRenderer>();
        BGrend.sortingOrder = -3;
        bgSprite = BGrend.sprite;
        customSprite = AssetLoader.LoadExternalSprite();

        MeshRenderer stars = GameObject.Find("starfield").GetComponent<MeshRenderer>();
        stars.sortingOrder = -5;
        
        GameObject.Find("/MainMenuManager/MainUI/AspectScaler/RightPanel/MaskedBlackScreen/GameModeButtons/Divider").SetActive(false); 

        ApplyBGSettings();
    }

    private void OnDestroy()
    {
        BGrend = null;
        tint = null;
        windowShine = null;
        rightPanel = null;
        maskedScreen = null;
        bgSprite = null;
        customSprite = null;
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