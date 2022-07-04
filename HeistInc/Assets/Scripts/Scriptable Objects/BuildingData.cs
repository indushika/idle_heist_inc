using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingData : ScriptableObject
{
    public Building[] Buildings; 
    public int Active_Building_Index; 
}
