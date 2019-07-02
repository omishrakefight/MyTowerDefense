using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomTowerBlueprints : MonoBehaviour {

    private int amountOfTowers;
    private int amountOfUndiscoveredTowers;
    bool[] towers;
    PlayerTowerLog towerLog;
    List<string> undiscoveredTowers = new List<string>();

    //buttons
    [SerializeField] Button towerButtonOne;
    [SerializeField] Button towerButtonTwo;
    [SerializeField] Button towerButtonThree;
    bool towerOneInUse = false;
    bool towerTwoInUse = false;
    bool towerThreeInUse = false;

    // Use this for initialization
    void Start()
    {
        towerLog = GetComponent<PlayerTowerLog>();
        towers = towerLog.towers1;
        amountOfTowers = towers.Length;
        GetAmountOfUndiscoveredTowers();
    }
    public void ButtonOne()
    {
        // how to get the reference to a booleanspot by a string name
        if (towerTwoInUse)
        {
            undiscoveredTowers.Add(towerButtonTwo.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
        if (towerThreeInUse)
        {
            undiscoveredTowers.Add(towerButtonThree.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
    }

    private void GetAmountOfUndiscoveredTowers()
    {
        //amountOfUndiscoveredTowers = 0;
        //for(int x = 0; x < amountOfTowers; x++)
        //{
        //    if(towers[x] == false)
        //    {
        //        amountOfUndiscoveredTowers++;
        //    }
        //}
        if (towers[(int)Towers.RifledTower] == false)
        {
            undiscoveredTowers.Add("RifledTower");
        }
        if (towers[(int)Towers.AssaultTower] == false)
        {
            undiscoveredTowers.Add("AssaultTower");
        }
        if (towers[(int)Towers.FlameTower] == false)
        {
            undiscoveredTowers.Add("FlameTower");
        }
        if (towers[(int)Towers.LighteningTower] == false)
        {
            undiscoveredTowers.Add("LighteningTower");
        }
        if (towers[(int)Towers.PlasmaTower] == false)
        {
            undiscoveredTowers.Add("PlasmaTower");
        }
        if (towers[(int)Towers.SlowTower] == false)
        {
            undiscoveredTowers.Add("SlowTower");
        }
        amountOfUndiscoveredTowers = undiscoveredTowers.Count;
    }

    // Update is called once per frame
    void Update()
    {
        PickTowers();
    }

    public void PickTowers()
    {
        int limit = 3;
        if(amountOfUndiscoveredTowers < 3)
        {
            limit = amountOfUndiscoveredTowers;
        }
        for(int x = 0; x < limit; x++)
        {
            int rando = UnityEngine.Random.Range(0, amountOfUndiscoveredTowers);
            if(x == 0)
            {
                towerButtonOne.GetComponentInChildren<Text>().text = undiscoveredTowers[rando];
                undiscoveredTowers.RemoveAt(rando);
                amountOfUndiscoveredTowers--;
                towerOneInUse = true;
            }
            if (x == 1)
            {
                towerButtonTwo.GetComponentInChildren<Text>().text = undiscoveredTowers[rando];
                undiscoveredTowers.RemoveAt(rando);
                amountOfUndiscoveredTowers--;
                towerTwoInUse = true;
            }
            if (x == 2)
            {
                towerButtonThree.GetComponentInChildren<Text>().text = undiscoveredTowers[rando];
                undiscoveredTowers.RemoveAt(rando);
                amountOfUndiscoveredTowers--;
                towerThreeInUse = true;
            }
        }
        print(UnityEngine.Random.Range(0, amountOfUndiscoveredTowers));
    }

}
