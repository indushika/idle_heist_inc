using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListner : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent response;

    public void OnEventRaised()
    {
        response.Invoke();
    }
    private void OnEnable()
    {
        Event.RegisterListner(this);
    }
    private void OnDisable()
    {
        Event.RemoveListner(this);
    }
    
}
