using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

	// Use this for initialization
	public void GameOver()
    {
        Debug.Log("Hey");
        SceneManager.LoadScene("Game");
    }
}
