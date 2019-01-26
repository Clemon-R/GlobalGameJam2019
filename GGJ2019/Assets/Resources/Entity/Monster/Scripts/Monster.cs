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
        var randomColors = GetRandomColors();
        _color = Color.Mix(randomColors);
        Color.ChangeGameObjectColor(this.gameObject, _color);
        Debug.Log("[" + name + "] - Construct the gameobject with the color: " + _color);
        Debug.Log("[" + name + "] - Constructed");
        Hit(tmpTarget, _color);
    }

    private string[] GetRandomColors()
    {
        Debug.Log("[" + name + "] - Max color: " + (World.Instance.Colors.Length - 1));
        int size = Random.Range(1, World.Instance.Colors.Length);
        Debug.Log("[" + name + "] - Number of random color: " + size);
        string[] result = new string[size];
        for (int i = 0; i < size; i++)
        {
            var codeColor = World.Instance.Colors[Random.Range(1, World.Instance.Colors.Length)][0];
            Debug.Log("[" + name + "] - Random color found: " + codeColor);
            result[i] = World.Instance.Colors[Random.Range(1, World.Instance.Colors.Length)][0];
        }
        return result;
    }
}
