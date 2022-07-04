using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradesData : ScriptableObject
{
    public LootUpgrade[] lootUpgrades;
    public SpeedUpgrade[] speedUpgrades;
    public CrewUpgrade[] crewUpgrades;
    public OEUpgrade[] oeUpgrades; 
}
