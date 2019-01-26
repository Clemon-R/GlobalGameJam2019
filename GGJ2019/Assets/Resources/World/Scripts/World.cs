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

    [SerializeField]
    private List<string> _colors;

    public List<string> Colors {
        get
        {
            return _colors;
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

    //Launch the game
    public void Start()
    {
    }
}