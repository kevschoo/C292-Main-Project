using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoneyEventArgs : EventArgs
{
    public int MoneyChangeAmount;
    public Player WhomstveChanged;
}
public static class MoneyEvent 
{


    public static event EventHandler<MoneyEventArgs> MoneyChanged;


    public static void InvokeEventStart(int changeValue, Player player)
    {
        MoneyChanged(null, new MoneyEventArgs { MoneyChangeAmount = changeValue, WhomstveChanged = player });

    }
}
