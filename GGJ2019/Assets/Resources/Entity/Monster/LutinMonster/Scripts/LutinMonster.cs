using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LutinMonster : Monster
{
    public enum LutinMonsterStates { Spawn, Move, SpawnBlobs, Die}
    private Player[] _players;

    [SerializeField]
    private float _moveSpeed;

    private Rigidbody2D rigidBody;
    private Vector3 _target = Vector3.zero;

    [SerializeField]
    private GameObject _blobPrefab;
    [SerializeField]
    private float _blobSpawnRate;
    private float _lastBlobSpawn;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
        _players = World.Instance.Players;
        currentState = LutinMonsterStates.Spawn;
    }

    void Spawn_EnterState()
    {
        Vector3 playersAveragePosition = Vector3.zero;

        for (int i = 0; i < _players.Length; i++)
        {
            playersAveragePosition += _players[i].transform.position / _players.Length;
        }
        currentState = LutinMonsterStates.Move;
    }

    void Spawn_Update()
    {

    }

    void Move_EnterState()
    {

    }

    void Move_Update()
    {
            Vector3 direction = _target - transform.position;
            rigidBody.MovePosition(transform.position + direction.normalized * _moveSpeed * Time.deltaTime);
    }

    void SpawnBlobs_EnterState()
    {
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

    void Die_EnterState()
    {
        Destroy(gameObject);
    }

    void Die_Update()
    {

    }

}
