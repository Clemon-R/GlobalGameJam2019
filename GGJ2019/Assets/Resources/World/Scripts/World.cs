using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private static World _instance;
    private static object _lock = new object();

    [SerializeField]
    private Fire _fire;

    public Fire Fire
    {
        get { return _fire; }
    }

    [SerializeField]
    private FreezeScreen _screenFreeze;

    public FreezeScreen ScreenFreeze
    {
        get { return _screenFreeze; }
    }

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

    public Rect Boundary { get; set; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            InitGame();
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
                    holder.name = "World Object";
                    InitGame();
                }
                return _instance;
            }
        }
    }

    private static void InitGame()
    {
        _instance._players = FindObjectsOfType<Player>();
        _instance._fire = FindObjectOfType<Fire>();
        _instance._screenFreeze = FindObjectOfType<FreezeScreen>();
        if (_instance._fire == null)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("Entity/Fire/Prefabs/Fire"), Vector3.zero, Quaternion.identity);
            _instance._fire = go.GetComponent<Fire>();
        }
        if (_instance._screenFreeze == null)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("World/Prefabs/ScreenFreezer"), Vector3.zero, Quaternion.identity);
            _instance._screenFreeze = go.GetComponent<FreezeScreen>();
        }
        if (_instance._players == null)
        {
            _instance._players = new Player[4];
            for (int i = 0; i < 4; i++)
            {
                GameObject go = (GameObject)Instantiate(Resources.Load("Entity/Player/Prefabs/Player"), Vector3.zero, Quaternion.identity);
                _instance._players[i] = go.GetComponent<Player>();
            }
        }
        else if (_instance._players.Length != 4)
        {
            Player[] players = new Player[4];
            for (int i = 0; i < 4; i++)
            {
                if (_instance._players.Length > i)
                {
                    players[i] = _instance._players[i];
                }
                else
                {
                    GameObject go = (GameObject)Instantiate(Resources.Load("Entity/Player/Prefabs/Player"), Vector3.zero, Quaternion.identity);
                    players[i] = go.GetComponent<Player>();
                }
            }
            _instance._players = players;
        }
        float distFromFire = 4;
        Vector3[] spawnPositions = new Vector3[]
        {
            new Vector3(_instance._fire.transform.position.x - distFromFire, _instance._fire.transform.position.y + distFromFire, 0),
            new Vector3(_instance._fire.transform.position.x + distFromFire, _instance._fire.transform.position.y + distFromFire, 0),
            new Vector3(_instance._fire.transform.position.x - distFromFire, _instance._fire.transform.position.y - distFromFire, 0),
            new Vector3(_instance._fire.transform.position.x + distFromFire, _instance._fire.transform.position.y - distFromFire, 0),
        };
        for (int i = 0; i < _instance._players.Length; i++)
        {
            _instance._players[i].SetPlayerNumber(i + 1);
            _instance._players[i].SetBaseColor(i == 1 || i == 3 ? ColorUtil.Colors.BLUE : ColorUtil.Colors.RED);
            _instance._players[i].transform.position = spawnPositions[i];
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
            _colors[3][0] = "#67153B";
            _colors[3][1] = "#b7276d";
            _colors[3][2] = "#f2495a";
        }
    }
}