using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;


public class WaveEventArgs : EventArgs
{
    public bool ReverseStatus;
    public int WaveNumber;
    public bool GameStatus;
    public Player player;
    public GameObject Target;
}
public static class WaveEvent 
{
    public static event EventHandler<WaveEventArgs> WaveStart;
    public static event EventHandler<WaveEventArgs> WaveEnd;
    public static event EventHandler<WaveEventArgs> ReverseModes;
    public static event EventHandler<WaveEventArgs> GameEnd;
    public static event EventHandler<WaveEventArgs> ChangePlayerObjective;

    public static void InvokeWaveStart(int WaveIndex)
    {
        WaveStart(null, new WaveEventArgs { WaveNumber = WaveIndex });
    }
    public static void InvokeWaveEnd(int WaveIndex)
    {
        WaveEnd(null, new WaveEventArgs { WaveNumber = WaveIndex });
    }
    public static void InvokeReversal(bool value)
    {
        ReverseModes(null, new WaveEventArgs { ReverseStatus = value });
    }
    public static void InvokeGameEnd(Player player, bool value)
    {
        GameEnd(null, new WaveEventArgs { GameStatus = value, player = player });
    }

    public static void InvokeChangePlayerObjective(Player player, GameObject Objective)
    {
        GameEnd(null, new WaveEventArgs { player = player, Target = Objective });
    }
}
