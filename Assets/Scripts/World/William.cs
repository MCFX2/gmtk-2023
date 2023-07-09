using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class William : MonoBehaviour, InteractableObject
{
    public static William Instance;
    
    [SerializeField] private AchievementObj willYellAchievement;

    public static bool Killed { get; private set; } = false;

    Animator animator;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0.2f;
    }
    
    public void OnTouch()
    {
        if (!Killed)
        {
            Killed = true;
            
            AchievementSystem.AwardAchievement(willYellAchievement);
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
