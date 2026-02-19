using System.Collections.Generic;
using MainMenuEnhanced.InteractiveMenu;
using MainMenuEnhanced.MenuBackground;
using MainPlugin;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace MainMenuEnhanced.Settings;

[RegisterInIl2Cpp]
public class CustomSettingsBehaviour : MonoBehaviour
{
    public CustomSettingsBehaviour(System.IntPtr ptr) : base(ptr) { }
    
    private Transform buttonParentTransform;
    private static CustomOptionsButton currentBackgroundButton;
    private static CustomOptionsButton currentWindowButton;
    private Collider2D closeCollider;
    
    private Dictionary<string, CustomSettings> settingsDefinition;
    private List<Transform> buttons;
    
    public void Start()
    {
        settingsDefinition = new Dictionary<string, CustomSettings>();
        settingsDefinition.Add("Default", CustomSettings.BackgroundDefault);
        settingsDefinition.Add("Custom", CustomSettings.BackgroundCustom);
        settingsDefinition.Add("None", CustomSettings.BackgroundNone);
        settingsDefinition.Add("Active", CustomSettings.WindowActive);
        settingsDefinition.Add("Inactive", CustomSettings.WindowInactive);

        buttons = new List<Transform>();
        buttons.Add(transform.Find("Buttons/BackgroundOption/Default"));
        buttons.Add(transform.Find("Buttons/BackgroundOption/Custom"));
        buttons.Add(transform.Find("Buttons/BackgroundOption/None"));
        buttons.Add(transform.Find("Buttons/WindowOption/Active"));
        buttons.Add(transform.Find("Buttons/WindowOption/Inactive"));
        
        foreach (Transform child in buttons)
        {
            CustomOptionsButton button = child.gameObject.AddComponent<CustomOptionsButton>();
               
            if (settingsDefinition.TryGetValue(child.name, out CustomSettings value))
            {
                button.representType = value;
                if (child.parent.name == "BackgroundOption") button.settingsType = CustomSettings.BackgroundOption;
                else if (child.parent.name == "WindowOption") button.settingsType = CustomSettings.WindowOption;
            }
        }

        closeCollider = transform.Find("Background").GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!closeCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                gameObject.SetActive(false);
            }
        }
    }

    public static void AssignSetting(CustomSettings settingsType, CustomSettings settingsValue, CustomOptionsButton button)
    {
        if (settingsType == CustomSettings.BackgroundOption)
        {
            MainMenuEnhancedPlugin.BackgroundMode.Value = settingsValue;
            if (currentBackgroundButton != null) currentBackgroundButton.Deactivate();
            button.Activate();
            currentBackgroundButton = button;
        }

        if (settingsType == CustomSettings.WindowOption)
        {
            MainMenuEnhancedPlugin.WindowMode.Value = settingsValue;
            if (currentWindowButton != null) currentWindowButton.Deactivate();
            button.Activate();
            currentWindowButton = button;
        }

        MainMenuEnhancedPlugin.config.Save();
        CustomMenu.ApplyBGSettings();
        GrabbableParticle.changeMask();
    }
}