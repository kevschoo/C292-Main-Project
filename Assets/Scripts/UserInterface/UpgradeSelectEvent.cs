using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class UpgradeEventArgs : EventArgs
{
    public int SelectedIndex;
}
public static class UpgradeSelectEvent 
{
    public static event EventHandler<UpgradeEventArgs> UpgradeChanged;
    public static event EventHandler UpgradeCleared;

    public static void InvokeSelectionChanged(int Index)
    {
        UpgradeChanged(null, new UpgradeEventArgs { SelectedIndex = Index });
    }

    public static void InvokeSelectionCleared()
    {
        UpgradeCleared(null, EventArgs.Empty);

    }
}
public class StoreEventArgs : EventArgs
{
    public int SelectedIndex;
}
public static class StoreSelectEvent
{
    public static event EventHandler<UpgradeEventArgs> PurchaseChanged;
    public static event EventHandler PurchaseCleared;

    public static void InvokeSelectionChanged(int Index)
    {
        PurchaseChanged(null, new UpgradeEventArgs { SelectedIndex = Index });
    }

    public static void InvokeSelectionCleared()
    {
        PurchaseCleared(null, EventArgs.Empty);

    }
}
