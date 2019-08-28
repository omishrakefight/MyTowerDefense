using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChooseNextMissionPath : MonoBehaviour {

    Singleton singleton;

    [SerializeField] Text choiceOneDescription;
    [SerializeField] Text choiceTwoDescription;


    public List<int> firstEnemySet = new List<int>();
    public List<int> secondEnemySet = new List<int>();

    private int mostCommonEnemy = 0;
    private float mostCommonEnemyCount = 0f;

    private int percentOfEnemies = 0;

    // Use this for initialization
    void Start () {

        singleton = FindObjectOfType<Singleton>();

        GetEnemyPathChoices();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void GetEnemyPathChoices()
    {
        firstEnemySet = singleton.GetEnemyList();
        CalculateMostCommonEnemy(firstEnemySet);
        choiceOneDescription.text = "We are seeing a lot of " + mostCommonEnemy.ToString()
            + ".  They comprise about " + percentOfEnemies.ToString() + "% of the enemies.";

        secondEnemySet = singleton.GetEnemyList();
        CalculateMostCommonEnemy(secondEnemySet);
        choiceTwoDescription.text = "We are seeing a lot of " + mostCommonEnemy.ToString()
            + ".  They comprise about " + percentOfEnemies.ToString() + "% of the enemies.";
    }


    //gets button information
    private void CalculateMostCommonEnemy(List<int> enemySet)
    {
        Dictionary<int, int> enemyCalc = new Dictionary<int, int>();

        float enemyCount = 0;

        // this adds all enemies in the list to a dictionary, compacting them into a dynamic summation of their count.
        foreach (int currentEnemy in enemySet)
        {
            if (enemyCalc.ContainsKey(currentEnemy))
            {
                enemyCalc[currentEnemy] += 1;
                print("im adding a repeat! enemy # is at " + enemyCalc[currentEnemy]);
            }
            else
            {
                enemyCalc.Add(currentEnemy, 1);
            }
        }

        //this adds total enemy count for %, as well as finds most common enemy.
        foreach(KeyValuePair<int, int> entry in enemyCalc)
        {
            print("im inside the dictionary loop!");
            enemyCount += entry.Value;
            if(mostCommonEnemyCount < entry.Value)
            {
                mostCommonEnemyCount = entry.Value;
                mostCommonEnemy = entry.Key;
            }
        }
        float floatPercent = ((mostCommonEnemyCount / enemyCount) * 100);
        percentOfEnemies = Mathf.RoundToInt( floatPercent );
        print("float % = " + floatPercent + " and enemy % =" + percentOfEnemies + "  -- common enemy count = " + mostCommonEnemyCount+ " and enemy count = " + enemyCount);
    }


}
