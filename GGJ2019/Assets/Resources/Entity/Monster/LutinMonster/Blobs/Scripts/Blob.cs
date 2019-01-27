using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Monster
{

    public enum BlobStates { Spawn, Move, FireJumpPause, FireJump, Die}

    [SerializeField]
    private float _spawnDuration = 0.5f;
    [SerializeField]
    private int _fireDamage = 10;
    [SerializeField]
    private float _moveSpeed = 6;
    [SerializeField]
    private float _jumpDistance = 5;
    [SerializeField]
    private float _pauseBeforeJump = 0.5f;
    [SerializeField]
    private float _jumpSpeed = 16;

    private Rigidbody2D rigidBody;
    private Fire _fire;
    private float _pauseStartTime;
    private float _spawnStartTime;

    protected override void Start ()
    {
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
        _color = ColorUtil.Colors.PURPLE;
        _fire = World.Instance.Fire;
        currentState = BlobStates.Spawn;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.TakeHit();
        }
        if (collision.transform.CompareTag("Fire"))
        {
            Fire fire = collision.GetComponent<Fire>();
            fire.TakeHit(_fireDamage);
            currentState = BlobStates.Die;
        }
    }

    void Spawn_EnterState()
    {
        _spawnStartTime = Time.time;
    }

    void Spawn_Update()
    {
        if (_spawnStartTime + _spawnDuration < Time.time)
        {
            currentState = BlobStates.Move;
        }
    }

    void Spawn_ExitState()
    {

    }

    void Move_EnterState()
    {
        GetComponent<Animator>().SetBool("Move", true);
    }

    void Move_Update()
    {
        if (_fire != null)
        {
            if (Vector3.Distance(transform.position, _fire.transform.position) < _jumpDistance)
            {
                currentState = BlobStates.FireJumpPause;
                return;
            }
            Transform target = _fire.transform;
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;
                rigidBody.MovePosition(transform.position + direction.normalized * _moveSpeed * Time.deltaTime);
            }
        }
    }

    void Move_ExitState()
    {
        GetComponent<Animator>().SetBool("Move", false);
    }

    void FireJumpPause_EnterState()
    {
        _pauseStartTime = Time.time;
    }

    void FireJumpPause_Update()
    {
        if (_pauseBeforeJump + _pauseStartTime < Time.time)
        {
            currentState = BlobStates.FireJump;
        }
    }

    void FireJump_EnterState()
    {
        GetComponent<Animator>().SetBool("Jump", true);
    }

    void FireJump_Update()
    {
        if (_fire != null)
        {
            Transform target = _fire.transform;
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;
                rigidBody.MovePosition(transform.position + direction.normalized * _jumpSpeed * Time.deltaTime);
            }
        }
    }

    void FireJump_ExitState()
    {

    }

    void Die_EnterState()
    {
        Die();
    }

    void Die_Update()
    {

    }
}
