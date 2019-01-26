using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    [SerializeField]
    private float _moveSpeed = 20;

    // Durée de vie ou disparition hors caméra ?
    [SerializeField]
    private float _lifeTime = 5;
    private float _spawnTime;

    private void Start()
    {
        _spawnTime = Time.time;
    }

    void Update ()
    {
        transform.position += transform.up * _moveSpeed * Time.deltaTime;
        if (_spawnTime + _lifeTime < Time.time)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Monster"))
        {
            Monster monster = collision.transform.GetComponent<Monster>();
            if (monster != null)
            {
                //monster.Hit(_color);
            }
        }
    }
}
