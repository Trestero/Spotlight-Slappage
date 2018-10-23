using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// A static class used to store game configuration information
// We should be able to use this to store config data between scenes, ex. when the player goes from menu to actual gameplay


public enum Mode { Timed };
public static class ConfigInfo
{
    // how many players to set up the game with
    public static int playerCount = 4;

    // which mode to start gameplay in
    public static Mode currentGameMode = Mode.Timed;

    public static int[] inputIndices = { -1, 0, 1, 2 };
    public static int[] characters = { 0, 1, 2, 1};

    public static Color GetColor(int i)
    {
        UIManager uiManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<UIManager>();
        return uiManager.playerColors[i];
    }
}
