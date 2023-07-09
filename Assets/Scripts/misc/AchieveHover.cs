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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descPanel.gameObject.SetActive(true);
        title.text = achievement.title;
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
