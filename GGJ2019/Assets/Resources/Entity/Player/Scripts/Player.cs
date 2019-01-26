using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMachine))]
public class Player : MonoBehaviour
{
    public ColorId colorId;
    string _color;

	// Use this for initialization
	void Start ()
    {
        Debug.Log("[" + name + "] - Constructing....");
        var colorCode = "#000000";
        if (World.COLORS.Count >= (int)colorId)
            colorCode = World.COLORS[(int)colorId];
        Color.ChangeGameObjectColor(this.gameObject, colorCode);
        Debug.Log("[" + name + "] - Construct the gameobject with the color: " + colorCode);
        Debug.Log("[" + name + "] - Constructed");
    }
}
