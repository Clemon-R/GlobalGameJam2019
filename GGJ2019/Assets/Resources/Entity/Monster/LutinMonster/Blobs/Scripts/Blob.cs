using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : StateMachine
{

    public enum BlobStates { Spawn, Move, Die}

    [SerializeField]
    private float _moveSpeed;

	void Start ()
    {
        currentState = BlobStates.Spawn;
	}
	
	void Spawn_EnterState()
    {

    }

    void Move_EnterState()
    {

    }

    void Move_Update()
    {

    }

    void Die_EnterState()
    {

    }

    void Die_Update()
    {

    }
}
