using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using BepInEx;
using Reactor.Utilities.Attributes;
using Object = Il2CppSystem.Object;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MainPlugin.JSONreader;

//[System.Serializable]
//[Serializable]
public class SettingsFile 
{
    //public SettingsFile(System.IntPtr ptr) : base(ptr) { }
    public string GameObject { get; set; }
    public string Component { get; set; }
    public string Property { get; set; }
    public string Value { get; set; }
}

[RegisterInIl2Cpp]
public class JsonFile : MonoBehaviour
{
    public void Start()
    {
        // this allows me to choose if the json file will be reset to default and updated with the new changes above
        // false = keep past settings, don't update
        // true = update to latest settings, this will reset to default
        bool saveState = true;
        
        var settingsNew = new SettingsFile { GameObject = "insert_gameobject_name", Component = "component_of_gameobject", Property =  "ex: SetActive, Sprite", Value = "ex: false, 5, 6.7f"};
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };
        if (!File.Exists(Path.Combine(Paths.PluginPath, "MMEconfig.json")))
        {
            string json = JsonSerializer.Serialize(settingsNew, options);
            File.WriteAllText(Path.Combine(Paths.PluginPath, "MMEconfig.json"), json);
        }
        
        else 
        {
            if (!saveState)
            {
                string newText = JsonSerializer.Serialize(settingsNew, options);
                File.WriteAllText(Path.Combine(Paths.PluginPath, "MMEconfig.json"), newText);
            }
            string json = File.ReadAllText(Path.Combine(Paths.PluginPath, "MMEconfig.json"));
            SettingsFile settings = JsonSerializer.Deserialize<SettingsFile>(json);
        }
    }
}
