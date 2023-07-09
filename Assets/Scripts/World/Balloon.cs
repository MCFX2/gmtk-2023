using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour, InteractableObject
{
    private static int balloonsLeft = 0;

    private ParticleSystem ps;
    private bool hit = false;

    [SerializeField] private AchievementObj _achievementObj;
    
    private void Awake()
    {
        balloonsLeft++;

        ps = GetComponent<ParticleSystem>();
    }

    public void OnBoost()
    {
        if(hit) return;
        hit = true;
        ps.Play();
        balloonsLeft--;
        if (balloonsLeft == 0)
        {
            AchievementSystem.AwardAchievement(_achievementObj);
        }

        GetComponent<SpriteRenderer>().enabled = false;
    }
}
