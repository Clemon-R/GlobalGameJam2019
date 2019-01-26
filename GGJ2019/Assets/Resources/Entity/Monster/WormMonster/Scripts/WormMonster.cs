using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMonster : MonoBehaviour {

    public enum WormMonsterStates { Spawn, MoveBuried, Kicked, MoveLand, Die}
    [SerializeField]
    private float _buriedMoveSpeed = 30;

    [SerializeField]
    private float _landMoveSpeed = 5;

    [SerializeField]
    private float _kickedOffset;

    private Fire _fire;

    private Vector3 _kickedLandPosition;

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
        Transform target = _fire.transform;
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            transform.position += direction.normalized * _buriedMoveSpeed * Time.deltaTime;
        }
    }

    void MoveBuried_ExitState()
    {

    }
    void Kicked_EnterState()
    {
        Vector3 direction = _fire.transform.position - transform.position;
        Vector2 perpendicularVector = Vector2.Perpendicular(direction).normalized;

        int random = Random.Range(0, 2);
        _kickedLandPosition = new Vector3(perpendicularVector.x, perpendicularVector.y, 0) * (_kickedOffset * (random == 0 ? -1 : 1));
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
        Transform target = _fire.transform;
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            transform.position += direction.normalized * _landMoveSpeed * Time.deltaTime;
        }
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
        Destroy(gameObject);
    }
}
