using UnityEngine;
using Reactor.Utilities.Attributes;
using TMPro;

namespace MainPlugin.Settings;

[RegisterInIl2Cpp]
public class SettingsButton : MonoBehaviour
{
    public SettingsButton(System.IntPtr ptr) : base(ptr) { }
    
    public GameObject buttonHighlight;
    public GameObject buttonNormal;
    public GameObject baseText;
    public TextMeshPro startText;
    public GameObject menu;
    public GameObject newmenu;
    public GameObject menutab;
    public GameObject menugeneral;
    public GameObject menugraphics;

    void Start()
    {
        // gameobject initialization
        gameObject.transform.localScale = new Vector3(0.07f, 0.07f, 1f);
        gameObject.transform.position = new Vector3(-2f, 1f, 1f);
        buttonHighlight = GameObject.Find("SettingsButton(Clone)/Highlight");
        buttonNormal = GameObject.Find("SettingsButton(Clone)/Normal");
        buttonHighlight.SetActive(false);
        // button text initialization
        baseText = GameObject.Find("PlayButton/FontPlacer/Text_TMP");
        startText = baseText.GetComponent<TextMeshPro>();
        GameObject Text = GameObject.Find("ReactorVersion");
        GameObject ModText = GameObject.Instantiate(Text);
        ModText.name = "Text_TMP";
        ModText.transform.SetParent(transform);
        var Text_TMP = ModText.GetComponent<TextMeshPro>();
        // text settings
        Text_TMP.text = "SETTINGS";
        Text_TMP.font = startText.font;
        Text_TMP.fontSize = 4;
        Text_TMP.color = startText.color;
        ModText.transform.position = gameObject.transform.position;
        ModText.transform.localPosition = new Vector3(134f, -34.3f, 0f);

        // options menu initialization
        menu = GameObject.FindObjectOfType<OptionsMenuBehaviour>(true).gameObject;
        newmenu = Instantiate(menu);
        newmenu.GetComponent<OptionsMenuBehaviour>().enabled = false;
        menutab = newmenu.transform.Find("TabButtons").gameObject;
        menugeneral = newmenu.transform.Find("GeneralTab").gameObject;
        menugraphics = newmenu.transform.Find("GraphicsTab").gameObject;
    }
    
    void OnMouseEnter()
    {
        buttonHighlight.SetActive(true);
        buttonNormal.SetActive(false);
        transform.position = new Vector2(-1f, 1f);
    }
    
    void OnMouseExit()
    {
        buttonHighlight.SetActive(false); 
        buttonNormal.SetActive(true);
        transform.position = new Vector2(-2f, 1f);
    }

    void OnMouseDown()
    {
        newmenu.SetActive(true);
        menutab.SetActive(false);
        menugeneral.SetActive(false);
        menugraphics.SetActive(false);
    }
    
    
}