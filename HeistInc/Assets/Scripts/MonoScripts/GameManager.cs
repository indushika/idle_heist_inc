using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public RuntimeData runtimeData;
    public GameData gameData;
    public UpgradesData upgradesData;
    public BoostsData boostsData;
    public CrewData crewData;
    public MapData mapData;

    public Advertisements advertisements; 
    public MoneySymbols moneySymbols; 

    private float timePerturn;
    private bool ObjectsRemoved;

    //Game Events 
    public GameEvent MoneyUpdateEvent;  
    public GameEvent LevelProgressUpdateEvent;          
    public GameEvent BuildingUpgrade;          
    public GameEvent MapPanelActivateEvent;          
    public GameEvent TutorialOverEvent;          
   


    private void Start()
    {
        LoadRuntimeData();
        MoneyUpdateEvent.Raise();

    }

    private void Update()
    {
        if (runtimeData.isGameStarted)
        {
            if (gameData.isTutorialStarted)
            {
                timePerturn += Time.deltaTime;
                if (timePerturn >= TimePerTurn())
                {
                    TotalMoneyPerTurn(CrewMoneyScorePerTurn());
                    Debug.Log(runtimeData.runtime_total_money + "\n" + timePerturn + "s"); 
                    timePerturn = 0;
                    MoneyUpdateEvent.Raise();
                }

            }

            if (runtimeData.DoubleEarningsBootsActive)
            {
                runtimeData.DoubleEarningsBoostTime += Time.deltaTime;
                if (runtimeData.DoubleEarningsBoostTime > boostsData.DoubleEarningsTime)
                {
                    runtimeData.DoubleEarningsBoostTime = 0.0f;
                    runtimeData.DoubleEarningsBootsActive = false;
                }
            }

            if (runtimeData.AutomaticTappingBoostActive)
            {
                AutomaticTappingAnimation(runtimeData.AutomaticTappingBoostActive);
                runtimeData.AutomaticTappingBoostTime += Time.deltaTime;
                if (runtimeData.AutomaticTappingBoostTime > boostsData.AutomaticTappingBoostTime)
                {
                    Debug.Log("ATtemptime: " + Time.time);
                    runtimeData.AutomaticTappingBoostTime = 0.0f;
                    runtimeData.AutomaticTappingBoostActive = false;
                    AutomaticTappingAnimation(runtimeData.AutomaticTappingBoostActive);
                }
            }
        }

    }

    public void GameStartCheck(bool check)
    {
        runtimeData.isGameStarted = check; 
    }

    public void LoadRuntimeData()
    {
        runtimeData.runtime_time_per_turn = gameData.time_per_turn;
        runtimeData.runtime_total_money = gameData.total_money; 
        for (int i = 0; i < gameData.Crew_size; i++)
        {
            runtimeData.ActiveCrewMembers.Add((Instantiate(crewData.CrewPrefabs[i])));
            PlayAnimationsForCrews(i,true); 
        }

        //Move Camera position to current building  
        mapData.CameraContainer.GetComponent<Animator>().Play(mapData.GameWorldPersistentObjectsPositionAnimations[gameData.Active_Building_Index]); 
        runtimeData.BoosterVan.GetComponent<Animator>().Play(mapData.GameWorldPersistentObjectsPositionAnimations[gameData.Active_Building_Index]); 
        Debug.Log("Camera moveed to building: " + mapData.CameraContainer.transform.position); 
    }


    public float CrewMoneyScorePerTurn()
    {
        float result = 0;

        float Processed_Loot_Amount;
        for (int i = 0; i < gameData.Crew_size; i++)
        {
            float raw_Loot_Amount = runtimeData.ActiveCrewMembers[i].GetComponent<CrewMember>().Loot_Amount;

            float PrestigeMultiplierApplied = raw_Loot_Amount * gameData._loot_per_crew_member_PrestigeMultiplier[gameData.Prestige_Level];
            float LootMultiplierApplied = PrestigeMultiplierApplied * upgradesData.lootUpgrades[gameData.LootUpgrade_currentLevel].Loot_Multiplier;
            float BoostMultiplierApplied = 0;
            float BuildingMultiplierApplied = raw_Loot_Amount * mapData.cityData.Cities[gameData.Active_City_Index].buildingData.Buildings[gameData.Active_Building_Index].BuildingBonusMultiplier;

            if (runtimeData.DoubleEarningsBootsActive)
            {
                BoostMultiplierApplied = PrestigeMultiplierApplied * boostsData.DoubleEarningsMultiplier;
            }

            Processed_Loot_Amount = PrestigeMultiplierApplied + LootMultiplierApplied + BoostMultiplierApplied + BuildingMultiplierApplied;
            runtimeData.ActiveCrewMembers[i].GetComponent<CrewMember>().ActiveLootAmount = Processed_Loot_Amount;
            result += Processed_Loot_Amount;
        }
        return result;
    }  

    public void TotalMoneyPerTurn(float CrewMoneyPerTurn)
    {
        runtimeData.runtime_total_money += CrewMoneyPerTurn;
        gameData.total_money = runtimeData.runtime_total_money;

        if (runtimeData.isPlayerTapping || runtimeData.AutomaticTappingBoostActive || runtimeData.DoubleEarningsBootsActive || runtimeData.InstantEarningsBoostActive)
        {
            runtimeData.level_total_money += CrewMoneyPerTurn;
            LevelProgressUpdateEvent.Raise(); 
        }
    } 
     


    public  float RevenueHitMarker()
    {
        float result;
        result = gameData.RevenueHitMarker_Level; 
        return result; 
    }

    public void InstantiateCrewMember()
    {
        runtimeData.ActiveCrewMembers.Add(Instantiate(crewData.CrewPrefabs[gameData.CrewUpgrade_currentLevel]));
        PlayAnimationsForCrews(gameData.Crew_size,true); 
        gameData.Crew_size += 1; 
    }


    public float TimePerTurn()
    {
        float result = 0;

        if(runtimeData.isPlayerTapping)
        {
            runtimeData.runtime_time_per_turn = gameData.time_per_turn * gameData.TappingLootSpeedMultiplier;
        } 
        else if(runtimeData.AutomaticTappingBoostActive)
        {
            runtimeData.runtime_time_per_turn = gameData.time_per_turn * gameData.TappingLootSpeedMultiplier;
        }
        else
        {
            runtimeData.runtime_time_per_turn = gameData.time_per_turn;
        }

        result = runtimeData.runtime_time_per_turn * upgradesData.speedUpgrades[gameData.SpeedUpgrade_currentLevel].Speed_Multiplier;

        return result; 

    }

    public void  TapHold()
    {
        runtimeData.isPlayerTapping = true; 

    }

    public void  TapRelease()
    {
        runtimeData.isPlayerTapping = false;
    }

    public void TapHoldReleaseAnimation()
    {
        for (int i = 0; i < gameData.Crew_size; i++)
        {

            if (runtimeData.isPlayerTapping)
            {
                runtimeData.ActiveCrewMembers[i].GetComponent<CrewMember>().animator.speed = gameData.TappingAnimationSpeedMultiplier; 
            }
            else
            {
                runtimeData.ActiveCrewMembers[i].GetComponent<CrewMember>().animator.speed = AnimationSpeedUpgrade();
            }
        } 
    }

    public void AutomaticTappingAnimation(bool check)
    {
        for (int i = 0; i < gameData.Crew_size; i++)
        {

            if (check)
            {
                runtimeData.ActiveCrewMembers[i].GetComponent<CrewMember>().animator.speed = gameData.TappingAnimationSpeedMultiplier; 
            }
            else 
            {
                runtimeData.ActiveCrewMembers[i].GetComponent<CrewMember>().animator.speed = AnimationSpeedUpgrade();
            }
            Debug.Log("speed: " + runtimeData.ActiveCrewMembers[i].GetComponent<CrewMember>().animator.speed); 
        }

        if (check)
        {
            runtimeData.BoosterVan.GetComponent<Animator>().speed = gameData.TappingAnimationSpeedMultiplier;

        }
        else
        {
            runtimeData.BoosterVan.GetComponent<Animator>().speed = AnimationSpeedUpgrade();
        }

    }

    public void LootUpgrade()
    {
        if (gameData.LootUpgrade_currentLevel < upgradesData.lootUpgrades.Length-1)
        {
            int NextLootUpgradeLevel = gameData.LootUpgrade_currentLevel + 1;
            if (runtimeData.runtime_total_money >= upgradesData.lootUpgrades[NextLootUpgradeLevel].Upgrade_Cost)
            {
                runtimeData.runtime_total_money -= upgradesData.lootUpgrades[NextLootUpgradeLevel].Upgrade_Cost;
                gameData.total_money = runtimeData.runtime_total_money;
                MoneyUpdateEvent.Raise();
                gameData.LootUpgrade_currentLevel = NextLootUpgradeLevel;
            }
        }
    }

    public void SpeedUpgrade()
    {
        if (gameData.SpeedUpgrade_currentLevel < upgradesData.speedUpgrades.Length-1)
        {
            int NextSpeedUpgradeLevel = gameData.SpeedUpgrade_currentLevel + 1;
            if (runtimeData.runtime_total_money >= upgradesData.speedUpgrades[NextSpeedUpgradeLevel].Upgrade_Cost)
            {
                runtimeData.runtime_total_money -= upgradesData.speedUpgrades[NextSpeedUpgradeLevel].Upgrade_Cost;
                gameData.total_money = runtimeData.runtime_total_money;
                MoneyUpdateEvent.Raise();
                gameData.SpeedUpgrade_currentLevel = NextSpeedUpgradeLevel;
            }
        }
    }

    public void CrewUpgrade()
    {
        if (gameData.CrewUpgrade_currentLevel < upgradesData.crewUpgrades.Length-1)
        {

            int NextCrewUpgradeLevel =gameData.CrewUpgrade_currentLevel + 1;
            if (runtimeData.runtime_total_money >= upgradesData.crewUpgrades[NextCrewUpgradeLevel].Upgrade_Cost)
            {
                runtimeData.runtime_total_money -= upgradesData.crewUpgrades[NextCrewUpgradeLevel].Upgrade_Cost;
                gameData.total_money = runtimeData.runtime_total_money;
                MoneyUpdateEvent.Raise();
                gameData.CrewUpgrade_currentLevel = NextCrewUpgradeLevel;
            }
        }

    }


    public void InstantEarningsBoosts()
    {
        runtimeData.runtime_total_money += boostsData.InstantEarnings;
        gameData.total_money = runtimeData.runtime_total_money; 
        MoneyUpdateEvent.Raise();

    }

    public void DoubleEarningsBoost()
    {
        runtimeData.DoubleEarningsBootsActive = true; 
    }  

    public void AutomaticTappingBoost()
    {
        runtimeData.AutomaticTappingBoostActive = true;
        //StartCoroutine(AutomaticTappingBoostCounter()); 
        Debug.Log("ATtemptime: " + Time.time);

    }


    public void LevelUp()
    {
        runtimeData.level_bonus_money = runtimeData.level_total_money * gameData.LevelUp_Reward_Multiplier;
        gameData.Player_Level += 1; 
        runtimeData.level_total_money = 0; 
        gameData.RevenueHitMarker_Level += gameData.RevenueHitMarker_Level * gameData.RevenueHitMarker_Level_Multiplier;
        Analytics.LevelUpAnalytic(gameData.Player_Level); 
    }
    public void CollectLevelUpBonus()
    {
        runtimeData.runtime_total_money += runtimeData.level_bonus_money;
        gameData.total_money = runtimeData.runtime_total_money;
        MoneyUpdateEvent.Raise(); 
        RemoveObjects();
        BuildingUpgradeLevelUp(); 
    }

    public void TripleLevelUpBonus()
    {
        Analytics.RVAdOnLevelBonusTriple(); 
        runtimeData.runtime_total_money += (runtimeData.level_bonus_money*3);
        gameData.total_money = runtimeData.runtime_total_money;
        MoneyUpdateEvent.Raise(); 
        RemoveObjects();
        BuildingUpgradeLevelUp(); 
    }

  public int ActiveBuildingUnlockLevel()
    {
        int result;

        BuildingData buildingData = mapData.cityData.Cities[gameData.Active_City_Index].buildingData;

        result = buildingData.Buildings[gameData.Active_Building_Index].Unlock_level; 
        return result; 
    }

    public void InstantiateObjects()
    {

        mapData.ActiveObjects.Clear(); 
        BuildingData buildingData = mapData.cityData.Cities[gameData.Active_City_Index].buildingData;
        if (buildingData.Buildings.Length > gameData.Active_Building_Index)
        {
            int ObjectCount = mapData.cityData.Cities[gameData.Active_City_Index].buildingData.Buildings[gameData.Active_Building_Index].ObjectPrefabs.Count;
            for (int i = 0; i < ObjectCount; i++)
            {
                if (gameData.Player_Level <= (buildingData.Buildings[gameData.Active_Building_Index].ObjectPrefabs[i].GetComponent<Objects>().Stolen_Level+ ActiveBuildingUnlockLevel()))
                {
                    mapData.ActiveObjects.Add(Instantiate(buildingData.Buildings[gameData.Active_Building_Index].ObjectPrefabs[i], mapData.BuildingParents[gameData.Active_Building_Index]));
                }
            }
        }
    }
    public void RemoveObjects()
    {

        int ObjectCount = mapData.ActiveObjects.Count;
        ObjectsRemoved = false;
        StartCoroutine(RemoveObjectsCoroutine(ObjectCount)); 
    } 

    IEnumerator RemoveObjectsCoroutine(int ObjectCount)
    {
        for (int i = 0; i < ObjectCount; i++)
        {
            if ((mapData.ActiveObjects[i].GetComponent<Objects>().Stolen_Level +ActiveBuildingUnlockLevel()) == (gameData.Player_Level-1))
            {
                yield return new WaitForSeconds(gameData.RemoveObjectsTime); 
                mapData.ActiveObjects[i].SetActive(false);
                mapData.ObjectDisappearVFX.gameObject.transform.SetParent(mapData.BuildingParents[gameData.Active_Building_Index]);
                mapData.ObjectDisappearVFX.gameObject.transform.localPosition = mapData.ActiveObjects[i].transform.localPosition;
                mapData.ObjectDisappearVFX.Play();  
                //mapData.ActiveObjects.RemoveAt(i);     
            }
        }
        ObjectsRemoved = true; 

    }

    public void BuildingUpgradeLevelUp()
    {
        BuildingData buildingData = mapData.cityData.Cities[gameData.Active_City_Index].buildingData;
        if (buildingData.Buildings.Length > gameData.Active_Building_Index+1)
        {
            int BuildingUpgradeLevel = buildingData.Buildings[gameData.Active_Building_Index + 1].Unlock_level;
            if (gameData.Player_Level == BuildingUpgradeLevel)
            {
                StartCoroutine(BuildingTransitonCoroutine());

            }  
        }
        else if (mapData.cityData.Cities[gameData.Active_City_Index].Number_of_Levels == gameData.Player_Level)
        {
            StartCoroutine(CityTransitonCoroutine());

        }
    }     

    IEnumerator BuildingTransitonCoroutine()
    {
        yield return new WaitUntil(() => ObjectsRemoved == true);
        
        BuildingUpgrade.Raise();


    }

    IEnumerator CityTransitonCoroutine()
    {
        yield return new WaitUntil(() => ObjectsRemoved == true);
        MapPanelActivateEvent.Raise();


    }

    public void BuildingTransition()
    {

        BuildingData buildingData = mapData.cityData.Cities[gameData.Active_City_Index].buildingData;
        PlayAnimationsForBoosterVan();

        for (int i = 0; i < gameData.Crew_size; i++)
        {
            PlayAnimationsForCrews(i, false); 
        }

        string stateName = "Building" + gameData.Active_Building_Index + (gameData.Active_Building_Index + 1);
        mapData.CameraContainer.GetComponent<Animator>().Play(stateName);


        buildingData.Buildings[gameData.Active_Building_Index].status = Status.Conquered;
        gameData.Active_Building_Index += 1;
        Analytics.UnlockNewBuilding(gameData.Active_Building_Index); 
        buildingData.Buildings[gameData.Active_Building_Index].status = Status.Active;
        InstantiateObjects(); 
    } 

    public float AnimationSpeedUpgrade()
    {
        float result;

        result = gameData.AnimationSpeed * (1/ upgradesData.speedUpgrades[gameData.SpeedUpgrade_currentLevel].Speed_Multiplier); 
        return result; 
    } 

    public void PlayAnimationsForCrews(int CrewMemberIndex, bool Robbing)
    {
        if(Robbing)
        {
            string stateName;
            stateName = "Building" + gameData.Active_Building_Index;
            runtimeData.ActiveCrewMembers[CrewMemberIndex].GetComponent<CrewMember>().animator.Play(stateName);
        }
        else
        {
            string stateName;
            stateName = "BuildingTransition" + gameData.Active_Building_Index  + (gameData.Active_Building_Index+1);
            runtimeData.ActiveCrewMembers[CrewMemberIndex].GetComponent<CrewMember>().animator.Play(stateName);
        }
    }

    public void PlayAnimationsForBoosterVan()
    {
        string stateNameBoosterVan = "Building" + gameData.Active_Building_Index + (gameData.Active_Building_Index + 1);
        runtimeData.BoosterVan.GetComponent<Animator>().Play(stateNameBoosterVan); 
    }

    public void BagPop(int BagIndex)
    {
        for (int i = 0; i < gameData.Crew_size; i++)
        {
            if(crewData.CrewPrefabs[i].GetComponent<CrewMember>().Bags.Length >BagIndex)
            {
                int runtimeBagIndex;
                GameObject temp = Instantiate(crewData.CrewPrefabs[i].GetComponent<CrewMember>().Bags[BagIndex], mapData.BuildingParents[gameData.Active_Building_Index]);
                runtimeData.Bags.Add(temp);
                runtimeBagIndex = runtimeData.Bags.IndexOf(temp); 
                
                mapData.BagPoppingVFX.gameObject.transform.SetParent(mapData.BuildingParents[gameData.Active_Building_Index]);
                mapData.BagPoppingVFX.gameObject.transform.localPosition = runtimeData.Bags[runtimeBagIndex].transform.localPosition;
                mapData.BagPoppingVFX.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
        
    }

    public void TutorialOverEventCall()
    {
        TutorialOverEvent.Raise(); 
    }
}