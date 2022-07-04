using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CrewMember : MonoBehaviour
{
    public int Crew_Index;
    public bool isActive;
    public Trait trait;
    public string Name;
    public Sprite CrewMemberImage; 
    public int CrewMember_Level;
    public float Loot_Amount;
    public int CrewUpgradeLevel;
    public Animator animator;
    public GameEvent[] BagPopEvent;
    public bool[] isBagPopped;

    public GameObject[] Bags; 

    public GameObject MoneyUpdateContainer;
    public float ActiveLootAmount; 

    public void SetActiveLootAmount(float ActLootAmount)
    {
        ActiveLootAmount = ActLootAmount; 
    }

    public void ActivateObjects(bool check)
    {

        MoneyUpdateContainer.SetActive(check);
        if(MoneyUpdateContainer.activeSelf)
        {
            Debug.Log("loot updater active" + ActiveLootAmount); 
            MoneyUpdateContainer.GetComponent<Score>().ActiveLootAmount = ActiveLootAmount;
        }
    }

    public void DeactivateObjects()
    {
        MoneyUpdateContainer.SetActive(false);

    } 

    public void RaiseEventBagPop(int i)
    {
        if (!isBagPopped[i])
        {
            BagPopEvent[i].Raise();
            isBagPopped[i] = true;
        }
    }

    
}
