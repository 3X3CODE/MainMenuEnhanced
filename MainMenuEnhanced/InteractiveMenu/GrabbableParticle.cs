using Reactor.Utilities.Attributes;
using UnityEngine;

namespace MainPlugin.InteractiveMenu;
[RegisterInIl2Cpp]
public class GrabbableParticle : MonoBehaviour
{
    public GrabbableParticle(System.IntPtr ptr) : base(ptr) { }

    PlayerParticle myParticle;
    bool isGrabbed;
    private float Distance;
    public static GameObject newParticle;

    void Start()
    {
        
        myParticle = gameObject.GetComponent<PlayerParticle>();
        myParticle.enabled = false;
        myParticle.enabled = true;
    }
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            isGrabbed = true;
        }
        if (isGrabbed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Distance = Vector3.Distance(mousePos, gameObject.transform.position);
            if (Distance < 0.7f)
            {
                myParticle.enabled = false;
                gameObject.transform.position = mousePos;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isGrabbed = false;
                myParticle.enabled = true;
                gameObject.transform.position = transform.position;
            }
        }
        
    }

    public static GameObject getParticle()
    {
        if (newParticle != null)
        {
            return newParticle;
        }
        else
        {
            MainMenuEnhancedPlugin.LogSource.LogInfo("newParticle is null");
            return null;
        }
    }
}