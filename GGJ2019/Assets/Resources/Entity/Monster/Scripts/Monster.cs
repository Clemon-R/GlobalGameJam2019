using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity {
    private string colorCode;

    //Constuct for the monster
    public void Start()
    {
        Debug.Log("[" + name + "] - Constructing....");
        var randomColors = GetRandomColors();
        colorCode = Color.Mix(randomColors);
        Color.ChangeGameObjectColor(this.gameObject, colorCode);
        Debug.Log("[" + name + "] - Construct the gameobject with the color: " + colorCode);
        Debug.Log("[" + name + "] - Constructed");
    }

    private string[] GetRandomColors()
    {
        Debug.Log("[" + name + "] - Max color: " + (World.Instance.Colors.Count - 1));
        int size = Random.Range(1, World.Instance.Colors.Count);
        Debug.Log("[" + name + "] - Number of random color: " + size);
        string[] result = new string[size];
        for (int i = 0; i < size; i++)
        {
            var codeColor = World.Instance.Colors[Random.Range(1, World.Instance.Colors.Count)];
            Debug.Log("[" + name + "] - Random color found: " + codeColor);
            result[i] = World.Instance.Colors[Random.Range(1, World.Instance.Colors.Count)];
        }
        return result;
    }
}
