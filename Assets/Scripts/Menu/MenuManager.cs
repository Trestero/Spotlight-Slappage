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

    [Header("Costume Data")]
    [SerializeField]
    private Sprite[] costumeSprites;
    [SerializeField]
    private string[] costumeNames;

    private Costume[] costumes;

    [SerializeField]
    private GameObject[] SpriteDisplays;

    HashSet<int> takenControllers; // used to keep track of which controllers have already been allocated to a player
    bool[] readInput = { false, false, false, false }; // whether a given controller received input and should wait for a reset

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

        costumes = new Costume[costumeSprites.Length];
        // initialize costume stuff
        for(int i = 0; i< costumeSprites.Length; i++)
        {
            costumes[i] = new Costume(costumeNames[i], costumeSprites[i], i);
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
            case (MenuState.Settings):
                {
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


        for(int x = 0; x < ConfigInfo.playerCount; x++)
        {
            int axisNum = ConfigInfo.inputIndices[x]; // get the input axis to check
            float input = Input.GetAxis("Horizontal" + axisNum);
            if(input == 0 ) // if no input, check the next joystick
            {
                readInput[x] = false;
                continue;
            }

            if (!readInput[x]) // only execute this once per input set, require that the axis resets to 0 between changes
            {
                int currentChar = ConfigInfo.characters[x];
                if (input > 0)
                {
                    // increment and wrap costume
                    currentChar++;
                    currentChar = currentChar % costumes.Length;
                }
                else if (input < 0)
                {
                    // decrement and wrap costume
                    currentChar--;
                    if (currentChar < 0)
                    {
                        currentChar = costumes.Length - 1;
                    }
                }
                ConfigInfo.characters[x] = currentChar;
                SpriteDisplays[x].GetComponent<Image>().sprite = costumes[currentChar].Outfit;
                readInput[x] = true;
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


            SpriteDisplays[ConfigInfo.playerCount].GetComponent<Image>().color = playerColors[ConfigInfo.playerCount];
            SpriteDisplays[ConfigInfo.playerCount].GetComponent<Image>().sprite = costumes[0].Outfit;
            ConfigInfo.characters[ConfigInfo.playerCount] = 0;

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