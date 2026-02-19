using System.Collections;

using Il2CppInterop.Runtime.Attributes;
using Il2CppSystem.Collections.Generic;
using MainMenuEnhanced.Helpers;
using MainMenuEnhanced.Settings;
using MainPlugin;
using Reactor.Utilities.Attributes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainMenuEnhanced.InteractiveMenu;
[RegisterInIl2Cpp]
public class ParticleController  : MonoBehaviour
{
    public ParticleController(System.IntPtr ptr) : base(ptr) { }
    PlayerParticle[] baseParticles;
    PlayerParticle[] CustomParticles;
    public ObjectPoolBehavior basePool;
    public ObjectPoolBehavior pool;
    public bool AutoInit;
    public GameObject basePrefab;
    public int index;
    private GameObject newParticle;
    private GameObject newPrefab;
    PlayerParticles mainManager;

    private Coroutine _claimRoutine;

    // TODO: Keep what we actually need and remove the rest
    
    public void Start()
    {
        transform.SetParent(GameObject.Find("ReferenceHolder").transform);
        
        GameObject particles = new GameObject("Particles");
        basePool = gameObject.GetComponent<ObjectPoolBehavior>();
        AutoInit = this.basePool.AutoInit;
        index = this.basePool.childIndex;
        basePrefab = this.basePool.Prefab.gameObject;
   
        Object.DestroyImmediate(basePool);
        
        newParticle = Instantiate(basePrefab);
        newParticle.name = "CustomParticle";
        newParticle.AddComponent<GrabbableParticle>();
        newParticle.GetComponent<PlayerParticle>().OwnerPool = pool;
        newParticle.SetActive(false);

        this.pool = gameObject.AddComponent<ObjectPoolBehavior>();
        foreach (var child in pool.activeChildren)
        {
            Object.Destroy(child.gameObject);
        }
        foreach (var child in pool.inactiveChildren)
        {
            Object.Destroy(child.gameObject);
        }
        pool.activeChildren.Clear();
        pool.inactiveChildren.Clear();
        
        this.pool.AutoInit = AutoInit;
        this.pool.DetachOnGet = false;
        this.pool.Prefab = newParticle.GetComponent<PlayerParticle>();
        //this.pool.Prefab = newPrefab.GetComponent<PlayerParticle>();
        this.pool.poolSize = 12;
        this.pool.InitPool(this.pool.Prefab);

        mainManager = GetComponent<PlayerParticles>();
        mainManager.pool = this.pool;
        this.pool.ReclaimAll();
        mainManager.Start();
        mainManager.Update();
  
        baseParticles = UnityEngine.Object.FindObjectsOfType<PlayerParticle>(true);
        foreach (PlayerParticle particle in baseParticles)
        {
            if (particle.gameObject.name != "PlayerParticle(Clone)") continue;
            Object.Destroy(particle.gameObject);
        }
        
        CustomParticles = UnityEngine.Object.FindObjectsOfType<PlayerParticle>(false);
        if (CustomParticles.Length == 0) return;
        
    }
    
    [HideFromIl2Cpp]
    public IEnumerator Claim()
    {
            foreach (PlayerParticle particleObject in CustomParticles)
            {
                //if (particleObject == null) break; 
                if (!particleObject.enabled) break;
                GameObject particle = particleObject.gameObject;
                Vector3 pos = particle.transform.position;
                float distance = Vector3.Distance(pos, Vector3.zero);
                if (distance > 7f)
                {
                    particleObject.enabled = false;
                    yield return null;
                    particle.SetActive(false);
                    yield return null;
                    particle.transform.position = new Vector2(0f, 0f);
                    yield return null;
                    particle.SetActive(true);
                    yield return null;
                    particleObject.enabled = true;
                    yield return new WaitForSeconds(2f);
                }

                MainMenuEnhancedPlugin.LogSource.LogInfo("tracking particle");
            }
    }
}