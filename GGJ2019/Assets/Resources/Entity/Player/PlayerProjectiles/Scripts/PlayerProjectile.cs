using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private ColorUtil.Colors _color;

    [SerializeField]
    private float _moveSpeed = 20;

    // Durée de vie ou disparition hors caméra ?
    [SerializeField]
    private float _lifeTime = 5;
    private float _spawnTime;

    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        _spawnTime = Time.time;
    }

    void Update ()
    {
        rigidBody.MovePosition(transform.position + transform.up * _moveSpeed * Time.deltaTime);
        if (_spawnTime + _lifeTime < Time.time)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CasterGameObject == null || CasterGameObject == collision.gameObject)
            return;
        //Si pas entity, plus chiant pour gèrer le perso faudra masse get/set + gèrer monstre et player dans le même fichier
        if (collision.transform.CompareTag("Monster"))
        {
            Monster monster = collision.transform.GetComponent<Monster>();
            if (monster != null)
            {
                //monster.TakeHit(gameObject, _color);
            }
        }
    }


    public ColorUtil.Colors GetColor()
    {
        return _color;
    }

    public void SetColor(ColorUtil.Colors color)
    {
        _color = color;
        ColorUtil.ChangeGameObjectColor(gameObject, _color);
    }

    public GameObject CasterGameObject { get; set; }
}
