using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWithVelocity : MonoBehaviour
{
    [SerializeField] private float baseSpinTime = 1.5f;
    private float baseVelocity;

    [SerializeField] private float maxSpinTime = 9.0f;
    private float maxVelocity;
    
    [SerializeField] private Interp.Type spinScalingMethod = Interp.Type.Linear;

    private float curTime = 0.0f;
    private int curIdx = 0;
    
    private Rigidbody2D _rigidbody2D;

    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private List<Sprite> _sprites = new();
    
    private void Awake()
    {
        var parent = transform.parent;
        _rigidbody2D = parent.GetComponent<Rigidbody2D>();
        var bullet = parent.GetComponent<PlayerBullet>();
        baseVelocity = bullet.StartVelocity;
        maxVelocity = bullet.MaxVelocity;
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        curTime += Time.deltaTime;
        
        var targetSpinSpeed = Interp.Erp(spinScalingMethod, baseSpinTime, maxSpinTime, (_rigidbody2D.velocity.magnitude - baseVelocity) / (maxVelocity - baseVelocity));

        if (curTime >= 1.0f / targetSpinSpeed)
        {
            // advance frame
            curTime -= 1.0f / targetSpinSpeed;
            curIdx = (curIdx + 1) % _sprites.Count;
            _spriteRenderer.sprite = _sprites[curIdx];
        }
    }
}
