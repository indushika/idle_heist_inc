using System.Collections.Generic;
using UnityEngine;
    
public class MapData : MonoBehaviour
{
    public CityData cityData;
    public int Number_of_cities;

    public Transform[] BuildingParents;
    public List<GameObject> ActiveObjects;

    public GameObject CameraContainer;
    public string[] GameWorldPersistentObjectsPositionAnimations;

    public ParticleSystem ObjectDisappearVFX; 
    public GameObject BagPoppingVFX; 

}
