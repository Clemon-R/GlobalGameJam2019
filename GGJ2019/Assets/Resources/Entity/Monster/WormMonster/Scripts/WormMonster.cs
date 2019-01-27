using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMonster : Monster
{
    public enum WormMonsterStates { Spawn, MoveBuried, Kicked, MoveLand, Die}

    [SerializeField]
    private int _buriedFireDamage = 10;
    [SerializeField]
    private int _landFireDamage = 20;

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

	protected override void Start ()
    {
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
        _fire = World.Instance.Fire;
        currentState = WormMonsterStates.MoveBuried;
        GetComponent<Animator>().SetBool(_color.ToString(), true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((WormMonsterStates)currentState == WormMonsterStates.MoveBuried)
        {
            if (collision.transform.CompareTag("Player"))
            {
                currentState = WormMonsterStates.Kicked;
                GetComponent<BoxCollider2D>().enabled = false;
            }
            if (collision.transform.CompareTag("Fire"))
            {
                Fire fire = collision.GetComponent<Fire>();
                if (fire != null)
                {
                    fire.TakeHit(_buriedFireDamage);
                }
                currentState = WormMonsterStates.Die;
            }
        }
        else if ((WormMonsterStates)currentState == WormMonsterStates.MoveLand)
        {
            if (collision.transform.CompareTag("Player"))
            {
                Player player = collision.GetComponent<Player>();

                player.TakeHit();
                currentState = WormMonsterStates.Die;
            }
            if (collision.transform.CompareTag("Fire"))
            {
                Fire fire = collision.GetComponent<Fire>();
                fire.TakeHit(_landFireDamage);
                currentState = WormMonsterStates.Die;
            }
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
        // Play buried animation
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
        // Play kicked animation
        Vector3 direction = _fire.transform.position - transform.position;
        Vector2 perpendicularVector = Vector2.Perpendicular(direction).normalized;

        int random = Random.Range(0, 2);
        _kickedLandPosition = new Vector3(perpendicularVector.x, perpendicularVector.y, 0) * (_kickedOffset * (random == 0 ? -1 : 1));
        _kickedStartPosition = transform.position;
        _kickedStartTime = Time.time;
    }

    void Kicked_Update()
    {
        transform.position = Vector3.Lerp(_kickedStartPosition, _kickedLandPosition, Time.time / (_kickedStartTime + _kickedLandTime));
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
        // Play move on land animation
        GetComponent<BoxCollider2D>().enabled = true;
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
        Die();
    }

    void Die_Update()
    {

    }

    void Die_ExitState()
    {
    }
}
