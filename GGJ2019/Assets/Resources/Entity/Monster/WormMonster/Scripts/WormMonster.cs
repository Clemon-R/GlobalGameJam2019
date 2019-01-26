using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMonster : MonoBehaviour {

    public enum WormMonsterStates { Spawn, MoveBuried, Kicked, MoveLand, Die}
    [SerializeField]
    private float _buriedMoveSpeed;

    [SerializeField]
    private float _landMoveSpeed;

    private Fire _fire;

	// Use this for initialization
	void Start ()
    {
        _fire = World.Instance.Fire;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Spawn_EnterState()
    {

    }

    void Spawn_Update()
    {

    }

    void Spawn_ExitState()
    {

    }

    void MoveBuried_EnterState()
    {

    }

    void MoveBuried_Update()
    {

    }

    void MoveBuried_ExitState()
    {

    }
    void Kicked_EnterState()
    {

    }

    void Kicked_Update()
    {

    }

    void Kicked_ExitState()
    {

    }

    void MoveLand_EnterState()
    {

    }

    void MoveLand_Update()
    {

    }

    void MoveLand_ExitState()
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

    }
}
