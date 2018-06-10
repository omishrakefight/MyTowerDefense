using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextArea : MonoBehaviour {

    bool loadNextArea;
    EnemySpawner enemySpawner;

	// Use this for initialization
	void Start () {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        

    }
	
	// Update is called once per frame
	void Update () {
       // if (enemySpawner.stillAlive == true && enemySpawner.level)
       // {

        //}


	}
    
    public void LoadTowerRoom()
    {
        SceneManager.LoadSceneAsync(2);
    }

}
