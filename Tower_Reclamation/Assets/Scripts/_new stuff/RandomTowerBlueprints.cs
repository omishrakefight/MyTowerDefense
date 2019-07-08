using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomTowerBlueprints : MonoBehaviour {

    private int amountOfTowers;
    public int amountOfUndiscoveredTowers;
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
        //towerLog = GetComponent<PlayerTowerLog>();
        //print("got component!");
        //towers = towerLog.towers1;
        //amountOfTowers = towers.Length;
        //GetAmountOfUndiscoveredTowers();
    }

    // call this from PlayerTowerLog because this laods first and is reliant on that script.
    // also calledo nload.
    public void ManualStart()
    {
        towerLog = GetComponent<PlayerTowerLog>();
        print("got component!");
        towers = towerLog.towers1;
        amountOfTowers = towers.Length;
        GetAmountOfUndiscoveredTowers();
        print(amountOfUndiscoveredTowers + " undiscovered");
        PickTowers();
    }

    public void ButtonOne()
    {
        // how to get the reference to a booleanspot by a string name
        LearnedANewTower(towerButtonOne.GetComponentInChildren<Text>().text);
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
        PickTowers();
    }
    public void ButtonTwo()
    {
        // how to get the reference to a booleanspot by a string name
        LearnedANewTower(towerButtonTwo.GetComponentInChildren<Text>().text);
        if (towerOneInUse)
        {
            undiscoveredTowers.Add(towerButtonOne.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
        if (towerThreeInUse)
        {
            undiscoveredTowers.Add(towerButtonThree.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
        PickTowers();
    }
    public void ButtonThree()
    {
        // how to get the reference to a booleanspot by a string name
        LearnedANewTower(towerButtonThree.GetComponentInChildren<Text>().text);
        if (towerTwoInUse)
        {
            undiscoveredTowers.Add(towerButtonTwo.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
        if (towerOneInUse)
        {
            undiscoveredTowers.Add(towerButtonOne.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
        PickTowers();
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
        undiscoveredTowers.Clear();
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

    public void LearnedANewTower(string buttonName)
    {
        print("trying to learn " + buttonName);
        if (buttonName.Equals("RifledTower"))
        {
            towerLog.towers1[(int)Towers.RifledTower] = true;
            print("tower[ " + (int)Towers.RifledTower + "] should be true");
        }
        else if (buttonName.Equals("AssaultTower"))
        {
            towerLog.towers1[(int)Towers.AssaultTower] = true;
            print("tower[ " + (int)Towers.AssaultTower + "] should be true");

        }
        else if(buttonName.Equals("FlameTower"))
        {
            towerLog.towers1[(int)Towers.FlameTower] = true;
            print("tower[ " + (int)Towers.FlameTower + "] should be true");

        }
        else if(buttonName.Equals("LighteningTower"))
        {
            towerLog.towers1[(int)Towers.LighteningTower] = true;
            print("tower[ " + (int)Towers.LighteningTower + "] should be true");

        }
        else if(buttonName.Equals("PlasmaTower"))
        {
            towerLog.towers1[(int)Towers.PlasmaTower] = true;
            print("tower[ " + (int)Towers.PlasmaTower + "] should be true");

        }
        else if(buttonName.Equals("SlowTower"))
        {
            towerLog.towers1[(int)Towers.SlowTower] = true;
            print("tower[ " + (int)Towers.SlowTower + "] should be true");

        }
    }

    // Update is called once per frame
    void Update()
    {
        //PickTowers();
    }

    public void PickTowers()
    {
        towerOneInUse = false;
        towerTwoInUse = false;
        towerThreeInUse = false;
        int limit = 3;
        //if(amountOfUndiscoveredTowers < 3)
        //{
        //    limit = amountOfUndiscoveredTowers;
        //}
        for(int x = 0; x < limit; x++)
        {
            int rando = UnityEngine.Random.Range(0, amountOfUndiscoveredTowers);
            if(x == 0)
            {
                if (amountOfUndiscoveredTowers == 0)
                {
                    towerButtonOne.GetComponentInChildren<Text>().text = "LOCKED";

                } else
                {
                    towerButtonOne.GetComponentInChildren<Text>().text = undiscoveredTowers[rando];
                    undiscoveredTowers.RemoveAt(rando);
                    amountOfUndiscoveredTowers--;
                    towerOneInUse = true;
                    //print("x was 0");
                }
            }
            if (x == 1)
            {
                if (amountOfUndiscoveredTowers == 0)
                {
                    towerButtonTwo.GetComponentInChildren<Text>().text = "LOCKED";
                }
                else
                {
                    towerButtonTwo.GetComponentInChildren<Text>().text = undiscoveredTowers[rando];
                    undiscoveredTowers.RemoveAt(rando);
                    amountOfUndiscoveredTowers--;
                    towerTwoInUse = true;
                    //print("x was 1");
                }
                           }
            if (x == 2)
            {
                if (amountOfUndiscoveredTowers == 0)
                {
                    towerButtonThree.GetComponentInChildren<Text>().text = "LOCKED";
                }
                else
                {
                    towerButtonThree.GetComponentInChildren<Text>().text = undiscoveredTowers[rando];
                    undiscoveredTowers.RemoveAt(rando);
                    amountOfUndiscoveredTowers--;
                    towerThreeInUse = true;
                    //print("x was 2");
                }
            }
            //print(rando + " is rando number");
            //print(amountOfUndiscoveredTowers + " is undiscovered towers");
        }
        
    }

}
