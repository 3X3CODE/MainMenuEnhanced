using MainMenuEnhanced.Assets;
using UnityEngine;
using Reactor.Utilities.Attributes;
using TMPro;

namespace MainMenuEnhanced.Settings;

[RegisterInIl2Cpp]
public class SettingsButton : MonoBehaviour
{
    public SettingsButton(System.IntPtr ptr) : base(ptr) { }
    
    public GameObject buttonHighlight;
    public GameObject buttonNormal;
    public TextMeshPro startText;
    public GameObject ModText;
    public GameObject customMenu;
    
    void Start()
    {
        // gameobject initialization
        
        gameObject.transform.localScale = new Vector3(0.07f, 0.07f, 1f);
        gameObject.transform.position = new Vector3(-2f, 1f, 1f);
        buttonHighlight = GameObject.Find("SettingsButton(Clone)/Highlight");
        buttonNormal = GameObject.Find("SettingsButton(Clone)/Normal");
        buttonHighlight.SetActive(false);
        
        // button text initialization
        
        startText = GameObject.Find("PlayButton/FontPlacer/Text_TMP").GetComponent<TextMeshPro>();
        ModText = GameObject.Instantiate(GameObject.Find("ReactorVersion"));
        ModText.SetActive(false);
        ModText.name = "Text_TMP";
        ModText.transform.SetParent(transform);
        var Text_TMP = ModText.GetComponent<TextMeshPro>();
        
        // text settings
        
        Text_TMP.text = "SETTINGS";
        Text_TMP.font = startText.font;
        Text_TMP.fontSize = 4;
        Text_TMP.color = startText.color;
        ModText.transform.localPosition = new Vector3(134f, -34.3f, 0f);
        
        customMenu = Instantiate(AssetLoader.LoadAsset("menu", "SettingsMenu"));
        customMenu.AddComponent<CustomSettingsBehaviour>();
        customMenu.transform.position = new Vector3(0f, 0f, -10f);
        customMenu.SetActive(false);
    }
    
    void OnMouseEnter()
    {
        buttonHighlight.SetActive(true);
        buttonNormal.SetActive(false);
        transform.position = new Vector2(-1f, 1f);
        ModText.SetActive(true);
    }
    
    void OnMouseExit()
    {
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
