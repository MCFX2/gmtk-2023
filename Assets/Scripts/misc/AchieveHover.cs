using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AchieveHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AchievementObj achievement;

    [SerializeField] private GameObject descPanel;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    private void Awake()
    {
        var img = GetComponent<UnityEngine.UI.Image>();
        img.sprite = achievement.thumbnail;

        var col = img.color;
        col.a = 0.5f;
        img.color = col;
        
        AchievementSystem.OnAchievementUnlocked += OnAchieve;
    }

    private void OnDestroy()
    {
        AchievementSystem.OnAchievementUnlocked -= OnAchieve;
    }

    private void OnAchieve()
    {
        if (AchievementSystem.HasAchievement(achievement))
        { 
            var img = GetComponent<UnityEngine.UI.Image>();
            var col = img.color;
            col.a = 1.0f;
            img.color = col;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descPanel.gameObject.SetActive(true);
        if (AchievementSystem.HasAchievement(achievement))
        {
            title.text = achievement.title;
        }
        else
        {
            title.text = "???";
        }
        description.text = achievement.description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (title.text == achievement.title)
        {
            descPanel.SetActive(false);
        }
    }
}
