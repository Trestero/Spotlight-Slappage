using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuState
{
    MainMenu,
    GameInit,
    Settings,
    Credits
}
public class MenuManager : MonoBehaviour
{
    MenuState currentState = MenuState.MainMenu;

    private GameObject startButton;
    public GameObject[] menus;

    [SerializeField]
    private GameObject[] playerPanels;

    [SerializeField]
    private Color[] playerColors;

    HashSet<int> takenControllers; // used to keep track of which controllers have already been allocated to a player
    // Use this for initialization
    void Start()
    {
        takenControllers = new HashSet<int>(); // initialize hash set
        ConfigInfo.playerCount = 0;
        startButton = GameObject.Find("StartButton");
        for(int i = 1; i < menus.Length; i++)
        {
            SetMenuVisibility(i, false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // switch to figure out what needs to be done based on the state of the menu
        switch (currentState)
        {
            case (MenuState.MainMenu):
                {
                    break;
                }
            case (MenuState.GameInit): // the "lobby" screen before a match
                {
                    CheckforInput();
                    if(ConfigInfo.playerCount > 1)
                    {
                        startButton.SetActive(true);
                    }
                    else
                    {
                        startButton.SetActive(false);
                    }
                    break;
                }
        }
        
    }

    // Called in the lobby every frame
    // Checks if any input scheme has input, and tries to register a player if it's a new controller
    private void CheckforInput()
    {
        for (int i = -1; i <= Input.GetJoystickNames().Length; i++)
        {
            if ((i < 1 && Input.GetButtonDown("Vertical" + i)) || i > 0 && Input.GetKeyDown("joystick " + i + " button 1"))
            {
                if (!takenControllers.Contains(i) && ConfigInfo.playerCount < 4)
                {
                    // register the player and add them to the list of controllers that have been registered
                    RegisterPlayer(i);
                    takenControllers.Add(i);
                }
            }
        }
    }

    // given a string identifier, load the associated scene
    public void StartGame(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    // Given an integer for the controller index, register a new player to use that controller
    public void RegisterPlayer(int controller)
    {
        if(ConfigInfo.playerCount < 4) // don't go above maximum playerCount of 4
        {
            // add the controller index to the input array
            ConfigInfo.inputIndices[ConfigInfo.playerCount] = controller;

            // reflect player joined in the menu
            GameObject go = playerPanels[ConfigInfo.playerCount];
            go.GetComponent<Image>().color = playerColors[ConfigInfo.playerCount];
            go.GetComponentInChildren<Text>().text = "P" + (ConfigInfo.playerCount + 1);

            Debug.Log(controller);
            ConfigInfo.playerCount++; // increment playerCount

        }
    }

    // set a menu's GameObject to be active or disabled
    public void SetMenuVisibility(int state, bool active)
    {
        menus[(int)state].SetActive(active);
    }


    // Switch to a given state given input
    public void SwitchState(int state)
    {
        // make whatever was showing inactive
        SetMenuVisibility((int)currentState, false);

        // set the new state to be active
        SetMenuVisibility(state, true);
        currentState = (MenuState)state;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}