using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractableObject
{
    /// <summary>
    /// Called when the bullet touches the object (not dashing)
    /// </summary>
    public virtual void OnTouch()
    {
        
    }

    /// <summary>
    /// Called when the bullet dashes through the object
    /// </summary>
    public virtual void OnBoost()
    {
        
    }
}
