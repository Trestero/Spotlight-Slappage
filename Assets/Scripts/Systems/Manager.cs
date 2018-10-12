using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // stuff regarding player spawning
    [Header("Player Spawn Info")]
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private List<Transform> spawnLocations;

    [SerializeField]
    private GameObject winPrefab;

    private UIManager uiManager;
    

    // information to use in setting up gamemodes
    [Header("Game Mode Configurations"), Space]
    [Header("Timed Mode Settings")]
    [SerializeField, Tooltip("Length of the round timer in seconds.")]
    private float timedModeTimer = 180;

    public GameMode instance; // the instance of the game to be initialized


	// Use this for initialization
	void Start ()
    {
        uiManager = GetComponent<UIManager>();
		switch(ConfigInfo.currentGameMode)
        {
            // Timed mode
            case Mode.Timed:
                {
                    instance = new TimedMode();
                    ((TimedMode)instance).Setup(ConfigInfo.playerCount, timedModeTimer);
                    break;
                }
        }

        // hook up win event to the display function
        instance.OnWin += DisplayWinner;

        SetupGame();

        // start the round instance
        instance.Start();
	}
	
    private void SetupGame()
    {
        for(int i = 0; i < instance.GetPlayers().Length; i++)
        {
            if (spawnLocations.Count > 0) // do nothing if there are no spawn locations to use
            {
                SpawnPlayer(instance.GetPlayers()[i], spawnLocations[i % spawnLocations.Count].position);
            }
        }

    }

    private void SpawnPlayer(Player playerToAssign, Vector3 location)
    {
        GameObject obj = GameObject.Instantiate(playerPrefab, location, Quaternion.identity);
        obj.GetComponent<PlayerController>().SetOwner(playerToAssign);


        uiManager.AddPlayerUI(playerToAssign);

    }

	// Update is called once per frame
	void Update ()
    {
		switch(ConfigInfo.currentGameMode)
        {
            // Timed mode
            case Mode.Timed:
                {
                    instance.Update(Time.deltaTime);
                    break;
                }
        }
	}

    void DisplayWinner()
    {
        GameObject go = GameObject.Instantiate(winPrefab, GameObject.Find("Canvas").transform);
        if(instance.GetLeader() != null)
        go.GetComponent<UnityEngine.UI.Text>().text = instance.GetLeader().Name + " Wins!";
    }
    
    // return whether the player in question has the light's focus
    bool PlayerHasLight(Player plyr)
    {
        return (instance.PlayerWithFocus == plyr);
    }
}
