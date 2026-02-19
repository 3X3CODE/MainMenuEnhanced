using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using BepInEx;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace MainPlugin.XMLreader;


[XmlRoot("Config")]
public class Config
{ 
    [XmlElement("EditActive")] public bool IsActive { get; set; }
    [XmlElement("GameObject")] public List<Definition> Objects { get; set; } = new List<Definition>();
}

public class Definition
{
    [XmlAttribute("name")] 
    public string Name { get; set; }
    
    public bool? Active { get; set; }
    public float? Scale { get; set; }
    public bool PositionActive { get; set; }
    public PositionData Position { get; set; }
}

public class PositionData
{
    [XmlAttribute("x")] public float X { get; set; }
    [XmlAttribute("y")] public float Y { get; set; }
    [XmlAttribute("z")] public float Z { get; set; }
}

[RegisterInIl2Cpp]
public class Executor : MonoBehaviour
{
    private readonly string path = OperatingSystem.IsAndroid() ? CustomPaths.androidXmlPath : CustomPaths.winXmlPath;

    private DateTime lastSaved;
    private DateTime currentSave;
    public void Start()
    {
        Config config = new Config();
        config.Objects.Add(new Definition
        {
            Name = "ExampleObject",
            Active = true,
            Scale = 1.0f,
            Position = new PositionData{ X=0,Y=0,Z=0 }
        });
        XmlSerializer serializer = new XmlSerializer(typeof(Config));
        if (!File.Exists(path))
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, config);
            }
        }
        else
        {
            if (!config.IsActive) return;
            ExecuteModifications(Load());
        }
    }

    private void Update()
    {
        if (File.Exists(path))
        {
            currentSave = File.GetLastWriteTime(path);
            if (currentSave != lastSaved)
            {
                ExecuteModifications(Load());
            }
        }
    }

    public Config Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Config));
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            lastSaved = File.GetLastWriteTime(path);
            return (Config)serializer.Deserialize(fs);
        }
    }

    public void ExecuteModifications(Config config)
    {
        if (!config.IsActive) return;
        foreach (var entry in config.Objects)
        {
            GameObject go = GameObject.Find(entry.Name);
            if (go != null)
            {
                if (go.TryGetComponent<AspectPosition>(out var aspect))
                {
                    aspect.enabled = false;
                }
                MainMenuEnhancedPlugin.LogSource.LogInfo(go.name);
                if (entry.Active.HasValue)
                {
                    go.SetActive(entry.Active.Value);
                    MainMenuEnhancedPlugin.LogSource.LogInfo(entry.Active.Value);
                }

                if (entry.Scale.HasValue)
                {
                    go.transform.localScale = Vector3.one * entry.Scale.Value;
                    MainMenuEnhancedPlugin.LogSource.LogInfo(entry.Scale.Value);
                }

                if (entry.Position != null)
                {
                    if (entry.PositionActive)
                    {
                        go.transform.position = new Vector3(entry.Position.X, entry.Position.Y, entry.Position.Z);
                        MainMenuEnhancedPlugin.LogSource.LogInfo(entry.Position.X + entry.Position.Y + entry.Position.Z);
                    }
                }
            }
        }
    }
}