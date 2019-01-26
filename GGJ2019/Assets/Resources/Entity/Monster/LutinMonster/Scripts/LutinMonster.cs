using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LutinMonster : Monster
{
    public enum LutinMonsterStates { Spawn, Move, SpawnBlobs}
    private Player[] _players;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        _players = World.Instance.Players;
	}
	
    void Spawn_EnterState()
    {

    }

    void Spawn_Update()
    {

    }

    void Move_EnterState()
    {

    }

    void SpawnBlobs_EnterState()
    {

    }

    void SpawnBlobs_Update()
    {

    }

}
