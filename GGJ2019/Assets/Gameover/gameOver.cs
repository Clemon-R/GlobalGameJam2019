using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameOver : MonoBehaviour {

    public Fire fireHealth;
    public TextMeshProUGUI gameOverTxt;
    Scene scene;

	// Use this for initialization
	void Start () {
        gameOverTxt.enabled = false;
        SceneManager.GetActiveScene();
        Debug.Log("Active scene is" +scene.name);
	}
	
	// Update is called once per frame
	void Update () {
		if (fireHealth.actualHealth <= 0)
        {
            Time.timeScale = 0f;
            gameOverTxt.enabled = true;
            if (Input.GetButton("Submit"))
               SceneManager.LoadScene(scene.name);
        }
	}
}
