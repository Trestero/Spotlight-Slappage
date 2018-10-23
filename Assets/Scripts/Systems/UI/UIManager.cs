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

    [SerializeField]
    private GameObject tagPrefab;
    [SerializeField]
    private GameObject scorePanelPrefab;

    [SerializeField]
    private GameObject timerBar;

    [SerializeField]
    private GameObject scoreTrackerPrefab;

    public GameObject[] playerTrackers = new GameObject[4];

    public Color[] playerColors = new Color[4];

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
                    float timeLeft = ((TimedMode)manager.instance).TimeRemaining;
                    float timeTotal = GetComponent<Manager>().timedModeTimer;
                    timerText.text = FormatTime(timeLeft);
                    timerBar.GetComponent<Image>().fillAmount = 1 - (timeLeft / timeTotal);

                    // update player tracking positions
                    for(int i = 0; i < playerTrackers.Length; i++)
                    {
                        int score = ((TimedMode)manager.instance).GetPlayers()[i].Points;
                        playerTrackers[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(515f * (score / timeTotal), -40 - (5 * i), -(score / timeTotal));
                    }
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

    public void AddPlayerUI(Player playerToAssign)
    {
        GameObject uiTag = GameObject.Instantiate(tagPrefab, GameObject.Find("Canvas").transform);
        uiTag.GetComponent<PlayerTag>().SetTarget(playerToAssign);

        GameObject scorePanel = GameObject.Instantiate(scorePanelPrefab, GameObject.Find("Canvas").transform);
        scorePanel.GetComponent<ScoreUI>().SetTarget(playerToAssign);

        GameObject tracker = GameObject.Instantiate(scoreTrackerPrefab, GameObject.Find("TimerBar").transform);
        playerTrackers[playerToAssign.Index - 1] = tracker;
        tracker.GetComponent<Image>().color = playerColors[playerToAssign.Index - 1];
    }
}
