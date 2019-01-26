using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : StateMachine, IEntity
{
    [SerializeField]
    private float _maxHp = 100;

    private float _currentHp;
    protected string _color;

    public void Hit(GameObject target, string colorCode)
    {
        if (target == null)
            return;
        IEntity entity = target.GetComponent<IEntity>();
        if (entity == null)
            return;
        Debug.Log("[" + name + "] - Attack: "+target.name);
        entity.TakeHit(this.gameObject, _color);
    }

    public void TakeHit(GameObject caster, string colorCode)
    {
        if (caster == null)
            return;
        if (colorCode == _color)
        {
            
        }
        Debug.Log("[" + name + "] - Get attacked by: " + caster.name);
    }

    //Constuct for the monster
    protected virtual void Start()
    {
        Debug.Log("[" + name + "] - Constructing....");
        _color = GetRandomColors();
        Color.ChangeGameObjectColor(this.gameObject, _color);
        Debug.Log("[" + name + "] - Construct the gameobject with the color: " + _color);
        Debug.Log("[" + name + "] - Constructed");
        _currentHp = _maxHp;
    }

    private string GetRandomColors()
    {
        return World.Instance.Colors[Random.Range(0, World.Instance.Colors.Length)][0]; ;
    }

    public string GetColor()
    {
        return _color;
    }
}
