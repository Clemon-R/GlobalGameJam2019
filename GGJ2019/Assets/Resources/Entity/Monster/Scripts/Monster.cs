﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : StateMachine
{
    [SerializeField]
    private float _maxHp = 100;

    private float _currentHp;
    [SerializeField]
    protected ColorUtil.Colors _color;

    public void Hit(GameObject target, string colorCode)
    {
        if (target == null)
            return;
        Debug.Log("[" + name + "] - Attack: "+target.name);
    }

    public void TakeHit(int damage, ColorUtil.Colors color)
    {
        if (color == _color)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
            {
                Die();
            }
        }
        Debug.Log("[" + name + "] - Get attacked for: " + damage + " damage");
    }

    protected virtual void Die()
    {
        GameObject go = (GameObject)(Instantiate(Resources.Load("VFX/Prefabs/DeathEffects/DeathEffect_Prefab"), transform.position, Quaternion.identity));
        if (go != null)
        {
            go.GetComponent<Animator>().SetBool(_color.ToString(), true);
        }
        Destroy(gameObject);
    }

    //Constuct for the monster
    protected virtual void Start()
    {
        Debug.Log("[" + name + "] - Constructing....");
        _color = ColorUtil.GetRandomColor();
        Debug.Log("[" + name + "] - Construct the gameobject with the color: " + _color);
        Debug.Log("[" + name + "] - Constructed");
        _currentHp = _maxHp;
    }

    private string GetRandomColors()
    {
        return World.Instance.Colors[Random.Range(0, World.Instance.Colors.Length)][0]; ;
    }

    public ColorUtil.Colors GetColor()
    {
        return _color;
    }
}
