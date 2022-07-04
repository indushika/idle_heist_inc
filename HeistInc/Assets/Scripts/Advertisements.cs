using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Advertisements : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    string gameId = "3325230"; 
#elif UNITY_ANDROID
    string gameId = "3325231";
#endif

    public bool testMode = true;

    public string TripleLevelBonusPlacementID = "TripleLevelBonus";

    public GameEvent TripleLevelBonusEvent; 

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    } 

    public bool IsAdReady(string placementID)
    {
        if(Advertisement.IsReady(placementID))
        {
            return true; 
        }
        else
        {
            return false; 
        }
    }

    public void ShowRewardedVideo(string placementID)
    {
        Advertisement.Show(placementID);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion. 
            if(placementId == TripleLevelBonusPlacementID)
            {
                TripleLevelBonusEvent.Raise(); 
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

}
