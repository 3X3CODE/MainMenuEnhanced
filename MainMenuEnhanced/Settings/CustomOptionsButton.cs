using System;
using Reactor.Utilities.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace MainMenuEnhanced.Settings;

[RegisterInIl2Cpp]
public class CustomOptionsButton : MonoBehaviour
{
    public CustomSettings settingsType;
    public CustomSettings representType;
    public SpriteRenderer background;
    public SpriteRenderer highlight;
    
    public void Start()
    {
        background = transform.Find("Background").GetComponent<SpriteRenderer>();
        highlight = transform.Find("Highlight").GetComponent<SpriteRenderer>();
        highlight.gameObject.SetActive(false);
        gameObject.transform.localPosition += new Vector3(0f, 0f, -5f);
    }

    private void OnMouseEnter()
    {
        if (OperatingSystem.IsAndroid()) return;
        
        highlight.gameObject.SetActive(true);
    }
    
    private void OnMouseExit()
    {
        if (OperatingSystem.IsAndroid()) return;
        
        highlight.gameObject.SetActive(false);
    }
    
    private void OnMouseDown()
    {
        CustomSettingsBehaviour.AssignSetting(settingsType, representType, this);
    }
    
    public void Activate()
    {
        background.color = Color.green;
    }
    
    public void Deactivate()
    {
        background.color = Color.white;
    }
}