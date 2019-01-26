﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMachine))]
public class Player : MonoBehaviour, IEntity
{
    public ColorId colorId;

    [SerializeField]
    private int _playerNumber = 1;
   
    public int PlayerNumber
    {
        get { return _playerNumber; }
    }

    private string _baseColor;
    private string _color;
    private bool _out;

    public void Hit(GameObject target, string colorCode)
    {
        if (target == null)
            return;
        IEntity entity = target.GetComponent<IEntity>();
        if (entity == null)
            return;
        Debug.Log("[" + name + "] - Attack: " + target.name);
        //entity.TakeHit(this.gameObject, _color);
    }

    public void TakeHit(GameObject caster, string colorCode)
    {
        if (caster == null)
            return;
        Monster monster = caster.GetComponent<Monster>();
        Debug.Log("[" + name + "] - Get attacked by: "+caster.name+", color: "+ colorCode);
        Player player;
        if (monster != null)
        {
            Destroy(this.gameObject);
        } else if ((player = caster.GetComponent<Player>()) != null && colorCode != "#000000")
        {
            _color = World.Instance.Colors[3][0];
            ColorUtil.ChangeGameObjectColor(this.gameObject, _color);
        }
    }


    // Use this for initialization
    void Start ()
    {
        Debug.Log("[" + name + "] - Constructing....");
        var colorCode = "#000000";
        if (World.Instance.Colors.Length >= (int)colorId)
            colorCode = World.Instance.Colors[(int)colorId][0];
        _baseColor = colorCode;
        _color = colorCode;
        ColorUtil.ChangeGameObjectColor(this.gameObject, _baseColor);
        Debug.Log("[" + name + "] - Construct the gameobject with the color: " + colorCode);
        Debug.Log("[" + name + "] - Constructed");
    }

    public bool IsOutside()
    {
        return _out;
    }

    public void SetIsOutside(bool value)
    {
        if (value == _out)
            return;
        _out = value;
        if (value)
            ColorUtil.ChangeGameObjectColor(gameObject, "#000000");
        else
            ColorUtil.ChangeGameObjectColor(gameObject, _color);
    }

    public string GetColor()
    {
        return _color;
    }

    public string GetBaseColor()
    {
        return _baseColor;
    }
}
