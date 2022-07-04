using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeData : MonoBehaviour
{
    public float runtime_total_money; 
    public string runtime_total_moneyText; 
    public float runtime_time_per_turn;
    public float level_total_money; 
    public float level_bonus_money; 
    public List<GameObject> ActiveCrewMembers;
    public GameObject BoosterVan; 
    public bool InstantEarningsBoostActive;
    public bool DoubleEarningsBootsActive;
    public float DoubleEarningsBoostTime; 
    public bool AutomaticTappingBoostActive;
    public float AutomaticTappingBoostTime; 
    public bool isPlayerTapping; 
    public bool isGameStarted;
    public List<GameObject> Bags; 

}
