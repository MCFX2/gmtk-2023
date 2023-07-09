using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour, InteractableObject
{
    public static Apple Instance;
    
    [SerializeField] private AchievementObj willTellAchievement;
    [SerializeField] private AchievementObj willFellAchievement;
    public GameObject gibs;

    public static bool Killed { get; private set; } = false;
    public static bool Touched{ get; private set; } = false;

    public void OnTouch()
    {
        if (!Touched)
        {
            Touched = true;
            AchievementSystem.AwardAchievement(willFellAchievement);
        }
    }
    
    public void OnBoost()
    {
        if (!Killed)
        { 
            Killed = true;
            // award achievement
            AchievementSystem.AwardAchievement(willTellAchievement);
            // spawn gibs and delete self
        }
    }
}
