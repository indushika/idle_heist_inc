using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;

    public float TapFasterMoneyStamp; 
    public float UpgradeMoneyStamp; 
    public float BoostsMoneyStamp; 
    public GameEvent TutorialStartedEvent;
    public GameEvent TutorialTapFasterEvent; 
    public GameEvent TutorialUpgradeEvent; 
    public GameEvent TutorialBoostsEvent;




    public void TutorialStarted()
    {
        if (!gameManager.gameData.isTutorialStarted)
        {
            gameManager.gameData.isTutorialStarted = true;
            gameManager.InstantiateCrewMember();
            TutorialStartedEvent.Raise(); 
        }

        
    }

    public void TutorialTapFasterCheck()
    {
        if( !gameManager.gameData.isTutorialTapFaster && (gameManager.runtimeData.runtime_total_money > TapFasterMoneyStamp))
        {
            Debug.Log("check");
            gameManager.gameData.isTutorialTapFaster = true; 
            TutorialTapFasterEvent.Raise(); 
        } 
        
    }

    public void TutorialUpgradeCheck()
    {
        Debug.Log("checkUpgrade");

        if (!gameManager.gameData.isTutorialUpgrade)
        {
            UpgradeMoneyStamp = gameManager.upgradesData.lootUpgrades[gameManager.gameData.LootUpgrade_currentLevel + 1].Upgrade_Cost;

            if (gameManager.runtimeData.runtime_total_money > UpgradeMoneyStamp)
            {
                Debug.Log("checkUpgrade");
                gameManager.gameData.isTutorialUpgrade = true;
                TutorialUpgradeEvent.Raise();
            }
        }

    }  

    public void TutorialBoostsCheck()
    {
        if (!gameManager.gameData.isTutorialBoosts)
        {
            

            if (gameManager.runtimeData.runtime_total_money > BoostsMoneyStamp)
            {
                gameManager.gameData.isTutorialBoosts = true;
                TutorialBoostsEvent.Raise(); 
            }
        }
    } 
    public void TutorialEnded()
    {
        if(!gameManager.gameData.isTutorialOver)
        {
            gameManager.gameData.isTutorialOver = true;
        }

        Analytics.TutorialOverAnalytic(); 
    }
  
}
