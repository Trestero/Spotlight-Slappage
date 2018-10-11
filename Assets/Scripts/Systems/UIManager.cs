using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] // canvas object
    private GameObject uiCanvas;

    [SerializeField] // the game timer to be updated
    private GameObject timer;
    private Text timerText;

    // panels for player score tracking
    [SerializeField]
    private GameObject[] playerPanels;
    private Text[] scoreText;

    private Manager manager; // the game manager script attached to the same object

    public void Start() // sets up component references
    {
        manager = GetComponent<Manager>();
        //scoreText = new Text[playerPanels.Length];
        //for(int i = 0; i < playerPanels.Length; i++)
        //{
        //    scoreText[i] = playerPanels[i].GetComponentInChildren<Text>();
        //}

        timerText = timer.GetComponent<Text>();


    }

    // Updates the UI to reflect gamestate
    public void Update()
    {
        switch(ConfigInfo.currentGameMode)
        {
            case (Mode.Timed):
                {
                    timerText.text = FormatTime(((TimedMode)manager.instance).TimeRemaining);
                    break;
                }
        }
    }

    // Formats the time in minutes:seconds left
    private string FormatTime(float secondsLeft)
    {
        int secsLeft = Mathf.CeilToInt(secondsLeft);
        int minutes = secsLeft / 60;
        int seconds = secsLeft % 60;

        string result = minutes + ":";
        if(seconds < 10)
        {
            result += "0" + seconds;
        }
        else
        {
            result += seconds;
        }

        return result;
    }
}
