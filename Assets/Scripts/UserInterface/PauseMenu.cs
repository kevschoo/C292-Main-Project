using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject TheMenu;
    [SerializeField] bool IsOpen = true;
    [SerializeField] float LastTimeScale;
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
    private void Update()
    {
        if(IsOpen == true && GameTime.IsPaused() == false)
        {
            GameTime.Pause();
        }
    }

    void EnableMenus(object sender, TimeEventArgs args)
    {
        LastTimeScale = args.floatValue;
        TheMenu.SetActive(true);
        IsOpen = true;
    }

    void DisableMenus(object sender, TimeEventArgs args)
    {
        LastTimeScale = args.floatValue;
        TheMenu.SetActive(false);
        IsOpen = false;
    }

    public void RestartLevel()
    {
        Debug.Log("RestartClicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        Debug.Log("MainMenu");
        int sceneToLoad = 0; //Menu Scene ID
        SceneManager.LoadScene(sceneToLoad);
    }
    public void Cancel()
    {
        Debug.Log("Canceled");
        IsOpen = false;
        TimeEffectEvent.InvokeEventEnd(1);
        GameTime.Resume();
    }
    public void QuitGame()
    {
        Debug.Log("Quitters never win");
        Application.Quit();

    }
}
