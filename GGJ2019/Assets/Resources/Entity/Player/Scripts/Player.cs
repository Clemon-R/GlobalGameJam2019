using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMachine))]
public class Player : MonoBehaviour, IEntity
{
    public ColorId colorId;

    private string _baseColor;
    private string _color;

    public void Hit(GameObject target, string colorCode)
    {
        if (target == null)
            return;
        IEntity entity = target.GetComponent<IEntity>();
        if (entity == null)
            return;
        Debug.Log("[" + name + "] - Attack: " + target.name);
        entity.TakeHit(this.gameObject, _color);
    }

    public void TakeHit(GameObject caster, string colorCode)
    {
        if (caster == null)
            return;
        Monster monster = caster.GetComponent<Monster>();
        Debug.Log("[" + name + "] - Get attacked by: "+caster.name);
        Player player;
        if (monster != null)
        {
            Destroy(this.gameObject);
        } else if ((player = caster.GetComponent<Player>()) != null)
        {
            _color = Color.Mix(_baseColor, colorCode);
            Color.ChangeGameObjectColor(this.gameObject, _color);
        }
    }


    // Use this for initialization
    void Start ()
    {
        Debug.Log("[" + name + "] - Constructing....");
        var colorCode = "#000000";
        if (World.Instance.Colors.Count >= (int)colorId)
            colorCode = World.Instance.Colors[(int)colorId];
        _baseColor = colorCode;
        _color = colorCode;
        Color.ChangeGameObjectColor(this.gameObject, colorCode);
        Debug.Log("[" + name + "] - Construct the gameobject with the color: " + colorCode);
        Debug.Log("[" + name + "] - Constructed");
    }
}
