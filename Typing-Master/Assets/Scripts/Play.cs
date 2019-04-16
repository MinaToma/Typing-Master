using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour {

	static public void startGame()
    {
        SceneManager.LoadScene("main");
    }
    
}
