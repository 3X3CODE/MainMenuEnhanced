using System;
using Reactor.Utilities.Attributes;
using UnityEngine;
using System.Reflection;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using MainPlugin;
using Object = Il2CppSystem.Object;

namespace MainMenuEnhanced.JSONreader;
[RegisterInIl2Cpp]
public class PropertyOverride : MonoBehaviour
{
    public PropertyOverride(System.IntPtr ptr) : base(ptr) { }

    public JsonFile localSettings;
    public SettingsFile settings;
    public Component component;
    public string bgOpt;
    private GameObject thing;
    
    void Start()
    {
        if (settings == null) return;
        if (!settings.EditActive) return;
        MainMenuEnhancedPlugin.LogSource.LogInfo(" Requested target object: " + settings.GameObject);

        string setObj = settings.GameObject;
        string setComp = settings.Component;
        string setProp = settings.Property;
        
        
        if (setObj != null)
        {
            if (setObj != "ignore")
            {
                thing = GameObject.Find(setObj);
                if (thing.TryGetComponent<AspectPosition>(out var aspect))
                {
                    aspect.enabled = false;
                }
            
                if (thing != null) MainMenuEnhancedPlugin.LogSource.LogInfo("Object found: " + thing.name);
                if (setComp != "ignore")
                {
                    MainMenuEnhancedPlugin.LogSource.LogInfo("Attempting to find component...");
                    component = thing.GetComponent(setComp);
                    if (component != null) MainMenuEnhancedPlugin.LogSource.LogInfo("Component found");
                }
            
                if (component != null)
                {
                    MainMenuEnhancedPlugin.LogSource.LogInfo("Attempting to find property or field...");
                    if (setProp != "ignore")
                    {
                        var cppType = component.GetIl2CppType();
                        Type propType = Type.GetType(cppType.AssemblyQualifiedName);
                        var prop = cppType.GetProperty(setProp);

                        #region getField

                        var cppType2 = component.GetIl2CppType();
                        Type fieldType = Type.GetType(cppType2.AssemblyQualifiedName);
                        PropertyInfo field = null;
                        while (fieldType != null)
                        {
                            field = fieldType.GetProperty(setProp,
                                BindingFlags.Instance | BindingFlags.Public);

                            if (field != null) break;
                            fieldType = fieldType.BaseType;
                        }

                        #endregion

                        if (prop == null) MainMenuEnhancedPlugin.LogSource.LogInfo("property null");
                        if (field == null) MainMenuEnhancedPlugin.LogSource.LogInfo("field null");
                        var val = new Il2CppReferenceArray<Object>(new Object[1]);
                        val[0] = settings.boolValue;
                        Object[] parameters = new Object[] { false };
                        var setter = prop.GetSetMethod();
                        setter.Invoke(component, val);
                        if (setter != null) MainMenuEnhancedPlugin.LogSource.LogInfo("Property found and value set");
                    }
                }
            }

            if (setComp == "ignore")
            {
                if (setProp != "ignore")
                {
                    Type objectType = typeof(GameObject);
                    MethodInfo method = objectType.GetMethod(setProp);
                    object[] param = new object[1];
                    param[0] = settings.boolValue;
                    method.Invoke(thing, param);
                }
            }
        }
    }

    private void Update()
    {
        if (thing != null)
        {
            if (settings.SetPosition)
            {
                thing.transform.position = toVector3(settings.x, settings.y, settings.z);
            }
        }
    }

    private Vector3 toVector3(float x, float y, float z)
    {
        Vector3 temp = new Vector3(x, y, z);
        return temp;
    }

    private Vector2 toVector2(float x, float y)
    {
        Vector2 temp = new Vector2(x, y);
        return temp;
    }
}