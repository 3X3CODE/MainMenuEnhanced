using System;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace MainMenuEnhanced.InteractiveMenu;

[RegisterInIl2Cpp]
public class GrabbableParticle : MonoBehaviour
{
    PlayerParticle myParticle;
    bool isGrabbed;
    private float Distance;
    private Vector2 mousePos;
    private Vector2 offset;
    private int? activeFingerId = null;
    Touch? activeTouch = null;

    void Start()
    {
        myParticle = gameObject.GetComponent<PlayerParticle>();
        SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
        rend.sortingOrder = -4;
    }
    void Update()
    {
        if (OperatingSystem.IsAndroid())
        {
            #region AndroidDrag
            
            if (Input.touchCount > 0)
            {
                if (activeTouch == null)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                            if (Vector3.Distance(touchPos, transform.position) < 0.7f)
                            {
                                activeTouch = touch;
                                offset = (Vector2)transform.position - touchPos;
                                myParticle.enabled = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (activeTouch.HasValue)
                    {
                        Touch touch = activeTouch.Value;
                        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touch.position);

                        if (touch.phase == TouchPhase.Moved)
                        {
                            transform.Rotate(0f, 0f, Time.deltaTime * myParticle.angularVelocity);
                            transform.position = worldPos + offset;
                        }
                        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                        {
                            myParticle.enabled = true;
                            activeFingerId = null;
                            activeTouch = null;
                        }
                    }
                    else
                    {
                        myParticle.enabled = true;
                        activeFingerId = null;
                        activeTouch = null;
                    }
                }
            }
            
            #endregion
        }

        else
        {
            #region PCDrag
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (Vector3.Distance(mousePos, transform.position) < 0.7f)
                {
                    isGrabbed = true;
                    offset = (Vector2)transform.position - mousePos;
                }
            }

            if (isGrabbed)
            {
                if (Input.GetMouseButton(0))
                {
                    myParticle.enabled = false;
                    transform.Rotate(0, 0, Time.deltaTime * myParticle.angularVelocity);
                    transform.position = mousePos + offset;
                }
                else
                {
                    isGrabbed = false;
                    myParticle.enabled = true;
                }
            }
            
            #endregion
        }
    }
}