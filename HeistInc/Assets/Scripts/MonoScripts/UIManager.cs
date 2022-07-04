using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager; 

    public Text moneyText;
    public GameObject TutorialTapFasterPanel; 
    public GameObject TutorialUpgradePanel;  
    public GameObject TutorialUpgradeArrowPanel;  
    public GameObject TutorialBoostsPanel;  
    public GameObject CrewMemberUnlockedPanel; 
    public GameObject LevelUpPanel;  
    public GameObject BuildingUpgradePanel;   
    public GameObject BoostsPanel;
    public GameObject BoostsVan;   
    public GameObject MapPanel;   

    [SerializeField]
    private Text LootUpgradeLevel,
                SpeedUpgradeLevel,
                CrewUpgradeLevel;  

     [SerializeField]
    private Text LootUpgradeCost,
                SpeedUpgradeCost,
                CrewUpgradeCost;

    [SerializeField]
    private Button LootUpgradeButton,
                SpeedUpgradeButton,
                CrewUpgradeButton;

    [SerializeField]
    private Sprite LootUpgradeButtonDisable, LootUpgradeButtonEnable,
            SpeedUpgradeButtonDisable, SpeedUpgradeButtonEnable,
            CrewUpgradeButtonDisable, CrewUpgradeButtonEnable;

    [SerializeField]
    private Button InstantBoostButton,
                DEBoostButton,
                ATBoostButton;
    [SerializeField]
    private Sprite InstantBoostDisable, InstantBoostEnable,
                DEBoostButtonDisable, DEBoostButtonEnable,
                ATBoostButtonDisable,  ATBoostButtonEnable;

    [SerializeField]
    private Image ATBoostActive, DEBoostActive;

    [SerializeField]
    private Text InstantBoostReloadTimeText,
                DEBoostReloadTimeText,
                ATBoostsReloadTimeText; 

    [SerializeField]
    private Text CrewMemberName,
                CrewMemberLevel,
                CrewMemberTrait,
                CrewMemberLootAmount; 

    [SerializeField]
    private Image CrewMemberImage;


    [SerializeField]
    private Slider LevelProgressSlider;
    [SerializeField]
    private Text LevelText, LevelBonusText;
    [SerializeField]
    private GameObject LevelUpButton;
    [SerializeField]
    private Button TripleLevelBonusButton; 


    public void MoneyUpdate()
    {
        gameManager.runtimeData.runtime_total_moneyText = Money.MoneyScore(gameManager.runtimeData.runtime_total_money, gameManager.moneySymbols.Symbols);
        moneyText.text = gameManager.runtimeData.runtime_total_moneyText;
    }

    public void LevelUpdate()
    {
        LevelText.text = "Level " + (gameManager.gameData.Player_Level + 1);
    }

    public void TutorialTapFasterPanelPopUp()
    {
        TutorialTapFasterPanel.SetActive(true);
    }

    public void TutorialTapFasterPanelPopDown()
    {
        TutorialTapFasterPanel.SetActive(false);
    } 

    public void TutorialUpgradePanelPopUp(bool check)
    {
        TutorialUpgradePanel.SetActive(check);
        TutorialUpgradeArrowPanel.SetActive(check); 
    }

    public void TutorialBoostsPanelPopUp(bool check)
    {
        TutorialBoostsPanel.SetActive(check); 

    }

    public void UpgradesUIUpdate()
    {
        if (gameManager.gameData.LootUpgrade_currentLevel < gameManager.upgradesData.lootUpgrades.Length-1)
        {
            float temp; 
            int NextLootUpgradeLevel = gameManager.gameData.LootUpgrade_currentLevel + 1;
            LootUpgradeLevel.text = "Level " + NextLootUpgradeLevel;
            temp = gameManager.upgradesData.lootUpgrades[NextLootUpgradeLevel].Upgrade_Cost; 
            LootUpgradeCost.text = Money.MoneyScore(temp,gameManager.moneySymbols.Symbols); 
        }
        else
        {
            int MaxLootUpgradeLevel = gameManager.gameData.LootUpgrade_currentLevel + 1;
            LootUpgradeLevel.text = "Level " + MaxLootUpgradeLevel;
            LootUpgradeCost.text = "MAX";

        }

        if (gameManager.gameData.SpeedUpgrade_currentLevel < gameManager.upgradesData.speedUpgrades.Length-1)
        {
            int NextSpeedUpgradeLevel = gameManager.gameData.SpeedUpgrade_currentLevel + 1;
            SpeedUpgradeLevel.text = "Level " + NextSpeedUpgradeLevel; 
            SpeedUpgradeCost.text = Money.MoneyScore(gameManager.upgradesData.speedUpgrades[NextSpeedUpgradeLevel].Upgrade_Cost,gameManager.moneySymbols.Symbols);
        }
        else
        {
            int MaxSpeedUpgradeLevel = gameManager.gameData.SpeedUpgrade_currentLevel + 1;
            SpeedUpgradeLevel.text = "Level " + MaxSpeedUpgradeLevel;
            SpeedUpgradeCost.text = "MAX";

        }
        if (gameManager.gameData.CrewUpgrade_currentLevel < gameManager.upgradesData.crewUpgrades.Length-1)
        {
            int NextCrewUpgradeLevel = gameManager.gameData.CrewUpgrade_currentLevel + 1;
            CrewUpgradeLevel.text = "Level " + NextCrewUpgradeLevel;
            CrewUpgradeCost.text = Money.MoneyScore( gameManager.upgradesData.crewUpgrades[NextCrewUpgradeLevel].Upgrade_Cost,gameManager.moneySymbols.Symbols);
        }
        else
        {
            int MaxCrewUpgradeLevel = gameManager.gameData.CrewUpgrade_currentLevel + 1;
            CrewUpgradeLevel.text = "Level " + MaxCrewUpgradeLevel;
            CrewUpgradeCost.text = "MAX";

        }
        UpgradesInteractebility(); 

    }
    public void UpgradesInteractebility()
    {
        if (gameManager.gameData.LootUpgrade_currentLevel < gameManager.upgradesData.lootUpgrades.Length-1)
        {
            int NextLootUpgradeLevel = gameManager.gameData.LootUpgrade_currentLevel + 1;
            if (gameManager.runtimeData.runtime_total_money >= gameManager.upgradesData.lootUpgrades[NextLootUpgradeLevel].Upgrade_Cost)
            {
                LootUpgradeButton.interactable = true;
                LootUpgradeButton.GetComponent<Image>().sprite = LootUpgradeButtonEnable; 

            }
            else
            {
                LootUpgradeButton.interactable = false;
                LootUpgradeButton.GetComponent<Image>().sprite = LootUpgradeButtonDisable;

            }
        } 
        else
        {
            LootUpgradeButton.interactable = false;
            LootUpgradeButton.GetComponent<Image>().sprite = LootUpgradeButtonDisable;

        }

        if (gameManager.gameData.SpeedUpgrade_currentLevel < gameManager.upgradesData.speedUpgrades.Length-1)
        {
            int NextSpeedUpgradeLevel = gameManager.gameData.SpeedUpgrade_currentLevel + 1;
            if (gameManager.runtimeData.runtime_total_money >= gameManager.upgradesData.speedUpgrades[NextSpeedUpgradeLevel].Upgrade_Cost)
            {
                SpeedUpgradeButton.interactable = true;
                SpeedUpgradeButton.GetComponent<Image>().sprite = SpeedUpgradeButtonEnable;

            }
            else
            {
                SpeedUpgradeButton.interactable = false;
                SpeedUpgradeButton.GetComponent<Image>().sprite = SpeedUpgradeButtonDisable;

            }
        }
        else
        {
            SpeedUpgradeButton.interactable = false;
            SpeedUpgradeButton.GetComponent<Image>().sprite = SpeedUpgradeButtonDisable;

        }

        if (gameManager.gameData.CrewUpgrade_currentLevel < gameManager.upgradesData.crewUpgrades.Length-1)
        {
            int NextCrewUpgradeLevel = gameManager.gameData.CrewUpgrade_currentLevel + 1;
            if (gameManager.runtimeData.runtime_total_money >= gameManager.upgradesData.crewUpgrades[NextCrewUpgradeLevel].Upgrade_Cost)
            {
                CrewUpgradeButton.interactable = true;
                CrewUpgradeButton.GetComponent<Image>().sprite = CrewUpgradeButtonEnable;

            }
            else
            {
                CrewUpgradeButton.interactable = false;
                CrewUpgradeButton.GetComponent<Image>().sprite = CrewUpgradeButtonDisable;

            }
        }
        else
        {
            CrewUpgradeButton.interactable = false;
            CrewUpgradeButton.GetComponent<Image>().sprite = CrewUpgradeButtonDisable;

        }


    }

    public void CrewMemberUnlockedPanelUpdate()
    {
        CrewMemberName.text = "Name: " + gameManager.crewData.CrewPrefabs[gameManager.gameData.CrewUpgrade_currentLevel].GetComponent<CrewMember>().Name; 
        CrewMemberLevel.text = "Level: " + gameManager.crewData.CrewPrefabs[gameManager.gameData.CrewUpgrade_currentLevel].GetComponent<CrewMember>().CrewMember_Level;  
        CrewMemberTrait.text = "Trait: " + gameManager.crewData.CrewPrefabs[gameManager.gameData.CrewUpgrade_currentLevel].GetComponent<CrewMember>().trait;   
        CrewMemberLootAmount.text = "Loot Amount: " + gameManager.crewData.CrewPrefabs[gameManager.gameData.CrewUpgrade_currentLevel].GetComponent<CrewMember>().Loot_Amount;
        CrewMemberImage.sprite = gameManager.crewData.CrewPrefabs[gameManager.gameData.CrewUpgrade_currentLevel].GetComponent<CrewMember>().CrewMemberImage ;  

        CrewMemberUnlockedPanel.SetActive(true); 

    }
    public void CrewMemberUnlockedPanelDeactivate()
    {
        CrewMemberUnlockedPanel.SetActive(false);

    }

    public void  BoostsVanActivate(bool check)
    {
        BoostsVan.SetActive(check); 
    }
    public void  BoostsVanColliderActivate()
    {
        if(gameManager.gameData.isTutorialBoosts)
        {
            BoostsVan.GetComponent<BoxCollider>().enabled = true;

        }
    }
    public void BoostsPanelActivate(bool check)
    {
        BoostsPanel.SetActive(check);
        gameManager.TutorialOverEventCall();  
    } 

    public void InstantEarningsBoost()
    {
        gameManager.InstantEarningsBoosts();
        InstantBoostButton.interactable = false;
        InstantBoostButton.GetComponent<Image>().sprite = InstantBoostDisable;
        StartCoroutine(InstantBoostReloadCountDown());
    }
    IEnumerator InstantBoostReloadCountDown()
    {
        for (int i = (int)gameManager.boostsData.InstantEarningsReloadTime; i > 0; i--)
        {
            if (i > 3599)
            {
                int hours = i / 3600;
                int seconds = i % 3600;
                int minutes = seconds / 60;
                seconds = seconds % 60;
                InstantBoostReloadTimeText.text = hours + ":" + minutes + ":" + seconds;
            }
            if (i > 59)
            {
                int minutes = i / 60;
                int seconds = i % 60;
                InstantBoostReloadTimeText.text = "00:" + minutes + ":" + seconds;
            }
            else
            {
                InstantBoostReloadTimeText.text = "00:00:" + i;

            }

            yield return new WaitForSeconds(1);
            //gameManager.boostsData.DoubleEarningsReloadTime -= 1; 
        }

        InstantBoostReloadTimeText.text = "Ready!";
        InstantBoostButton.interactable = true;
        InstantBoostButton.GetComponent<Image>().sprite = InstantBoostEnable;

    }

    public void DoubleEarningsBoost()
    {
        gameManager.DoubleEarningsBoost();
        DEBoostButton.interactable = false;
        DEBoostButton.GetComponent<Image>().sprite = DEBoostButtonDisable;
        StartCoroutine(DEBoostReloadCountDown()); 
        StartCoroutine(DEBoostActiveProgress()); 
    } 
    IEnumerator DEBoostReloadCountDown()
    {
        for (int i = (int)gameManager.boostsData.DoubleEarningsReloadTime; i >0 ; i--)
        {
            if(i>3599)
            {
                int hours = i / 3600;
                int seconds = i % 3600; 
                int minutes = seconds / 60;
                seconds = seconds % 60;
                DEBoostReloadTimeText.text = hours + ":" + minutes + ":" + seconds;
            }
            if (i >59)
            {
                int minutes = i / 60; 
                int seconds =i % 60;
                DEBoostReloadTimeText.text = "00:" + minutes + ":" + seconds; 
            }
            else
            {
                DEBoostReloadTimeText.text = "00:00:" + i; 

            }

            yield return new WaitForSeconds(1);
            //gameManager.boostsData.DoubleEarningsReloadTime -= 1; 
        }
        DEBoostReloadTimeText.text = "Ready!";

        DEBoostButton.interactable = true;
        DEBoostButton.GetComponent<Image>().sprite = DEBoostButtonEnable;        

    }
    IEnumerator DEBoostActiveProgress()
    {
        for (int i = (int)gameManager.boostsData.DoubleEarningsTime; i >=0 ; i--)
        {
            DEBoostActive.fillAmount = i / gameManager.boostsData.DoubleEarningsTime; 
            yield return new WaitForSeconds(1);

        }
    }

    public void AutomaticTappingBoost()
    {
        gameManager.AutomaticTappingBoost();
        ATBoostButton.interactable = false;
        ATBoostButton.GetComponent<Image>().sprite = ATBoostButtonDisable;
        StartCoroutine(ATBoostReloadCountDown()); 
        StartCoroutine(ATBoostActiveProgress()); 
    }
    IEnumerator ATBoostReloadCountDown()
    {
        for (int i = (int)gameManager.boostsData.AutomaticTappingBoostReloadTime; i > 0; i--)
        {
            if (i > 3599)
            {
                int hours = i / 3600;
                int seconds = i % 3600;
                int minutes = seconds / 60;
                seconds = seconds % 60;
             ATBoostsReloadTimeText.text = hours + ":" + minutes + ":" + seconds;
            }
            if (i > 59)
            {
                int minutes = i / 60;
                int seconds = i % 60;
                ATBoostsReloadTimeText.text = "00:" + minutes + ":" + seconds;
            }
            else
            {
                ATBoostsReloadTimeText.text = "00:00:" + i;

            }

            yield return new WaitForSeconds(1);
            //gameManager.boostsData.DoubleEarningsReloadTime -= 1; 
        }
        ATBoostsReloadTimeText.text = "Ready!";
        ATBoostButton.interactable = true;
        ATBoostButton.GetComponent<Image>().sprite = ATBoostButtonEnable; 

    }
    IEnumerator ATBoostActiveProgress()
    {
        for (int i = (int)gameManager.boostsData.AutomaticTappingBoostTime; i >= 0; i--)
        {
            ATBoostActive.fillAmount = i / gameManager.boostsData.AutomaticTappingBoostTime;
            yield return new WaitForSeconds(1);

        }
    }

    public void LevelProgressUpdate()
    {
        if (gameManager.runtimeData.level_total_money < gameManager.RevenueHitMarker())
        {
            LevelProgressSlider.value = gameManager.runtimeData.level_total_money / gameManager.RevenueHitMarker();
        } 
        else
        {
            Debug.Log("Level Up"); 
            LevelUpButton.SetActive(true);
        }
    } 

    public void LevelUp(bool check)
    {
        LevelUpButton.SetActive(check); 
        gameManager.LevelUp();
        LevelBonusText.text = Money.MoneyScore(gameManager.runtimeData.level_bonus_money, gameManager.moneySymbols.Symbols).ToString();
        LevelUpdate(); 
        LevelProgressUpdate(); 
        LevelUpPanel.SetActive(true);
        if (gameManager.advertisements.IsAdReady(gameManager.advertisements.TripleLevelBonusPlacementID))
        {
            TripleLevelBonusButton.interactable = true; 
        }
        else
        {
            TripleLevelBonusButton.interactable = false;
        }
        //play level panel active animation 
    } 

    public void CollectLevelBonus()
    {
        LevelUpPanel.SetActive(false);
        //play out the level panel playing out animation 
        gameManager.CollectLevelUpBonus();
        MoneyUpdate(); 
        //play animation for bonus adding up 
    } 
    
    public void TripleLevelBonus()
    {
        LevelUpPanel.SetActive(false);
        //play out the level panel playing out animation 
        gameManager.advertisements.ShowRewardedVideo(gameManager.advertisements.TripleLevelBonusPlacementID); 
        //play animation for bonus adding up 
    } 

    public void BuildingUpgrade()
    {
        BuildingUpgradePanel.SetActive(true); 

    } 
    public void UpgradeToBuilding()
    {
        BuildingUpgradePanel.SetActive(false);
        gameManager.BuildingTransition(); 
    }  

    public void ActivateMapPanel(bool check)
    {
        MapPanel.SetActive(check); 
    }
}

