using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem Instance { get; private set; }
    
    [SerializeField] private GameObject achievementPrefab;

    [SerializeField] private TransitionList animations;
    
    private GameObject popup;
    
    private void Awake()
    {
        Instance = this;
        
        popup = Instantiate(Instance.achievementPrefab, Instance.transform);
        popup.SetActive(false);
    }
    
    private static readonly List<AchievementObj> EarnedAchievements = new();
    
    public delegate void AchievementUnlockEvent();
    
    public static event AchievementUnlockEvent OnAchievementUnlocked;

    public static bool HasAchievement(AchievementObj achievement)
    {
        return EarnedAchievements.Contains(achievement);
    }

    public static void AwardAchievement(AchievementObj achievement)
    {
        if (HasAchievement(achievement)) return;
        
        EarnedAchievements.Add(achievement);

        OnAchievementUnlocked?.Invoke();

        Instance.popup.SetActive(true);

        var tr = Instance.popup.GetComponent<RectTransform>();
        //tr.anchoredPosition = new Vector2(-64, -300);
        tr.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            achievement.title;
        tr.GetChild(1).GetComponent<Image>().sprite = achievement.thumbnail;

        Instance.StartCoroutine(Instance.animations.Play(Instance.popup, () => Instance.popup.SetActive(false)));
        
    }
}
