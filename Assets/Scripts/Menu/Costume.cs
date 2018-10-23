using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Costume
{
    private string name;
    private Sprite outfit;
    private int number;
    public Costume(string nm, Sprite img, int num)
    {
        name = nm;
        outfit = img;
        number = num;
    }

    public string Name
    {
        get { return name; }
    }

    public Sprite Outfit
    {
        get { return outfit; }
    }
    public int Number
    {
        get { return number; }
    }
}
