using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    //Static const
    public static List<string> COLORS; 

    public List<string> colors = new List<string>();

    //Set const
    public World()
    {
        COLORS = colors;
    }

    //Launch the game
    public void Start()
    {
    }
}
