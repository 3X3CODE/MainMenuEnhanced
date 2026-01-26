using MainPlugin.Assets;
using MainPlugin.DragDrop;
using MainPlugin.JSONreader;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace MainPlugin.Settings;
[RegisterInIl2Cpp]
public class ReferenceHolder : MonoBehaviour
{
    public ReferenceHolder(System.IntPtr ptr) : base(ptr) { }

    public GameObject buttonPrefab;
    public GameObject button;
    public JsonFile settings;
    
    void Start()
    {
        buttonPrefab = AssetLoader.LoadAsset("menu", "SettingsButton");
        button = Instantiate(buttonPrefab);
        button.AddComponent<SettingsButton>();
        settings = gameObject.AddComponent<JsonFile>();
    }
}