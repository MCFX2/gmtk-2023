using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [SerializeField] private float onTime = 0.5f;
    [SerializeField] private float offTime = 1.0f;

    private float curTime;
    
    private TextMeshProUGUI _image;
    
    private void Awake()
    {
        _image = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        curTime += Time.deltaTime;
        if (_image.enabled)
        {
            if (curTime >= onTime)
            {
                curTime -= onTime;
                _image.enabled = false;
            }
        }
        else
        {
            if (curTime >= offTime)
            {
                curTime -= offTime;
                _image.enabled = true;
            }
        }
    }
}
