using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliMonster : StateMachine
{
    public enum EliMonsterStates { Spawn, Move }

	void Start ()
    {
        currentState = EliMonsterStates.Spawn;
	}
	
	void Spawn_EnterState()
    {
        currentState = EliMonsterStates.Move;
    }

    void Spawn_Update()
    {

    }

    void Spawn_ExitState()
    {

    }

    void Move_EnterState()
    {

    }

    void Move_Update()
    {

    }

    void Move_ExitState()
    {

    }
}
