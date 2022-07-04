using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListner> listners = new List<GameEventListner>();

    public void Raise()
    {
        for (int i = 0; i < listners.Count; i++)
        {
            if (listners[i] != null)
            {
                listners[i].OnEventRaised();
            }
        }
    }

    public void RegisterListner(GameEventListner listner)
    {
        listners.Add(listner);
    }
    public void RemoveListner(GameEventListner listner)
    {
        listners.Remove(listner);
    }
}
