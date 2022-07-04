using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private GameData gameData;

    [SerializeField]
    private GameObject TutorialControllerObjects;

    public GameEvent GameStart; 
    public GameEvent TutorialStartedEvent;

    public bool EditorClear;

    void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK"); 
        }
    }

    void Start()
    {
        if(EditorClear)
        {
            PlayerPrefs.DeleteAll();
            OnInitialize();
        }


        OnLoad();


        if (!gameData.isTutorialStarted)
        {
            //play tutorial 
            OnInitialize();
            TutorialControllerObjects.SetActive(true); 

        }
        else
        {
            TutorialControllerObjects.SetActive(false);
            TutorialStartedEvent.Raise();

        }


        DontDestroyOnLoad(this.gameObject);
        GameStart.Raise(); 
        
    }


    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            OnSave();
        }

    }

    private void OnInitialize()
    {
        gameData.total_money = 0;
        gameData.LootUpgrade_currentLevel = 0;
        gameData.SpeedUpgrade_currentLevel = 0;
        gameData.CrewUpgrade_currentLevel = 0;
        gameData.Crew_size = 0;
        gameData.Player_Level = 0;
        gameData.RevenueHitMarker_Level = 500;
        gameData.Active_City_Index = 0;
        gameData.Active_Building_Index = 0;
        gameData.isTutorialStarted = false; 
        gameData.isTutorialTapFaster = false; 
        gameData.isTutorialUpgrade = false; 
        gameData.isTutorialBoosts = false; 
        gameData.isTutorialOver = false; 

    }


    private void OnLoad()
    {
        //load data from player prefs for now 

        //load up saved data to GameData 
        //active city index 
        //active building index 
        // crew size 
        //active crew members (using CrewUpgradeCurrentLevel -> everything before that index is active 

        //active objects -> check for player level vs stolen level if player level is greater than stolen level, object isActive = false 
        gameData.total_money = PlayerPrefs.GetFloat("total_money");
        gameData.LootUpgrade_currentLevel= PlayerPrefs.GetInt("LootUpgrade_currentLevel");
        gameData.SpeedUpgrade_currentLevel =  PlayerPrefs.GetInt("SpeedUpgrade_currentLevel");
        gameData.CrewUpgrade_currentLevel =  PlayerPrefs.GetInt("CrewUpgrade_currentLevel");
        gameData.Crew_size =  PlayerPrefs.GetInt("crew_size");
        gameData.Player_Level= PlayerPrefs.GetInt("player_level");
        gameData.RevenueHitMarker_Level = PlayerPrefs.GetFloat("RevenueHitMarker_Level");
        //gameData.RevenueHitMarker_Level_Multiplier =  PlayerPrefs.GetFloat("RevenueHitMarker_Level_Multiplier");
        //gameData.LevelUp_Reward_Multiplier =  PlayerPrefs.GetFloat("LevelUp_Reward_Multiplier");
        //gameData.TappingAnimationSpeedMultiplier =  PlayerPrefs.GetFloat("TappingAnimationSpeed");
        //gameData.AnimationSpeed =  PlayerPrefs.GetFloat("AnimationSpeed");
        //gameData.TappingAnimationSpeedMultiplier =  PlayerPrefs.GetFloat("TappingLootSpeedMultiplie");
        gameData.Active_City_Index =  PlayerPrefs.GetInt("Active_CityIndex");
        gameData.Active_Building_Index =  PlayerPrefs.GetInt("Active_Building");

        if (PlayerPrefs.GetInt("isTutorialStarted") == 1)
        {
            gameData.isTutorialStarted = true; 
        }
        else if (PlayerPrefs.GetInt("isTutorialStarted") == 0)
        {
            gameData.isTutorialStarted = false;

        }
        if (PlayerPrefs.GetInt("isTutorialTapFaster") == 1)
        {
            gameData.isTutorialTapFaster = true; 
        }
        else if (PlayerPrefs.GetInt("isTutorialTapFaster") == 0)
        {
            gameData.isTutorialTapFaster = false;

        }
        if (PlayerPrefs.GetInt("isTutorialUpgrade") == 1)
        {
            gameData.isTutorialUpgrade = true; 
        }
        else if (PlayerPrefs.GetInt("isTutorialUpgrade") == 0)
        {
            gameData.isTutorialUpgrade = false;

        }
        if (PlayerPrefs.GetInt("isTutorialBoosts") == 1)
        {
            gameData.isTutorialBoosts = true; 
        }
        else if (PlayerPrefs.GetInt("isTutorialBoosts") == 0)
        {
            gameData.isTutorialBoosts = false;

        }
        if (PlayerPrefs.GetInt("isTutorialOver") == 1)
        {
            gameData.isTutorialOver = true; 
        }
        else if (PlayerPrefs.GetInt("isTutorialOver") == 0)
        {
            gameData.isTutorialOver = false;

        }

    }

    private void OnSave()
    {
        //save data to player prefs for now 
        PlayerPrefs.SetFloat("total_money", gameData.total_money);
        PlayerPrefs.SetInt("LootUpgrade_currentLevel", gameData.LootUpgrade_currentLevel);
        PlayerPrefs.SetInt("SpeedUpgrade_currentLevel", gameData.SpeedUpgrade_currentLevel);
        PlayerPrefs.SetInt("CrewUpgrade_currentLevel", gameData.CrewUpgrade_currentLevel);
        PlayerPrefs.SetInt("crew_size", gameData.Crew_size);
        PlayerPrefs.SetInt("player_level", gameData.Player_Level);
        PlayerPrefs.SetFloat("RevenueHitMarker_Level", gameData.RevenueHitMarker_Level);
        //PlayerPrefs.SetFloat("RevenueHitMarker_Level_Multiplier", gameData.RevenueHitMarker_Level_Multiplier);
        //PlayerPrefs.SetFloat("LevelUp_Reward_Multiplier", gameData.LevelUp_Reward_Multiplier);
        //PlayerPrefs.SetFloat("TappingAnimationSpeed", gameData.TappingAnimationSpeedMultiplier);
        //PlayerPrefs.SetFloat("AnimationSpeed", gameData.AnimationSpeed);
        //PlayerPrefs.SetFloat("TappingLootSpeedMultiplier", gameData.TappingAnimationSpeedMultiplier);
        PlayerPrefs.SetInt("Active_CityIndex", gameData.Active_City_Index);
        PlayerPrefs.SetInt("Active_Building", gameData.Active_Building_Index); 

        if(gameData.isTutorialStarted)
        {
            PlayerPrefs.SetInt("isTutorialStarted", 1); 
        }
        else
        {
            PlayerPrefs.SetInt("isTutorialStarted", 0);
        }
        if(gameData.isTutorialTapFaster)
        {
            PlayerPrefs.SetInt("isTutorialTapFaster", 1); 
        }
        else
        {
            PlayerPrefs.SetInt("isTutorialTapFaster", 0);
        } 
        if(gameData.isTutorialUpgrade)
        {
            PlayerPrefs.SetInt("isTutorialUpgrade", 1); 
        }
        else
        {
            PlayerPrefs.SetInt("isTutorialUpgrade", 0);
        } 
        if(gameData.isTutorialBoosts)
        {
            PlayerPrefs.SetInt("isTutorialBoosts", 1); 
        }
        else
        {
            PlayerPrefs.SetInt("isTutorialBoosts", 0);
        } 
        if(gameData.isTutorialOver)
        {
            PlayerPrefs.SetInt("isTutorialOver", 1); 
        }
        else
        {
            PlayerPrefs.SetInt("isTutorialOver", 0);
        } 


    } 





}
