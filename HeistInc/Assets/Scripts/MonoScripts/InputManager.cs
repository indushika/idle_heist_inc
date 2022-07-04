using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 fp;   
    private Vector3 lp;   
    private float dragDistance; 
    public float tapTimeout;
    private float NewTime; 

    public GameEvent TapHoldEvent; 
    public GameEvent TapReleaseEvent;  
    public GameEvent BoostsVanTappedEvent;  

    void Start()
    {
        dragDistance = Screen.height * 15 / 100; 
    }

    void Update()
    {
#if UNITY_EDITOR
        InputSystemEditor(); 
#elif UNITY_ANDROID 
        InputSystemMobile(); 
#elif UNITY_IOS 
        InputSystemMobile(); 
#endif

    }

    public void InputSystemMobile()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {

                    if (raycastHit.collider.CompareTag("BoostsVan"))
                    {
                        Debug.Log("BoostsVan clicked");
                        BoostsVanTappedEvent.Raise(); 
                    }

                }
                TapHoldEvent.Raise();
                NewTime = Time.time + tapTimeout; 
            } 
            else if (touch.phase == TouchPhase.Stationary)
            {
                if(Time.time > NewTime)
                {
                    TapReleaseEvent.Raise();

                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (Time.time > NewTime)
                {
                    TapReleaseEvent.Raise();

                }
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                TapReleaseEvent.Raise();
                lp = touch.position;

                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {

                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {
                        if ((lp.x > fp.x))
                        {
                            Debug.Log("Right Swipe");
                        }
                        else
                        {
                            Debug.Log("Left Swipe");
                        }
                    }
                    else
                    {
                        if (lp.y > fp.y)
                        {
                            Debug.Log("Up Swipe");
                        }
                        else
                        {
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {
                    Debug.Log("Tap");
                }
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                Debug.Log("tap and hold");
            }
        }
    } 

    public void InputSystemEditor()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("tap hold");
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {

                if (raycastHit.collider.CompareTag("BoostsVan"))
                {
                    Debug.Log("BoostsVan clicked");
                    BoostsVanTappedEvent.Raise();
                }

            }
            TapHoldEvent.Raise();  
        }

        else if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("tap release");
            TapReleaseEvent.Raise(); 
        }
    }
}


    