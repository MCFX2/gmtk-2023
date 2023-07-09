using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AchieveHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AchievementObj achievement;

    [SerializeField] private GameObject descPanel;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    private Image img;
    private void Awake()
    {
        img = GetComponent<Image>();
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
        
        var col = img.color;
        col.r = 0.7f;
        col.g = 0.7f;
        col.b = 0.7f;
        img.color = col;
        
        AudioManager.Play("click");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var col = img.color;
        col.r = 1f;
        col.g = 1f;
        col.b = 1f;
        img.color = col;
        
        if (title.text == achievement.title)
        {
            descPanel.SetActive(false);
        }
    }
}
