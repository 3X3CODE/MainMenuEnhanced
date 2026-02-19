using System.Collections.Generic;
using MainPlugin;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace MainMenuEnhanced.Helpers;

[RegisterInIl2Cpp]
public class ObjectPooler : MonoBehaviour
{
    public GameObject prefab;
    public int childCount;
    public List<GameObject> activeChildren = new List<GameObject>();
    public List<GameObject> inactiveChildren = new List<GameObject>();
    public int InUse
    {
        get
        {
            return this.activeChildren.Count;
        }
    }
    public int NotInUse
    {
        get
        {
            return this.inactiveChildren.Count;
        }
    }
    void Start()
    {
        for (int i = 0; i < this.childCount; i++)
        {
            CreateInactive();
        }
    }

    public GameObject Get()
    {
        if (this.inactiveChildren.Count == 0)
        {
            CreateInactive();
        }

        GameObject obj = this.inactiveChildren[this.inactiveChildren.Count - 1];
        obj.SetActive(true);
        this.inactiveChildren.RemoveAt(this.inactiveChildren.Count - 1);
        this.activeChildren.Add(obj);
        return obj;
    }

    public void Reclaim(GameObject obj)
    {
        //if (obj == null) return;
        obj.SetActive(false);
        this.activeChildren.RemoveAt(this.activeChildren.Count - 1);
        this.inactiveChildren.Add(obj);
    }

    public void CreateInactive()
    {
        //if (prefab == null) return;
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        this.inactiveChildren.Add(obj);
        MainMenuEnhancedPlugin.LogSource.LogInfo("Inactive created");
    }
}