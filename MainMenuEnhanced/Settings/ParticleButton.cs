using UnityEngine;
using MainPlugin.Assets;
using PowerTools;
using Reactor.Utilities.Attributes;

namespace MainPlugin.Settings;

[RegisterInIl2Cpp]
public class ParticleButton : MonoBehaviour
{
    public ParticleButton(System.IntPtr ptr) : base(ptr) { }
    
    public GameObject buttonPrefab;
    public GameObject buttonBase;
    public GameObject button;
    public GameObject buttonPress;
    public SpriteAnim anim;

    void Start()
    {
        buttonPrefab = AssetLoader.LoadAsset("mainmenuenhanced", "ParticleButton");
        button = Instantiate(buttonPrefab);
        //button.transform.localScale = new Vector3(0.07f, 0.07f, 1f);
        buttonPress = GameObject.Find("ParticleButton(Clone)/Pressed");
        anim = button.GetComponentInChildren<SpriteAnim>();
        anim.Play();
        
    }

    void Update()
    {
        
    }
}