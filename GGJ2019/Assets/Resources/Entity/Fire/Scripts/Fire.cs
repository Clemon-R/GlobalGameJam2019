using System.Collections;
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
    [SerializeField]
    private int actualHealth;

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
	void Update ()
    {
        var percent = ((float)actualHealth / (float)originalHealth);
        var tmpRange = percent * (1f + (1f - percent) / 1.50f)  * originalRange ;
        var tmpSize = percent * (percent < 0.7f ? percent / 0.7f * 0.5f + 0.5f : 1) * originalCookie;
        light.range = tmpRange < minHaloRange ? minHaloRange : tmpRange;
        halo.cookieSize = tmpSize < minHaloRange ? minHaloRange : tmpSize; 
        AreaChecking(World.Instance.Players);
    }

    public void TakeHit(int damage)
    {
        actualHealth -= damage;
        Debug.Log(actualHealth);
        if (actualHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
