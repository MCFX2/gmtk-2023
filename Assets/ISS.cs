using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISS : MonoBehaviour, InteractableObject
{

    public static ISS Instance;
    [SerializeField] private AchievementObj ImpostorAmongusAchievement;
    public float torque_value;
    public static bool achieved { get; private set; } = false;
    public float y_threshold;
    public Sprite death_sprite;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    BoxCollider2D box_collider;

    private void Awake()
    {
        Instance = this;
    }

    public void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        box_collider = GetComponent<BoxCollider2D>();

        rb.AddTorque(torque_value);
        anim.speed = 0;
    }

    public void Update() {
        {
            if (rb.velocity.y < 0) anim.speed = 1;

            if (transform.position.y < y_threshold && !achieved)
            {
                achieved = true;
                AchievementSystem.AwardAchievement(ImpostorAmongusAchievement);
                rb.gravityScale = 1.0f;
            }
        }
    }

    /// Use to determine when to show crash sprite.
    void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D otherRigidbody = other.gameObject.GetComponent<Rigidbody2D>();

        // Check if the other collider has a Rigidbody2D component
        if (otherRigidbody != null)
        {
            // Check if the other Rigidbody2D is static
            if (otherRigidbody.bodyType == RigidbodyType2D.Static)
            {
                // Collision occurred with a static Rigidbody2D
                Debug.Log("The ISS was not the Impostor.");
                transform.rotation = Quaternion.identity;
                anim.enabled = false;
                sr.sprite = death_sprite;
                box_collider.size = new Vector2(1.2f, 0.02f);
                box_collider.offset = new Vector2(0, -0.63f);
            }
        }

    }
}
