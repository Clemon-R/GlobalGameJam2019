using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IEntity
{
    public GameObject tmpTarget;
    private string _color;

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
        Debug.Log("[" + name + "] - Get attacked by: " + caster.name);
    }

    //Constuct for the monster
    public void Start()
    {
        Debug.Log("[" + name + "] - Constructing....");
        var randomColor = GetRandomColors();
        _color = randomColor;
        Color.ChangeGameObjectColor(this.gameObject, _color);
        Debug.Log("[" + name + "] - Construct the gameobject with the color: " + _color);
        Debug.Log("[" + name + "] - Constructed");
        Hit(tmpTarget, _color);
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
