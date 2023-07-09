using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitty : MonoBehaviour
{
    [SerializeField] private Sprite fall;

    [SerializeField] private Sprite land;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Fall()
    {
        _rigidbody2D.simulated = true;
        spriteRenderer.sprite = fall;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (Multitag.CompareTag(col.gameObject, "Ground"))
        {
            spriteRenderer.sprite = land;
            _rigidbody2D.simulated = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
