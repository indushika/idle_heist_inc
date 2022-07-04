using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public float total_money; 
    public float time_per_turn;
    public int LootUpgrade_currentLevel;
    public int SpeedUpgrade_currentLevel; 
    public int CrewUpgrade_currentLevel; 
    public int OEUpgrade_currentLevel;
    public int Crew_size;
    public int Active_City_Index;
    public int Active_Building_Index;
    public int Player_Level;  
    public int Prestige_Level;  
    public float RevenueHitMarker_Level; 
    public float RevenueHitMarker_Level_Multiplier;   
    public float LevelUp_Reward_Multiplier;
    public float RemoveObjectsTime; 
    public float TappingAnimationSpeedMultiplier; 
    public float AnimationSpeed; 
    public float TappingLootSpeedMultiplier; 
    public float[] _loot_per_crew_member_PrestigeMultiplier;  
    public bool isTutorialStarted; 
    public bool isTutorialTapFaster; 
    public bool isTutorialUpgrade; 
    public bool isTutorialBoosts; 
    public bool isTutorialOver;  


}
