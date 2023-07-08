using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class William : MonoBehaviour, InteractableObject
{
    public static William Instance;
    
    [SerializeField] private AchievementObj willYellAchievement;

    public static bool Killed { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
    }
    
    public void OnTouch()
    {
        if (!Killed)
        {
            Killed = true;
            // award achievement
        }
    }
    
    public void OnBoost()
    {
        if (!Killed)
        { 
            Killed = true;
            // award achievement
            AchievementSystem.AwardAchievement(willYellAchievement);
        }
    }
}
