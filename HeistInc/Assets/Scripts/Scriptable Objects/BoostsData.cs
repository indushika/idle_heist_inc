using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoostsData : ScriptableObject 
{
    public float InstantEarnings;  
    public float InstantEarningsReloadTime;  
    
    public float DoubleEarningsMultiplier;  
    public float DoubleEarningsTime;
    public float DoubleEarningsReloadTime;  

    public float AutomaticTappingBoostTime;
    public int NumberofAutomaticTaps; 
    public float AutomaticTappingBoostReloadTime;  
}


