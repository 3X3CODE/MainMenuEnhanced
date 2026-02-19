using MainMenuEnhanced.Assets;
using MainMenuEnhanced.InteractiveMenu;
using MainMenuEnhanced.JSONreader;
using MainMenuEnhanced.MenuBackground;
using MainPlugin.XMLreader;
using Reactor.Utilities.Attributes;
using UnityEngine;
using UnityEngine.Rendering;

namespace MainMenuEnhanced.Settings;
[RegisterInIl2Cpp]
public class ReferenceHolder : MonoBehaviour
{
    public ReferenceHolder(System.IntPtr ptr) : base(ptr) { }

    public GameObject buttonPrefab;
    public GameObject button;
    public JsonFile settings;
    
    void Start()
    {
        GameObject amb = GameObject.Find("PlayerParticles");
        amb.AddComponent<ParticleController>();
        
        buttonPrefab = AssetLoader.LoadAsset("menu","SettingsButton");
        
        button = Instantiate(buttonPrefab, transform);
        button.AddComponent<SettingsButton>();
        settings = gameObject.AddComponent<JsonFile>();
        gameObject.AddComponent<PropertyOverride>();
        gameObject.AddComponent<CustomMenu>();
        gameObject.AddComponent<Executor>();
        gameObject.AddComponent<SortingGroup>();
        GameObject particles = new GameObject("ParticleParent");
        particles.transform.SetParent(transform);
    }
}