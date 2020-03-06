using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public sealed class Singleton : MonoBehaviour {

    const string towerNumTag = "Tower Number Dropdown";
    TowerFactory towerFactory;
    [SerializeField] Text levelText;
    // do not put a singleton in first map, it has static base turret for level one.
    public List<int> enemyList = new List<int>();
    // this holds the set tower choices
    public Tower tempTower;
    public Tower towerOne;
    public Tower towerTwo;
    public Tower towerThree;
    protected Dropdown dropdown;

    public EnemyHealth preferedTargetEnemy = null;


    TowerSelecter towerSelector;

    int towerButton = 0;
    public static Singleton Instance { get; private set; }

    [SerializeField] public int scenesChanged;
    public int level = 1;
    private int waveEnemyDifficultyChecker = 0;

    public bool isHasPickedAPath = false;
    public bool isHasLearnedATower = false;

    public void TowerOne()
    { 
        towerFactory = new TowerFactory();
        towerFactory.AddTower(towerOne);
    }

    // TODO something with the load destroying references, that i cannot apply new towers (old references bad !!! THE TOWER BUTTONS!!!!
    // This keeps the old towers, they are never reset.  Look into this, maybe the singleton is getting destroyed?  either way they persist when everything else wipes maybe error there.  They have a bad towerfactory!!!
    // it ge ts routed to creation wiht a singleton it gets destroyed.  After load it doesnt work it has no activation.
    //Fix, route the function through something  that persists, THEN that thing calls the singleton function.  Roundabout but works.

    // Use this for initialization
    void Start()
    {

        level = 2;
        levelText.text = "Level : " + level.ToString();
        silverWiring = true;
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
        dropdown = GameObject.FindGameObjectWithTag(towerNumTag).GetComponent<Dropdown>();
        towerSelector = FindObjectOfType<TowerSelecter>();
        tempTower = towerSelector.PickTower();
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
        print(level + "is the level now!!!!!!");
        levelText.text = "Level : " + level.ToString();
    }

    public void LoadLevel(int loadedLevel)
    {
        level = loadedLevel;
        levelText.text = "Level : " + level.ToString();
    }



    // Update is called once per frame
    void Update()
    {

    }

//          ______                                      
//         / ____/  ____     ___     ____ ___     __ __
//        / __/    / __ \   / _ \   / __ `__ \   / / / /
//       / /___   / / / /  /  __/  / / / / / /  / /_/ / 
//      /_____/  /_/ /_/   \___/  /_/ /_/ /_/   \__, /  
//                                             /____/ 


    public void SetPreferedEnemy(EnemyHealth newEnemy)
    {
        Tower[] towers = FindObjectsOfType<Tower>();
        if (towers.Length != 0) {
            foreach (Tower tower in towers)
            {
                tower.preferedEnemyBody = newEnemy;
            }
            FindObjectOfType<PreferedEnemyPanel>().SetTargetEnemy(newEnemy);
        }
    }

    public void DecidedPath(List<int> chosenEnemies)
    {
        enemyList = chosenEnemies;
        //foreach (int x in enemyList)
        //{
        //    print(x);
        //}
    }

    public List<int> GetEnemyList()
    {
        return enemyList;
    }

    // Maybe move this out of here and to a script inside of base room control / meeting room?
    public List<int> CreateEnemyList(List<int> newList)
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
        int rng = UnityEngine.Random.Range(0, 100);
        if(rng < 75)
        { // change to max reg enemy.
            enemy = UnityEngine.Random.Range(1, 5);
            waveEnemyDifficultyChecker -= 1;
        } else
        {
            enemy = UnityEngine.Random.Range(20, 22);
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

    //    _______       __                __ __                            __         
    //   /_ __ (_)___  / / _____ _____   / / / /___  ____ __________ _____/ /__ _____
    //    / / / / __ \/ //_/ _ \/ ___/  / / / / __ \/ __ `/ ___/ __ `/ __  / _ \/ ___/
    //   / / / / / / / ,< /  __/ /     / /_/ / /_/ / /_/ / /  / /_/ / /_/ /  __(__  )
    //  /_/ /_/_/ /_/_/|_|\___/_/      \____/ .___/\__, /_/   \__,_/\__,_/\___/____/  
    //                                     /_/    /____/                                          
    private List<int> tinkerUpgrades = null;

    public void SendUpdateTinkerUpgrades(List<int> _tinkerUpgrades)
    {
        CheckIfNeedList();
        tinkerUpgrades = _tinkerUpgrades;
    }

    public void GetUpdateTinkerUpgrades()
    {
        TinkerUpgrades upgrades = FindObjectOfType<TinkerUpgrades>();
        tinkerUpgrades = upgrades.GetTinkerUpgrades();
    }

    public int GetResearchLevel(int x)
    {
        CheckIfNeedList();
        return tinkerUpgrades[x];
    }

    private void CheckIfNeedList()
    {
        if (tinkerUpgrades == null)
        {
            TinkerUpgrades upgrades = FindObjectOfType<TinkerUpgrades>();
            tinkerUpgrades = upgrades.GetTinkerUpgrades();
        }
    }

    public float GetPercentageModifier(int tinkerUpgrade)
    {
        CheckIfNeedList();

        float returnPercentModifier = 1.0f;
        try
        {
            int version = tinkerUpgrades[tinkerUpgrade];
            switch (version)
            {
                case 0:
                    //this is 100 - 100 is 0% bonus
                    returnPercentModifier = 100.0f;
                    break;
                case 1:
                    returnPercentModifier = (float)TinkerUpgradePercent.mark1;
                    break;
                case 2:
                    returnPercentModifier = (float)TinkerUpgradePercent.mark2;
                    break;
                case 3:
                    returnPercentModifier = (float)TinkerUpgradePercent.mark3;
                    break;
                case 4:
                    returnPercentModifier = (float)TinkerUpgradePercent.mark4;
                    break;
                default:
                    Debug.Log("Error, case exceeded expected");
                    print("Error, case exceeded expected");
                    returnPercentModifier = 1.0f;
                    break;
            }
            returnPercentModifier = returnPercentModifier / 100f;
        } catch (Exception e)
        {
            Debug.Log("Error, tinkerUpgrade not found");
            print("Error, tinkerUpgrade not found");
        }
        return returnPercentModifier;
    }

    public bool silverWiring = false;
    public bool goldWiring = false;
    public bool platinumWiring = false;
    public bool diamondWiring = false;

    public bool alloyReasearchI = false;
    public bool alloyReasearchII = false;
    public bool alloyReasearchIII = false;
    public bool alloyReasearchIV = false;

    public bool sturdyTankI = false;
    public bool sturdyTankII = false;
    public bool sturdyTankIII = false;
    public bool sturdyTankIV = false;

    public bool heavyShellingI = false;
    public bool heavyShellingII = false;
    public bool heavyShellingIII = false;
    public bool heavyShellingIV = false;

    public bool towerEngineerI = false;
    public bool towerEngineerII = false;
    public bool towerEngineerIII = false;
    public bool towerEngineerIV = false;


}
