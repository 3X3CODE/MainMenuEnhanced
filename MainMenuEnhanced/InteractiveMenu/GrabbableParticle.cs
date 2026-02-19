using System;
using MainMenuEnhanced.Settings;
using MainPlugin;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace MainMenuEnhanced.InteractiveMenu;
[RegisterInIl2Cpp]
public class GrabbableParticle : MonoBehaviour
{
    public GrabbableParticle(System.IntPtr ptr) : base(ptr) { }

    PlayerParticle myParticle;
    bool isGrabbed;
    private float Distance;
    private Vector2 mousePos;
    private Vector2 offset;

    private static SpriteRenderer[] allRends;

    void Start()
    {
        myParticle = gameObject.GetComponent<PlayerParticle>();
        myParticle.enabled = false;
        myParticle.enabled = true;
        SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
        gameObject.transform.SetParent(GameObject.Find("ParticleParent").transform);
        rend.sortingOrder = -5;

        allRends = transform.parent.gameObject.GetComponentsInChildren<SpriteRenderer>();
    }
    void Update()
    {
        if (OperatingSystem.IsAndroid())
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                    Distance = Vector3.Distance(mousePos, gameObject.transform.position);
                    
                    if (Distance < 0.7f)
                    {
                        offset = (Vector2)transform.position - mousePos;
                        
                        if (touch.phase == TouchPhase.Began) myParticle.enabled = false;
                        if (touch.phase == TouchPhase.Moved)
                        {
                            transform.Rotate(0f, 0f, Time.deltaTime * myParticle.angularVelocity);
                            transform.position = mousePos + offset;
                        }
                    }

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        myParticle.enabled = true;
                    }
                }
            }
        }

        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        
            if (Input.GetMouseButtonDown(0))
            {
                Distance = Vector3.Distance(mousePos, gameObject.transform.position);
                if (Distance < 0.7f)
                {
                    isGrabbed = true;
                    offset = (Vector2)transform.position - mousePos;
                }
            }
            if (isGrabbed && Input.GetMouseButton(0))
            {
                if (Distance < 0.7f)
                {
                    myParticle.enabled = false;
                    transform.Rotate(0f,0f,Time.deltaTime * myParticle.angularVelocity);
                    transform.position = mousePos + offset;
                }
            }
        
            if (Input.GetMouseButtonUp(0))
            {
                isGrabbed = false;
                myParticle.enabled = true;
            }
        }
        
        
        Vector3 position = transform.position;
        float distance = position.sqrMagnitude;
        if (distance > 6f * 6f)
        {
            gameObject.SetActive(false);
            transform.position = new Vector2(0f, 0f);
            gameObject.SetActive(true);
        }
    }
    
    public static void changeMask()
    {
        switch (MainMenuEnhancedPlugin.WindowMode.Value)
        {
            case CustomSettings.WindowActive:
                foreach (SpriteRenderer rend in allRends)
                {
                    rend.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                }

                break;
            case CustomSettings.WindowInactive:
                foreach (SpriteRenderer rend in allRends)
                {
                    rend.maskInteraction = SpriteMaskInteraction.None;
                }

                break;
        }
    }
    
}