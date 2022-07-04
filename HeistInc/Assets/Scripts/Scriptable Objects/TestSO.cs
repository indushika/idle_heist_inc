using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Test", menuName = "Test")]
public class TestSO : ScriptableObject
{
    public GameObject[] prefabs;
    public int value; 
}
