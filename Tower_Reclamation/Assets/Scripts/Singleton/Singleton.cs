using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class Singleton : MonoBehaviour {

    TowerFactory towerFactory = new TowerFactory();
    [SerializeField] Text levelText;
    // do not put a singleton in first map, it has static base turret for level one.
    public List<int> enemyList = new List<int>();
    // this holds the set tower choices
    public Tower tempTower;
    public Tower towerOne;
    public Tower towerTwo;
    public Tower towerThree;
    [SerializeField] Dropdown dropdown;

    TowerSelecter towerSelector;

    int towerButton = 0;
    public static Singleton Instance { get; private set; }

    [SerializeField] public int scenesChanged;
    public int level = 1;
    private int waveEnemyDifficultyChecker = 0;

    public void TowerOne()
    {
        towerFactory.AddTower(towerOne);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FindTower()
    {
        towerSelector = FindObjectOfType<TowerSelecter>();
        tempTower = towerSelector.PickTower();
        print(dropdown.value + " " + tempTower);
        //temp tower holds the new tower, swtich determines what button it takes over.need to convert to Tower instetad of towerDmG
        switch (dropdown.value)
        {
            case 1:
                towerOne = tempTower;
                print(towerOne.name);
                //FindObjectOfType<TowerButton1>().UpdateName();
                break;
            case 2:
                towerTwo = tempTower;
                //FindObjectOfType<TowerButton2>().UpdateName();
                break;
            case 3:
                towerThree = tempTower;
                //FindObjectOfType<TowerButton3>().UpdateName();
                break;
            default:
                break;
        }


    }

 //           __   Slant font           __           ____            ____                                      __     _                
 //          / /   ___  _   __  ___    / /          /  _/   ____    / __/  ____    _____   ____ ___   ____ _  / /_   (_)  ____    ____ 
 //         / /   / _ \| | / / / _ \  / /           / /    / __ \  / /_   / __ \  / ___/  / __ `__ \ / __ `/ / __/  / /  / __ \  / __ \
 //        / /___/  __/| |/ / /  __/ / /          _/ /    / / / / / __/  / /_/ / / /     / / / / / // /_/ / / /_   / /  / /_/ / / / / /
 //       /_____/\___/ |___/  \___/ /_/          /___/   /_/ /_/ /_/     \____/ /_/     /_/ /_/ /_/ \__,_/  \__/  /_/   \____/ /_/ /_/ 
                                                                                                                             
                                
     public void LevelCleared()
     {
        level++;
        levelText.text = "Level : " + level.ToString();
    }

    public void LoadLevel(int loadedLevel)
    {
        level = loadedLevel;
        levelText.text = "Level : " + level.ToString();
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void DecidedPath(List<int> chosenEnemies)
    {
        enemyList = chosenEnemies;
    }

    // Maybe move this out of here and to a script inside of base room control / meeting room?
    public List<int> GetEnemyList(List<int> newList)
    {
        for (int x = 0; x < 5; x++)
        {
            waveEnemyDifficultyChecker = 8;
            while(waveEnemyDifficultyChecker > 0)
            {
                newList.Add(PickARandoEnemy());
            }

            newList.Add(-1);
        }   
        return newList;
    }

    public int PickARandoEnemy()
    {
        int enemy = 0;
        int rng = Random.Range(0, 100);
        if(rng < 75)
        { // change to max reg enemy.
            enemy = Random.Range(1, 4);
            waveEnemyDifficultyChecker -= 1;
        } else
        {
            enemy = Random.Range(20, 22);
            waveEnemyDifficultyChecker -= 2;
        }

        return enemy;
    }

    public void LoadEnemyList(int[] enemies)
    {
        enemyList.Clear();
        foreach(int enemy in enemies)
        {
            enemyList.Add(enemy);
        }
    }
    //private static Singleton instance = null;
    //private static readonly object padlock = new object();

    //Singleton()
    //{
    //    int scenesChanged = 0;

    //}

    //public static Singleton Instance
    //{
    //    get
    //    {
    //        lock (padlock)
    //        {
    //            if (instance == null)
    //            {
    //                instance = new Singleton();
    //            }
    //            return instance;
    //        }
    //    }
    //}
}
