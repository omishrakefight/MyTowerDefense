using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class Singleton : MonoBehaviour {

    TowerFactory towerFactory = new TowerFactory();

    // do not put a singleton in first map, it has static base turret for level one.

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
