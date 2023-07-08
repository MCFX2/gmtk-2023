using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISS : MonoBehaviour, InteractableObject
{

    public static ISS Instance;
    [SerializeField] private AchievementObj ImpostorAmongusAchievement;
    public static bool Killed { get; private set; } = false;
    private void Awake()
    {
        Instance = this;
    }

    void OnBoost()
    {
       if (!Killed)
       {
            Killed = true;
            AchievementSystem.AwardAchievement(ImpostorAmongusAchievement);
       } 
    }
    void OnTouch()
    {
       if (!Killed)
       {
            Killed = true;
            AchievementSystem.AwardAchievement(ImpostorAmongusAchievement);
       } 
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
