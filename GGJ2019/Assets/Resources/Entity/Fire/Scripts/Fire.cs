﻿using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Fire : MonoBehaviour
{

    private float actualRange, originalRange;
    private float originalCookie;

    [SerializeField]
    private Light light, halo;

    [SerializeField]
    private int originalHealth = 100;
    private int actualHealth = 100;

    [SerializeField]
    private float minHaloRange = 10f;

    private bool CheckIfPlayerInCircle(Vector2 center, float radius, Player player)
    {
        float dist = Vector2.Distance(center, player.gameObject.transform.position);
        return dist <= radius;
    }

    private void AreaChecking(Player[] players)
    {
        foreach(Player player in players)
        {
            if (!CheckIfPlayerInCircle(transform.position, halo.cookieSize / 2, player))
            {
                player.SetIsOutside(true);
            } else
            {
                player.SetIsOutside(false);
            }
        }

    }

    void Start()
    {
        actualHealth = originalHealth;
        originalRange = light.range;
        originalCookie = halo.cookieSize;
    }
	

	// Update is called once per frame
	void Update () {
        var percent = ((float)actualHealth / (float)originalHealth);
        var tmpRange = percent * originalRange;
        var tmpSize = percent * originalCookie;
        light.range = tmpRange;
        halo.cookieSize = tmpSize < minHaloRange ? minHaloRange : tmpSize; 
        AreaChecking(World.Instance.Players);
    }

    public void TakeHit(int damage)
    {
        actualHealth -= damage;
        if (actualHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
