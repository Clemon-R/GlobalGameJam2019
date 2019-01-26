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
    bool win;

    [SerializeField]
    private Light lt1, lt2;

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
        lt2.cookieSize = originalCookie2 / 2;
	}
}
