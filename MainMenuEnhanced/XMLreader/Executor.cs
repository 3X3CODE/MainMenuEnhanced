using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using BepInEx.Unity.IL2CPP.Utils;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Reactor.Utilities.Attributes;
using UnityEngine;
using Object = System.Object;

namespace MainMenuEnhanced.XMLreader;


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
    public string? Component { get; set; }
    public string? Property { get; set; }
    
    public Object[] Params { get; set; } = Array.Empty<Object>();
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
    private readonly string path = CustomPaths.XmlPath;

    private DateTime lastSaved;
    private DateTime currentSave;
    private List<GameObject> cachedObjects = new();
    private Dictionary<GameObject, Vector3> cachedPositions = new();
    public void Start()
    {
        Config config = new Config();
        config.Objects.Add(new Definition
        {
            Name = "PlayButton",
            Active = true,
            Scale = 1.0f,
            Component = "",
            Property = "SetActive",
            Params = new Object[] { false },
            PositionActive = false,
            Position = new PositionData{ X=0,Y=0,Z=0 }
        });
        
        if (!OperatingSystem.IsAndroid()) this.StartCoroutine(HotReload());
        
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
            try
            {
                ExecuteModifications(Load());
            }
            catch
            {
                MainMenuEnhancedPlugin.LogSource.LogInfo("Invalid config data");
            }
        }
    }

    public Config Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Config));
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            if (!OperatingSystem.IsAndroid()) lastSaved = File.GetLastWriteTime(path);
            return (Config)serializer.Deserialize(fs);
        }
    }

    public void ExecuteModifications(Config config)
    {
        if (!config.IsActive) return;
        foreach (var entry in config.Objects)
        {
            MainMenuEnhancedPlugin.LogSource.LogInfo("[XML] Attempting to find GameObject");
            GameObject go = GameObject.Find(entry.Name);
            
            if (go == null)
            {
                foreach (GameObject obj in cachedObjects)
                {
                    if (obj.name == entry.Name) go = obj;
                } 
            }
            if (go == null) continue;
            
            Il2CppReferenceArray<Il2CppSystem.Object> NewIL2CPPArray = new(entry.Params.Length);
            
            for (int i = 0; i < entry.Params.Length; i++)
            {
                var p = entry.Params[i];

                if (p is bool vBool)
                {
                    NewIL2CPPArray[i] = vBool;
                }
                else if (p is int vInt)
                {
                    NewIL2CPPArray[i] = vInt;
                }
                else if (p is float vFloat)
                {
                    NewIL2CPPArray[i] = vFloat;
                }
                else if (p is string vString)
                {
                    NewIL2CPPArray[i] = (Il2CppSystem.String)vString;
                }
            }
            
            if (go != null)
            {
                MainMenuEnhancedPlugin.LogSource.LogInfo("[XML] GameObject found");
                
                if (!cachedObjects.Contains(go)) cachedObjects.Add(go);
                if (!cachedPositions.ContainsKey(go)) cachedPositions.Add(go, go.transform.position);
                
                if (go.TryGetComponent<AspectPosition>(out var aspect))
                {
                    aspect.enabled = false;
                }
                if (entry.Active.HasValue)
                {
                    go.SetActive(entry.Active.Value);
                }

                if (entry.Scale.HasValue)
                {
                    go.transform.localScale = Vector3.one * entry.Scale.Value;
                }

                if (!entry.Component.IsNullOrWhiteSpace())
                {
                    Component component = go.GetComponent(entry.Component);
                    if (component == null)
                    {
                        MainMenuEnhancedPlugin.LogSource.LogInfo("Component not found");
                        return;
                    }
                    else
                    {
                        if (!entry.Property.IsNullOrWhiteSpace())
                        {
                            var cppType = component.GetIl2CppType();
                            var prop = cppType.GetProperty(entry.Property);

                            if (prop == null) MainMenuEnhancedPlugin.LogSource.LogInfo("property null");
                            
                            var setter = prop.GetSetMethod();
                            setter.Invoke(component, NewIL2CPPArray);
                            if (setter != null) MainMenuEnhancedPlugin.LogSource.LogInfo("[XML] Changes applied");
                        }
                    }
                }
                else
                {
                    if (!entry.Property.IsNullOrWhiteSpace())
                    {
                        MethodInfo method = go.GetType().GetMethod(entry.Property);
                        method.Invoke(go, entry.Params);
                    }
                }
                
                if (entry.PositionActive)
                {
                    go.transform.position = new Vector3(entry.Position.X, entry.Position.Y, entry.Position.Z);
                }
                else
                {
                    if (cachedPositions.TryGetValue(go, out Vector3 pos))
                    {
                        go.transform.position = pos;
                    }
                }
            }
        }
    }
    
    private IEnumerator HotReload()
    {
        while (true && File.Exists(path))
        {
            currentSave = File.GetLastWriteTime(path);
            if (currentSave != lastSaved)
            {
                try
                {
                    ExecuteModifications(Load());
                }
                catch (Exception e)
                {
                    MainMenuEnhancedPlugin.LogSource.LogInfo($"Error while loading config or write in progress. Message: {e.Message}");
                }
            }
            
            yield return new WaitForSeconds(1f);
        }
    }
}