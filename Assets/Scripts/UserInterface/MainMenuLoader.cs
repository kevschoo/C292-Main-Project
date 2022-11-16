using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour
{

    [SerializeField] TMP_Dropdown Dropdownlist;
    [SerializeField] List<int> Scenes = new List<int>{0, 1, 2, 3, 4, 5, 6, 7, 8, 9}; 


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Main Menu Loaded");
    }



    public void StartGame()
    {
        Debug.Log("Loading First Level");
        int sceneToLoad = 3;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

    public void PlaySelectedLevel(TMP_Dropdown dropdown)
    {
        Debug.Log("Playing Level " + dropdown.value);
        int SelectedLevel = dropdown.value + 3;
        SceneManager.LoadScene(SelectedLevel);
    }

}
