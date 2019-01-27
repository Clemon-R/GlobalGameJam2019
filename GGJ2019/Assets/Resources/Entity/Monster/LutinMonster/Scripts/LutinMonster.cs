using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LutinMonster : Monster
{
    public enum LutinMonsterStates { Spawn, Move, SpawnBlobs, Die}
    private Player[] _players;

    [SerializeField]
    private float _moveSpeed = 4;
    [SerializeField]
    private float _blobSpawnRate = 2;

    private Rigidbody2D rigidBody;
    private Vector3 _target = Vector3.zero;

    [SerializeField]
    private GameObject _blobPrefab;

    private float _lastBlobSpawn;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        _color = Random.Range((int)0, (int)2) == 0 ? ColorUtil.Colors.RED : ColorUtil.Colors.BLUE;
        rigidBody = GetComponent<Rigidbody2D>();
        _players = World.Instance.Players;
        currentState = LutinMonsterStates.Spawn;
        GetComponent<Animator>().SetBool(_color.ToString(), true);
    }

    void Spawn_EnterState()
    {
        Vector3 playersAveragePosition = Vector3.zero;

        for (int i = 0; i < _players.Length; i++)
        {
            playersAveragePosition += _players[i].transform.position / _players.Length;
        }
        float maxDist;
        Vector3[] corners = new Vector3[]
        {
            new Vector3(World.Instance.Boundary.max.x, World.Instance.Boundary.max.y, 0),
            new Vector3(World.Instance.Boundary.max.x, World.Instance.Boundary.min.y, 0),
            new Vector3(World.Instance.Boundary.min.x, World.Instance.Boundary.max.y, 0),
            new Vector3(World.Instance.Boundary.min.x, World.Instance.Boundary.min.y, 0),
            new Vector3(World.Instance.Boundary.center.x, World.Instance.Boundary.max.y, 0),
            new Vector3(World.Instance.Boundary.center.x, World.Instance.Boundary.min.y, 0),
            new Vector3(World.Instance.Boundary.max.x, World.Instance.Boundary.center.y, 0),
            new Vector3(World.Instance.Boundary.min.x, World.Instance.Boundary.center.y, 0),
        };
        maxDist = Vector3.Distance(playersAveragePosition, corners[0]);
        _target = corners[0];

        for (int i = 0; i < corners.Length; i++)
        {
            float dist = Vector3.Distance(playersAveragePosition, corners[i]);
            if (dist > maxDist)
            {
                maxDist = dist;
                _target = corners[i];
            }
        }
        currentState = LutinMonsterStates.Move;
    }

    void Spawn_Update()
    {

    }

    void Move_EnterState()
    {
        GetComponent<Animator>().SetBool("Walk", true);
    }

    void Move_Update()
    {
        Vector3 direction = _target - transform.position;
        rigidBody.MovePosition(transform.position + direction.normalized * _moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _target) < 0.2f)
        {
            currentState = LutinMonsterStates.SpawnBlobs;
        }
    }

    void Move_ExitState()
    {
        GetComponent<Animator>().SetBool("Walk", false);
    }

    void SpawnBlobs_EnterState()
    {
        GetComponent<Animator>().SetBool("SpawnEnemies", true);
        _lastBlobSpawn = -1;
    }

    void SpawnBlobs_Update()
    {
        if (_lastBlobSpawn + _blobSpawnRate < Time.time)
        {
            Instantiate(_blobPrefab, transform.position, Quaternion.identity);
            _lastBlobSpawn = Time.time;
        }
    }

    void SpawnBlobs_ExitState()
    {
        GetComponent<Animator>().SetBool("SpawnEnemies", false);
    }

    void Die_EnterState()
    {
        Die();
    }

    void Die_Update()
    {

    }

}
