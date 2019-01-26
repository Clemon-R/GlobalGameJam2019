using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private static World _instance;
    private static object _lock = new object();

    //Static const
    [SerializeField]
    private Player[] _players;

    public Player[] Players
    {
        get { return _players; }
    }

    private string[][] _colors;

    public string[][] Colors {
        get
        {
            return _colors;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            _players = FindObjectsOfType<Player>();
        }
    }

    public static World Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    var holder = new GameObject();
                    _instance = holder.AddComponent<World>();
                    _instance._players = FindObjectsOfType<Player>();
                    holder.name = "World Object";

                }
                return _instance;
            }
        }
    }

    public World()
    {
        if (_colors == null || _colors.Length < 3)
        {
            _colors = new string[4][];
            _colors[0] = new string[3];
            _colors[0][0] = "#ffffff";
            _colors[0][1] = "#ffffff"; //Nuance 1
            _colors[0][2] = "#ffffff"; //Nuance 2
            _colors[1] = new string[3];
            _colors[1][0] = "#b40001";
            _colors[1][1] = "#ff091e";
            _colors[1][2] = "#ff566b";
            _colors[2] = new string[3];
            _colors[2][0] = "#0081ff";
            _colors[2][1] = "#00d2ff";
            _colors[2][2] = "#00f9ff";
            _colors[3] = new string[3];
            _colors[3][0] = "#67153b";
            _colors[3][1] = "#b7276d";
            _colors[3][2] = "#f2495a";
        }
    }
}