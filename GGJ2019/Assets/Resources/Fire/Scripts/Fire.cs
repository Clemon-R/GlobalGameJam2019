using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Fire : MonoBehaviour {

    private int originalHealth = 100;
    public int actualHealth;
    private float actualRange1, actualRange2;
    private float originalRange1;
    private float originalCookie2;
    private bool isInCircle = true;

    [SerializeField]
    private Light lt1, lt2;

    private bool CheckIfPlayerInCircle(Vector2 center, float radius, Player player)
    {
        float dist = Vector2.Distance(center, player.gameObject.transform.position);
        Debug.Log("["+name+"] - Distance with player: "+player.name+", is: "+dist + ", radius: "+ radius);
        return dist <= radius;
    }

    private void AreaChecking(Player[] players)
    {
        foreach(Player player in players)
        {
            if (!CheckIfPlayerInCircle(transform.position, lt2.cookieSize / 2, player))
            {
                player.SetColor("#000000");
            } else
            {
                player.SetColor(player.GetBaseColor());
            }
        }

    }

    void Start()
    {
        actualHealth = originalHealth;
        originalRange1 = lt1.range;
        originalCookie2 = lt2.cookieSize;
        
    }
	

	// Update is called once per frame
	void Update () {
        actualRange1 = ((float)actualHealth * originalRange1) / (float)originalHealth;
        originalCookie2 = ((float)actualHealth * originalRange1) / (float)originalHealth;
        lt1.range = actualRange1;
        lt1.intensity = 3.05f;
        lt2.cookieSize = originalCookie2;
        AreaChecking(World.Instance.Players);
    }
}
