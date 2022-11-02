using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject TheMenu;

    float LastTimeScale;
    //Having time effect class give args for future development
    void Start()
    {
        TimeEffectEvent.TimeEventStarted += EnableMenus;
        TimeEffectEvent.TimeEventEnded += DisableMenus;

    }
    private void OnDestroy()
    {
        TimeEffectEvent.TimeEventStarted -= EnableMenus;
        TimeEffectEvent.TimeEventEnded -= DisableMenus;
    }
    void EnableMenus(object sender, TimeEventArgs args)
    {
        LastTimeScale = args.floatValue;
        TheMenu.SetActive(true);

    }

    void DisableMenus(object sender, TimeEventArgs args)
    {
        LastTimeScale = args.floatValue;
        TheMenu.SetActive(false);

    }

    public void RestartLevel()
    {
        Debug.Log("RestartClicked");

    }
    public void MainMenu()
    {
        Debug.Log("MainMenu");

    }
    public void Cancel()
    {
        Debug.Log("Canceled");
        TimeEffectEvent.InvokeEventEnd(LastTimeScale);
    }
    public void QuitGame()
    {
        Debug.Log("Quitters never win");

    }
}
