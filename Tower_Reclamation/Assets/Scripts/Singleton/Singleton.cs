using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class Singleton : MonoBehaviour {

    TowerFactory towerFactory = new TowerFactory();

    // do not put a singleton in first map, it has static base turret for level one.
    private List<int> enemyList = new List<int>();
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

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public List<int> GetEnemyList()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                if(y > 3) /// temporary, it adds 1-3 for variation.
                {
                    enemyList.Add(y - 3);
                }
                else
                enemyList.Add(y);
            }
            enemyList.Add(-1);
        }
        //enemyList = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        return enemyList;
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
