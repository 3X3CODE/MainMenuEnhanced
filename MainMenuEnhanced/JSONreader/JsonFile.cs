using System;
using System.IO;
using UnityEngine;
using System.Text.Json;
using BepInEx;
using Reactor.Utilities.Attributes;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MainMenuEnhanced.JSONreader;


public class SettingsFile 
{
    public bool EditActive { get; set; }
    public string GameObject { get; set; }
    public string Component { get; set; }
    public string Property { get; set; }
    public bool boolValue { get; set; }
    public bool Save { get; set; }
    public bool SetPosition { get; set; }

    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
        
}

[RegisterInIl2Cpp]
public class JsonFile : MonoBehaviour
{
    public SettingsFile settings;

    public string path = Path.Combine(Paths.PluginPath, "MainMenuEnhanced", "MMEconfig.json");

    private SettingsFile settingsNew;
    private JsonSerializerOptions options;
    private DateTime lastSaveTime;
    private DateTime currentSaveTime;
    
    public void Start()
    {
        // this allows me to choose if the json file will be reset to default and updated with the new changes above
        // true = keep past settings, don't update
        // false = update to latest settings, this will reset to default
        bool saveState = false;
        
        settingsNew = new SettingsFile { GameObject = "insert_gameobject_name", Property =  "ex: SetActive, Sprite", Component = "component", Save = true};
        options = new JsonSerializerOptions
        {
            WriteIndented = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };
        if (!File.Exists(path))
        {
            string json = JsonSerializer.Serialize(settingsNew, options);
            File.WriteAllText(Path.Combine(path), json);
            return;
        }
        
        else 
        {
            LoadSettings();
        }

        gameObject.GetComponent<PropertyOverride>().settings = settings;
    }

    private void Update()
    {
        if (File.Exists(path))
        {
            currentSaveTime = File.GetLastWriteTime(path);
            if (currentSaveTime != lastSaveTime)
            {
                LoadSettings();
            }
        }
    }

    void LoadSettings()
    {
        string json = File.ReadAllText(path);
        settings = JsonSerializer.Deserialize<SettingsFile>(json);
        lastSaveTime = File.GetLastWriteTime(path);
        if (!settings.Save)
        {
            //settingsNew.Save = true;
            string newText = JsonSerializer.Serialize(settingsNew, options);
            File.WriteAllText(path, newText);
            string stringjson = File.ReadAllText(path);
            settings = JsonSerializer.Deserialize<SettingsFile>(stringjson);
        }
    }
}
