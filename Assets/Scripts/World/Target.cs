using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, InteractableObject
{
    [SerializeField] private AchievementObj achievement;

    private bool hit = false;
    
    public void OnTouch()
    {
        if (!hit)
        {
            hit = true;
            AchievementSystem.AwardAchievement(achievement);
        }
    }

    public void OnBoost()
    {
        if (!hit)
        {
            hit = true;
            AchievementSystem.AwardAchievement(achievement);
        }
    }
}
