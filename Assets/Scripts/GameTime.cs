using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Static
This class is for managing time state, pausing, slowing time, speeding up time
This class will contain the following methods and variables
*/

public static class GameTime 
{

    public static bool IsPaused()
    {

        if(Time.timeScale == 0)
        {
            return true;
        }
        return false; 
    }

    public static void Pause()
    {
        Time.timeScale = 0;
    }

    public static void Resume()
    {
        Time.timeScale = 1;
    }

    public static void DecreaseTime()
    {
        float newTime = Time.timeScale - 0.25f;
        if (newTime < 0)
        {Time.timeScale = 0;}
        Time.timeScale = newTime;
    }

    public static void IncreaseTime()
    {
        float newTime = Time.timeScale + 0.25f;
        if (newTime < 0)
        {Time.timeScale = 0;}
        Time.timeScale = newTime;
    }
}
