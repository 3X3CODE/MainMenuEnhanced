using System;
using UnityEngine;
using Reactor.Utilities.Attributes;
using TMPro;

namespace MainMenuEnhanced.Settings;

[RegisterInIl2Cpp]
public class SettingsButton : MonoBehaviour
{
    public GameObject buttonHighlight;
    public GameObject buttonNormal;
    public GameObject ModText;
    public GameObject customMenu;
    
    void Start()
    {
        // gameobject initialization
        
        gameObject.transform.position = new Vector3(-2f, 1f, 1f);
        buttonHighlight = transform.Find("Highlight").gameObject;
        buttonNormal = transform.Find("Normal").gameObject;
        buttonHighlight.SetActive(false);
        
        // button text initialization

        ModText = transform.Find("Text_TMP").gameObject;
        ModText.SetActive(false);
        
        customMenu = Instantiate(CustomAssets.SettingsMenu);
        customMenu.AddComponent<CustomSettingsBehaviour>();
        customMenu.transform.position = new Vector3(0f, 0f, -10f);
        customMenu.SetActive(false);
    }
    
    void OnMouseEnter()
    {
        if (OperatingSystem.IsAndroid()) return;
        
        buttonHighlight.SetActive(true);
        buttonNormal.SetActive(false);
        transform.position = new Vector2(-1f, 1f);
        ModText.SetActive(true);
    }
    
    void OnMouseExit()
    {
        if (OperatingSystem.IsAndroid()) return;
        
        buttonHighlight.SetActive(false); 
        buttonNormal.SetActive(true);
        transform.position = new Vector2(-2f, 1f);
        ModText.SetActive(false);
    }

    void OnMouseDown()
    {
        customMenu.SetActive(true);
    }
}
