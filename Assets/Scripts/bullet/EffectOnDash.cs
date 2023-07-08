using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnDash : MonoBehaviour
{

    [SerializeField] private List<Sprite> _sprites = new();

    [SerializeField] private float framerate = 10.0f;
    [SerializeField] private float playTime = 1.0f;
    
    private SpriteRenderer _spriteRenderer;

    private float curTime;
    private float totalTime = 0;
    private int curIdx = 0;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;

            curTime += Time.deltaTime;
            if (curTime >= 1.0f / framerate)
            {
                // advance frame
                curTime -= 1.0f / framerate;
                curIdx = (curIdx + 1) % _sprites.Count;
                _spriteRenderer.sprite = _sprites[curIdx];
            }
            
            if (totalTime < 0)
            {
                _spriteRenderer.enabled = false;
            }
        }
    }

    public void Fire()
    {
        _spriteRenderer.enabled = true;
        totalTime = playTime;
    }
}
