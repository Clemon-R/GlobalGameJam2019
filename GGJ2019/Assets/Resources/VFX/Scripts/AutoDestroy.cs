using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

    [SerializeField]
    private float _timeToDestroy;

    private float _spawnTime;

	void Start ()
    {
        _spawnTime = Time.time;
	}
	
	void Update ()
    {
        if (_timeToDestroy + _spawnTime < Time.time)
        {
            Destroy(gameObject);
        }
	}
}
