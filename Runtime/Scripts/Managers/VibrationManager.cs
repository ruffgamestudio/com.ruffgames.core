using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VibrationManager
{ 
    public void Vibrate(VibrationType type, float duration = 1f)
    {
            
        OnVibrate(type, duration);
    }

    protected abstract void OnVibrate(VibrationType type, float duration);
}

public enum VibrationType
{
    Light,
    Medium,
    Heavy,
    Continuous
}
