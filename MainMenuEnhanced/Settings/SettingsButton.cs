using UnityEngine;
using MainPlugin.Assets;
using Reactor.Utilities.Attributes;

namespace MainPlugin.Settings;

[RegisterInIl2Cpp]
public class SettingsButton : MonoBehaviour
{
    public SettingsButton(System.IntPtr ptr) : base(ptr) { }
    
    public GameObject buttonPrefab;
    public GameObject button;
    public GameObject buttonInactive;
    public GameObject buttonActive;
    public GameObject buttonSprite;
    public GameObject baseText;
    public GameObject buttonText;

    void Start()
    {
        buttonPrefab = AssetLoader.LoadAsset("mainmenuenhanced", "SettingsButton");
        button = Instantiate(buttonPrefab);
        button.transform.localScale = new Vector3(0.07f, 0.07f, 1f);
        buttonSprite = GameObject.Find("SettingsButton(Clone)/Sprite");
        buttonInactive = GameObject.Find("SettingsButton(Clone)/Unpressed");
        buttonActive = GameObject.Find("SettingsButton(Clone)/Pressed");
        buttonActive.SetActive(false);
        baseText = GameObject.Find("PlayButton/FontPlacer/Text_TMP");
        buttonText = Instantiate(baseText);
        buttonText.transform.SetParent(button.transform);
        button.transform.position = new Vector3(-2f, 1f, 0f);
    }

    void Update()
    {
        
    }
}