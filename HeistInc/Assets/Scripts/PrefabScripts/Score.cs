using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Score : MonoBehaviour
{

    public GameEvent ScoreAnimationDoneEvent;

    public GameObject TextMeshScore;
    public float ActiveLootAmount; 

    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            TextMeshScore.GetComponent<TextMeshPro>().text = "+" + ActiveLootAmount + "$";
            TextMeshScore.transform.LookAt(Camera.main.transform);
            // Vector3 pos = Camera.main.transform.position;
            // //pos.y = 0;
            // pos.z = 0; 
            // transform.rotation = Quaternion.LookRotation(pos);  //Quaternion.Euler(0.0f, , 0.0f); 
        }
    }

    public void Disable()
    {
        ScoreAnimationDoneEvent.Raise(); 
    }
}
