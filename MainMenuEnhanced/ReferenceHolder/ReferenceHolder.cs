using MainMenuEnhanced.MenuBackground;
using MainMenuEnhanced.Settings;
using MainMenuEnhanced.XMLreader;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace MainMenuEnhanced.ReferenceHolder;

[RegisterInIl2Cpp]
public class ReferenceHolder : MonoBehaviour
{
    private GameObject button;
    
    void Start()
    {
        button = Instantiate(CustomAssets.SettingsButton);
        button.AddComponent<SettingsButton>();
        gameObject.AddComponent<CustomMenu>();
        gameObject.AddComponent<Executor>();
    }
}