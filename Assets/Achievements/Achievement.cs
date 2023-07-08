using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Achievement")]
public class AchievementObj : ScriptableObject
{
    public Sprite thumbnail;
    public string title;
    public string description;
}
