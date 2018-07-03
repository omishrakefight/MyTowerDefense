using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextArea : MonoBehaviour {

    bool loadNextArea;
    EnemySpawner enemySpawner;

    int currentLevel;

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
    
    public void LoadBase()
    {
        SceneManager.LoadSceneAsync("_Scenes/_Base");
    }

    public void LoadNextLevel()
    {
        FindObjectOfType<LevelTracker>().IncreaseLevel();
        currentLevel = FindObjectOfType<LevelTracker>().currentLevel;      
        SceneManager.LoadSceneAsync("_Scenes/Level_ " + currentLevel.ToString());
    }
}
