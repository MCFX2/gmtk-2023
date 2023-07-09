using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchBreak : MonoBehaviour, InteractableObject
{
    [SerializeField] private GameObject kitty;
    [SerializeField] private AchievementObj achievement;
    private bool activated = false;
    public void OnBoost()
    {
        if (activated) return;
        activated = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        // activate cat fall
        kitty.GetComponent<Kitty>().Fall();
        
        AchievementSystem.AwardAchievement(achievement);
        
    }
}
