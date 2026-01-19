using Reactor.Utilities.Attributes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainMenuEnhanced.InteractiveMenu;
[RegisterInIl2Cpp]
public class ParticleController  : MonoBehaviour
{
    public ParticleController(System.IntPtr ptr) : base(ptr) { }
    PlayerParticle[] baseParticles;
    public ObjectPoolBehavior basePool;
    public ObjectPoolBehavior pool;
    public GameObject grabParticle;
    public bool AutoInit;
    public GameObject basePrefab;
    public bool Detach;
    private GameObject newParticle;

    public void Start()
    {
        basePool = gameObject.GetComponent<ObjectPoolBehavior>();
        AutoInit = this.basePool.AutoInit;
        Detach = this.basePool.DetachOnGet;
        basePrefab = this.basePool.Prefab.gameObject;
        
        Object.DestroyImmediate(basePool);
        
        newParticle = Instantiate(basePrefab);
        newParticle.name = "CustomParticle";
        newParticle.AddComponent<GrabbableParticle>();
        newParticle.GetComponent<PlayerParticle>().OwnerPool = pool;
        newParticle.SetActive(false);

        pool = gameObject.AddComponent<ObjectPoolBehavior>();
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
        this.pool.DetachOnGet = true;
        this.pool.Prefab = newParticle.GetComponent<PlayerParticle>();
        this.pool.poolSize = 12;
        this.pool.InitPool(this.pool.Prefab);

        PlayerParticles mainManager = GetComponent<PlayerParticles>();
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
    }
    
}