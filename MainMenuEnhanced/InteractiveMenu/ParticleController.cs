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
    public GameObject grabParticle;
    public bool AutoInit;
    public GameObject basePrefab;
    public bool Detach;
    private GameObject newParticle;
    private GameObject newPrefab;
    PlayerParticles mainManager;

    public void Start()
    {
        basePool = gameObject.GetComponent<ObjectPoolBehavior>();
        AutoInit = this.basePool.AutoInit;
        Detach = this.basePool.DetachOnGet;
        basePrefab = this.basePool.Prefab.gameObject;
        //newPrefab = this.basePool.Prefab.gameObject;
        //newPrefab.AddComponent<GrabbableParticle>();
        
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
        this.pool.DetachOnGet = true;
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
    }

    private void Update()
    {
        if (CustomParticles.Length == 0) return;
        foreach (PlayerParticle particle in CustomParticles)
        {
            //if (particle != null) return;
            Vector3 p = particle.gameObject.transform.position;
            float distance = Vector3.Distance(p, Vector3.zero);
            if (distance > 7f)
            {
                //this.pool.Reclaim(particle);
                particle.enabled = false;
                particle.gameObject.SetActive(false);
                particle.transform.position = new Vector2(0f, 0f);
                particle.gameObject.SetActive(true);
                particle.enabled = true;
                
                //PlayerParticle newparticle = this.pool.Get<PlayerParticle>();
                //mainManager.PlacePlayer(newparticle, false);
            }
            
        }

        /*if (this.pool.NotInUse > 0)
        {
            PlayerParticle particle = this.pool.Get<PlayerParticle>();
            mainManager.PlacePlayer(particle, false);
        }

        if (this.pool.InUse < 12)
        {
            PlayerParticle particle = this.pool.Get<PlayerParticle>();
            mainManager.PlacePlayer(particle, false);
        }*/
    }
}