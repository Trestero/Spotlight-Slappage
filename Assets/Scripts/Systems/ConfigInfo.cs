﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// A static class used to store game configuration information
// We should be able to use this to store config data between scenes, ex. when the player goes from menu to actual gameplay


public enum Mode { Timed };
public static class ConfigInfo
{
    // how many players to set up the game with
    public static int playerCount = 2;

    // which mode to start gameplay in
    public static Mode currentGameMode = Mode.Timed;

    public static int[] inputIndices = { -1, 0, 1, 2 };

}
