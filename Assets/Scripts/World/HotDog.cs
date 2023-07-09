using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDog : MonoBehaviour, InteractableObject
{
    public static HotDog Instance;
    
    [SerializeField] private AchievementObj BreadalJacket;

    public SpriteRenderer hotdogguy;
    public Sprite saddogguy;

    public static bool Killed { get; private set; } = false;

    public void OnTouch()
    {
        if (!Killed)
        {
            Killed = true;
            // award achievement
            AchievementSystem.AwardAchievement(BreadalJacket);
            hotdogguy.flipX = true;
            hotdogguy.sprite = saddogguy;
        }
    }
    
    public void OnBoost()
    {
        if (!Killed)
        { 
            Killed = true;
            // award achievement
            AchievementSystem.AwardAchievement(BreadalJacket);
            hotdogguy.flipX = true;
            hotdogguy.sprite = saddogguy;
        }
    }
}
