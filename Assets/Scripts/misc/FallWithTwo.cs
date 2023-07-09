using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWithTwo : MonoBehaviour
{
    [SerializeField] private SpriteRenderer first;

    [SerializeField] private SpriteRenderer second;

    private bool fell = false;

    // Update is called once per frame
    private void Update()
    {
        if (!fell && !first.enabled && !second.enabled)
        {
            GetComponent<Rigidbody2D>().simulated = true;
        }
    }
}
