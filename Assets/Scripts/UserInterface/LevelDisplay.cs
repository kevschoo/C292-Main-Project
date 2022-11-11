using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] LevelHandler levelHandler;
    [SerializeField] Player MainPlayer;

    //Texts
    [SerializeField] TextMeshProUGUI MoneyText;
    [SerializeField] TextMeshProUGUI WaveText;
    [SerializeField] TextMeshProUGUI SurvialTimeLeftText;
    [SerializeField] TextMeshProUGUI TimeScaleText;
    [SerializeField] TextMeshProUGUI GameStatusText;

    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject VictoryScreen;

    float LevelTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameOverScreen.gameObject.SetActive(false);
        VictoryScreen.gameObject.SetActive(false);
        GameStatusText.gameObject.SetActive(false);
        if (levelHandler == null)
        {
            levelHandler = LevelHandler.Level;
            if(levelHandler.IsWaveLevel)
            {
                SurvialTimeLeftText.gameObject.SetActive(false);
            }
            else if(!levelHandler.IsWaveLevel)
            {
                WaveText.gameObject.SetActive(false);
            }
        }
           
        if(MainPlayer == null)
        {
            MainPlayer = GameObject.FindObjectOfType<RealPlayer>();
        }
        WaveEvent.GameEnd += OnGameEnd;
        WaveEvent.ReverseModes += ReverseMode ;

    }
    void OnGameEnd(object sender, WaveEventArgs args)
    {
        if(GameStatusText.text == null)
        { return;}
        if (args.GameStatus == false && args.player == MainPlayer)
        {
            GameStatusText.gameObject.SetActive(true);
            GameStatusText.text = "Game Over";
            GameOverScreen.gameObject.SetActive(true);
        }
        else if(args.GameStatus == true && args.player == MainPlayer)
        {
            GameStatusText.gameObject.SetActive(true);
            GameStatusText.text = "Level Complete";
            VictoryScreen.gameObject.SetActive(true);
        }
        else if (args.GameStatus == false && args.player != MainPlayer)
        {
            GameStatusText.gameObject.SetActive(true);
            GameStatusText.text = "Level Complete";
            VictoryScreen.gameObject.SetActive(true);
        }
        else
        {
            GameStatusText.gameObject.SetActive(true);
            GameStatusText.text = "Game Over";
            GameOverScreen.gameObject.SetActive(true);
        }
    }
    void ReverseMode(object sender, WaveEventArgs args)
    {
        if (args.ReverseStatus == true)
        {
            GameStatusText.gameObject.SetActive(true);
            GameStatusText.text = "Mode Reversal";
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(MainPlayer != null)
        {
            MoneyText.text = "$"+MainPlayer.Money;
        }
        if (levelHandler != null)
        {
            if (levelHandler.IsWaveLevel)
            {
                WaveText.text = levelHandler.CurrentWaveNumber + "/" + levelHandler.LastWaveNumber;
            }
            else
            {
                SurvialTimeLeftText.text = "Time:" + (LevelTime + Time.deltaTime) + "/" + levelHandler.SurvivalTimeLength;
            }
                
        }
        TimeScaleText.text = Time.timeScale + "";
    }
}
