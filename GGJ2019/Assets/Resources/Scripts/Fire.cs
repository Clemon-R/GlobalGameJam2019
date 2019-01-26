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

    public float newRange
    {
        get
        {
            return actualHealth;
        }
        set
        {
            if (actualHealth > 0)
            {
                actualRange = ((float)actualHealth * originalRange) / (float)originalHealth;
            }
            else
                win = false;
        }
    }

    void Start()
    {
        lt = GetComponent<Light>();
        originalRange = lt.range;
    }
	
	// Update is called once per frame
	void Update () {

        actualRange = ((float)actualHealth * originalRange) / (float)originalHealth;
        Debug.Log(actualRange);
        lt.range = actualRange;
	}
}
