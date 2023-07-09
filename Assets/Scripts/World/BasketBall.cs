using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    public static BasketBall Instance;
    
    [SerializeField] private AchievementObj Rimshot;

    public static bool Achieved { get; private set; } = false;
    public BoxCollider2D block;
    public GameObject ball;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == ball)
        {
            AchievementSystem.AwardAchievement(Rimshot);
            block.enabled = false; 
        }
    }
}
