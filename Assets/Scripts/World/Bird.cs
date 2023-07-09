using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour, InteractableObject
{
    public static Bird Instance;
    [SerializeField] private AchievementObj BirdFlu;
    public float leftmost, rightmost;
    public float speed;
    public bool empty_spot;

    private Color original_color;
    private float migration_timer = 1.5f;
    private bool is_dead = false, is_inside = false;
    private Vector2 current_dir = new Vector2(1, 0);
    SpriteRenderer sprite_renderer;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        if (empty_spot)
        {
            Color transparent = new Color(original_color.r, original_color.g, original_color.b, 0);
            sprite_renderer.color = transparent;
        }
    }

    public void OnTouch()
    {
        Die();
    }

    public void OnBoost()
    {
        Die();
    }

    private void Die()
    {
        original_color = sprite_renderer.color;
        Color transparent = new Color(original_color.r, original_color.g, original_color.b, 0);
        sprite_renderer.color = transparent;
    }

    private void Live()
    {
        sprite_renderer.color = original_color;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightmost < transform.position.x)
        {
            current_dir = Vector2.left;
            sprite_renderer.flipX = true;
            if (is_dead) Live();
        }
        else if (leftmost > transform.position.x)
        {
            current_dir = Vector2.right;
            sprite_renderer.flipX = false;
            if (is_dead) Live();
        }

        if (is_inside) migration_timer -= Time.deltaTime;
        else migration_timer = 1.5f;

        if (migration_timer < 0)
        {
            AchievementSystem.AwardAchievement(BirdFlu);
        }


        Vector3 move = new Vector3(speed * Time.deltaTime * current_dir.x, 0, 0);
        transform.Translate(move);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (empty_spot)
        {
            is_inside = true;
            Debug.Log("Inside");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (empty_spot)
        {
            is_inside = false;
            Debug.Log("Outside");
        }
    }
}
