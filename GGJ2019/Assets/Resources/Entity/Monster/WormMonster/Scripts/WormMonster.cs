using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMonster : StateMachine
{
    public enum WormMonsterStates { Spawn, MoveBuried, Kicked, MoveLand, Die}

    [SerializeField]
    private float _buriedMoveSpeed = 30;
    [SerializeField]
    private float _landMoveSpeed = 5;
    [SerializeField]
    private float _kickedOffset = 5;
    [SerializeField]
    private float _kickedLandTime = 2;

    private Fire _fire;
    private Vector3 _kickedLandPosition;
    private Vector3 _kickedStartPosition;
    private float _kickedStartTime;
    private Rigidbody2D rigidBody;

	void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        _fire = World.Instance.Fire;
        GetComponent<BoxCollider2D>().isTrigger = true;
        currentState = WormMonsterStates.MoveBuried;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("collisioning ");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.transform.name);
        if ((WormMonsterStates)currentState == WormMonsterStates.MoveBuried && collision.transform.CompareTag("Player"))
        {
            currentState = WormMonsterStates.Kicked;
            GetComponent<BoxCollider2D>().enabled = false;
        }
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
        if (_fire != null)
        {
            Transform target = _fire.transform;
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;
                rigidBody.MovePosition(transform.position + direction.normalized * _buriedMoveSpeed * Time.deltaTime);
            }
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
        _kickedStartPosition = transform.position;
        _kickedStartTime = Time.time;
    }

    void Kicked_Update()
    {
        transform.position = Vector3.Lerp(_kickedStartPosition, _kickedLandPosition, _kickedStartTime + _kickedLandTime / Time.time);
        if (_kickedStartTime + _kickedLandTime < Time.time)
        {
            currentState = WormMonsterStates.MoveLand;
        }
    }

    void Kicked_ExitState()
    {

    }

    void MoveLand_EnterState()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

    void MoveLand_Update()
    {
        if (_fire != null)
        {
            Transform target = _fire.transform;
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;
                rigidBody.MovePosition(transform.position + direction.normalized * _landMoveSpeed * Time.deltaTime);
            }
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
