using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour {
    private float _start;

	// Use this for initialization
	void Awake () {
        _start = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (_start + 1 < Time.time)
            Destroy(gameObject);
	}
}
