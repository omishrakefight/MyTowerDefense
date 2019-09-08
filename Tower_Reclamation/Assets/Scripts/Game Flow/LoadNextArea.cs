using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadNextArea : MonoBehaviour {

    bool loadNextArea;
    EnemySpawner enemySpawner;

    ChooseNextMissionPath nextPath;
    private bool pickedPath = false;

    [SerializeField] Text pickALane;

    int currentLevel;

	// Use this for initialization
	void Start () {
        pickALane.enabled = false;
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
        // add singleton reset here**

        SceneManager.LoadSceneAsync("_Scenes/_Base");
    }

    public void LoadNextLevel() // checks next level / wave HAS been chosen first.
    {
        nextPath = FindObjectOfType<ChooseNextMissionPath>();
        pickedPath = nextPath.isHasChosen;

        if (pickedPath)
        {
            FindObjectOfType<LevelTracker>().IncreaseLevel();
            currentLevel = FindObjectOfType<LevelTracker>().currentLevel;
            SceneManager.LoadSceneAsync("_Scenes/Level_ " + currentLevel.ToString());
            //testing purposes
            Singleton.Instance.scenesChanged++;
        }
        else
        {
            StartCoroutine(YouMustPickAPathTextShowing());
        }
    }

    public IEnumerator YouMustPickAPathTextShowing()
    {
        //Joey
        pickALane.enabled = true;
        yield return new WaitForSeconds(4);

        pickALane.enabled = false;
        yield break;
    }

    // get a co-routine going to display message for pick a lane.
}
