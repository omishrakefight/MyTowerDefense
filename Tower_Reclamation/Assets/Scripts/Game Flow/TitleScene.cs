using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("Start!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameStart()
    {
        print("function called");
        SceneManager.LoadSceneAsync(1);

    }
}
