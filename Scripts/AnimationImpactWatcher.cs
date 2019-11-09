using System;
using UnityEngine;

public class AnimationImpactWatcher : MonoBehaviour
{
    public event Action OnImpact; //adding objects to action events?
        //Would it be best to only use this action when impacting with collider instead of per frame because shapes/sizes vary?

    /// <summary>
    /// Called by animation
    /// </summary>

    private void Impact() //I assume the name of this function has to match what was put in the editor?
    {
        if (OnImpact != null)
            OnImpact();
    }
}
