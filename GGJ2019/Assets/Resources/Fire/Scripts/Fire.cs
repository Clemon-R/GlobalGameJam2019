using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Fire : MonoBehaviour {

    private int originalHealth = 100;
    public int actualHealth;
    public float actualRange;
    private float originalRange;
    bool win;

    Light lt;

    // Use this for initialization

   /* public float newRange
    {
        get
        {
            return actualHealth;
        }
        set
        {
            actualRange = ((float)actualHealth * originalRange) / (float)originalHealth;
            if (actualRange <= 0)
                Console.Write("Game Over FDP");
        }
    }*/

    void Start()
    {
        lt = GetComponent<Light>();
        originalRange = lt.range;
        
    }
	
	// Update is called once per frame
	void Update () {
        actualRange = ((float)actualHealth * originalRange) / (float)originalHealth;
        lt.range = actualRange;
        if (actualHealth <= 0)
            Debug.Log("Game Over fdp");
	}
}
