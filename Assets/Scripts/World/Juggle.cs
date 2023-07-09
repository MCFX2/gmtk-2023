using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggle : MonoBehaviour, InteractableObject
{
    public int bounces_for_win = 3;

    public static Juggle Instance;
    public GameObject player;
    public float time_between_hits = 0.3f;
    public Sprite clown_sad;
    public Sprite clown_awe;
    public GameObject clown;
    private SpriteRenderer sprite_renderer;
    
    [SerializeField] private AchievementObj juggling;


    public static bool completed { get; private set; } = false;

    public void Start()
    {
        sprite_renderer = clown.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        time_between_hits -= Time.deltaTime;
        if (time_between_hits <= 0) 
        {
            time_between_hits = 0;
            sprite_renderer.sprite = clown_sad;
        }
        else sprite_renderer.sprite = clown_awe;

        if (clown.transform.position.x > transform.position.x)
        {
            sprite_renderer.flipX = false;
        }
        else
        {
            sprite_renderer.flipX = true;
        }

        if (completed) sprite_renderer.sprite = clown_awe;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject == player && time_between_hits <= 0)
        {
            bounces_for_win -= 1;

            if (bounces_for_win == 0)
            {
                AchievementSystem.AwardAchievement(juggling);
            }
            Debug.Log("Player hit!");
        }
        else
        {
            Debug.Log("Other hit!");
            bounces_for_win = 3;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.gameObject != player)
        {
            bounces_for_win = 3;
        }
    }
}
