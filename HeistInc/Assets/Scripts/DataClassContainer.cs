using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class LootUpgrade
{
    public int UpgradeLevel;
    public float Loot_Multiplier;
    public float Upgrade_Cost; 
}


[System.Serializable] 
public class SpeedUpgrade   
{
    public int UpgradeLevel;
    public float Speed_Multiplier;
    public float Upgrade_Cost; 
}

[System.Serializable]
public class CrewUpgrade
{
    public int UpgradeLevel;
    public int Crew_Increment;
    public float Upgrade_Cost;

}

[System.Serializable]
public class OEUpgrade 
{
    public int UpgradeLevel;
    public float OfflineEarnings_TimeLimit; 
    public float Upgrade_Cost;
}  

[System.Serializable] 
public enum Status
{
    Locked, 
    Active, 
    Conquered
} 

[System.Serializable] 
public enum Trait
{
    Robber, 
    Hacker, 
    SecurityDetail
} 

[System.Serializable]
public class Building
{
    public int Building_Index;
    public string Building_name;
    public Status status;
    public int Unlock_level;
    public List<GameObject> ObjectPrefabs; 
    public int Number_of_objects;
    public float BuildingBonusMultiplier; 
}    

[System.Serializable] 
public class City
{
    public int City_Index;
    public string City_name;
    public BuildingData buildingData;
    public int number_of_buildings;
    public Status status;
    public int Unlock_level;
    public int Number_of_Levels; 
}




