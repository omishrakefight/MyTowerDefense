using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneBonusStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        Singleton singleton = Singleton.Instance;
        singleton.enemyList = new List<int> {  1, 1, 1, -1, 1, -1, 1 };
        //enemySpawner.enemyList = new List<int> { 1, 1, -1, 1, 1, 1};

        singleton.SetLevel(1);
	}
	
    // TODO make this maybe do a different lvl one?  Instead I load NEW base and then save immediately?

	// Update is called once per frame
	void Update () {
		
	}
}
