﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliMonster : StateMachine
{
    public enum EliMonsterStates { Spawn, Move, Die }
    private Player[] _players;
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float _moveSpeed;

	void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        _players = World.Instance.Players;
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
        Transform target = GetTarget();
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            rigidBody.MovePosition(transform.position + direction.normalized * _moveSpeed * Time.deltaTime);
        }
    }

    void Move_ExitState()
    {

    }

    void Die_EnterState()
    {

    }

    void Die_Update()
    {

    }

    void Die_ExitState()
    {
        Destroy(gameObject);
    }

    Transform GetTarget()
    {
        Transform closest = null;
        float closestDistance = 0;

        if (_players != null)
        {
            for (int i = 0; i < _players.Length; i++)
            {
                // if color == _players[i].getcolor
                if (closest == null)
                {
                    closest = _players[i].transform;
                    closestDistance = Vector3.Distance(transform.position, _players[i].transform.position);
                }
                else
                {
                    float currentDistance = Vector3.Distance(transform.position, _players[i].transform.position);
                    if (currentDistance < closestDistance)
                    {
                        closest = _players[i].transform;
                        closestDistance = currentDistance;
                    }
                }
            }
        }
        return closest;
    }
}
