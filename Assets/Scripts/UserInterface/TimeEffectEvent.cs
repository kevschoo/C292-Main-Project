using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeEventArgs : EventArgs
{
    public bool boolValue;
    public float floatValue;
}
public static class TimeEffectEvent 
{
    public static event EventHandler<TimeEventArgs> TimeEventStarted;
    public static event EventHandler<TimeEventArgs> TimeEventEnded;

    public static void InvokeEventStart(bool newBool, float newFloat)
    {
        TimeEventStarted(null, new TimeEventArgs { boolValue = newBool, floatValue = newFloat });

    }

    public static void InvokeEventStart(bool newBool)
    {
        TimeEventStarted(null, new TimeEventArgs { boolValue = newBool });

    }
    public static void InvokeEventStart(float newFloat)
    {
        TimeEventStarted(null, new TimeEventArgs { floatValue = newFloat });

    }
    public static void InvokeEventEnd(bool newBool, float newFloat)
    {
        TimeEventEnded(null, new TimeEventArgs { boolValue = newBool, floatValue = newFloat });

    }

    public static void InvokeEventEnd(bool newBool)
    {
        TimeEventEnded(null, new TimeEventArgs { boolValue = newBool });

    }
    public static void InvokeEventEnd(float newFloat)
    {
        TimeEventEnded(null, new TimeEventArgs { floatValue = newFloat });

    }
}
