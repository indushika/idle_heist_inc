using System.Collections.Generic;
using Facebook.Unity;


public class Analytics
{
    public static void TutorialOverAnalytic()
    {
        var tutParams = new Dictionary<string, object>();
        tutParams[AppEventParameterName.ContentID] = "tutorial_over";
        tutParams[AppEventParameterName.Description] = "Last step in the tutorial, tutorial completed";
        tutParams[AppEventParameterName.Success] = "1";

        FB.LogAppEvent(
            AppEventName.CompletedTutorial,
            parameters: tutParams
        );
    }

    public static void LevelUpAnalytic(int Level)
    {
        var tutParams = new Dictionary<string, object>();
        tutParams[AppEventParameterName.ContentID] = "Level_Up";
        tutParams[AppEventParameterName.Description] = Level.ToString();
        tutParams[AppEventParameterName.Success] = "1";

        FB.LogAppEvent(
            AppEventName.AchievedLevel,
            parameters: tutParams
        );
    }

    public static void UnlockNewBuilding(int BuildingLevel)
    {
        var tutParams = new Dictionary<string, object>();
        tutParams[AppEventParameterName.ContentID] = "Building Upgrade";
        tutParams[AppEventParameterName.Description] = "Upgraded to new building: Building " + BuildingLevel.ToString();
        tutParams[AppEventParameterName.Success] = "1";

        FB.LogAppEvent(
            AppEventName.UnlockedAchievement,
            parameters: tutParams
        );
    }  

    
    public static void RVAdOnLevelBonusTriple()
    {
        var tutParams = new Dictionary<string, object>();
        tutParams[AppEventParameterName.ContentID] = "Reward Video Ad";
        tutParams[AppEventParameterName.Description] = "Rewarded Video Ad played on Level Bonus Tripling";
        tutParams[AppEventParameterName.Success] = "1";

        FB.LogAppEvent(
            AppEventName.ViewedContent,
            parameters: tutParams
        );
    }  



}
