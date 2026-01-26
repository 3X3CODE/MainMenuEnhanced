using Reactor.Utilities.Attributes;
using UnityEngine;

namespace MainPlugin.DragDrop;
[RegisterInIl2Cpp]
public class PropertyOverride : MonoBehaviour
{
    public PropertyOverride(System.IntPtr ptr) : base(ptr) { }
    
    void Awake()
    {
        
        //MainMenuEnhancedPlugin.LogSource.LogInfo(newSettings.gameobject);
    }
}