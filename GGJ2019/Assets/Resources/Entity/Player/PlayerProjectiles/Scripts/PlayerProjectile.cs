using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour, IEntity
{
    string _color;

    [SerializeField]
    private float _moveSpeed = 20;

    // Durée de vie ou disparition hors caméra ?
    [SerializeField]
    private float _lifeTime = 5;
    private float _spawnTime;

    private Rigidbody2D rigidBody;
    private string _color;

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
        IEntity target = collision.gameObject.GetComponent<IEntity>();
        IEntity caster = CasterGameObject.GetComponent<IEntity>();
        if (target == null || caster == null) //Not an entity
            return;
        caster.Hit(collision.gameObject, _color);
        target.TakeHit(CasterGameObject, _color);
        Destroy(gameObject);
        //Useless
        /*if (collision.transform.CompareTag("Monster"))
        {
            Monster monster = collision.transform.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeHit(gameObject, _color);
            }
        }*/
    }

<<<<<<< HEAD
    public void Hit(GameObject target, string colorCode)
    {
    }

    public void TakeHit(GameObject caster, string colorCode)
    {
        //throw new System.NotImplementedException();
    }

    public string GetColor()
    {
        throw new System.NotImplementedException();
    }
=======
    public void SetColor(string colorCode)
    {
        _color = colorCode;
        ColorUtil.ChangeGameObjectColor(this.gameObject, _color);
    }

    public GameObject CasterGameObject { get; set; }
>>>>>>> 6c253b1cb564fbeae1ce6b6ff22e6d6284ed1392
}
