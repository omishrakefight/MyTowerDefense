using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadNextArea : MonoBehaviour {

    bool loadNextArea;
    EnemySpawner enemySpawner;

    [SerializeField] Button nextLevelButton;
    ChooseNextMissionPath nextPath;
    private bool pickedPath = false;

    [SerializeField] Text pickALane;

    Singleton singleton;
    SaveAndLoad save;
    int currentLevel;

	// Use this for initialization
	void Start () {
        pickALane.enabled = false;
        enemySpawner = FindObjectOfType<EnemySpawner>();
        singleton = FindObjectOfType<Singleton>();
        save = FindObjectOfType<SaveAndLoad>();
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
        singleton.LevelCleared();
        Singleton.Instance.isHasLearnedATower = false;

        SceneManager.LoadSceneAsync("_Scenes/_Base");
    }

    public void LoadNextLevel() // checks next level / wave HAS been chosen first.
    {
        //save on next wave start, that way they have the updated towers list saved for next base section.  Otherwise it wont treat them as learned.
        save.Save();

        nextPath = FindObjectOfType<ChooseNextMissionPath>();
        pickedPath = nextPath.isHasChosen;

        if (Singleton.Instance.isHasPickedAPath)
        {
            //print("level is currently: " + FindObjectOfType<LevelTracker>().currentLevel);
            //FindObjectOfType<LevelTracker>().IncreaseLevel();
            //print("level is now : " + FindObjectOfType<LevelTracker>().currentLevel);

            currentLevel = singleton.level;
            print("current level is : " + currentLevel);

            SceneManager.LoadSceneAsync("_Scenes/Level_ " + currentLevel.ToString());
            nextLevelButton.enabled = false;
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
