using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class TowerButton3 : MonoBehaviour {

    //[SerializeField] public Button button;
    [SerializeField] Text buttonName3;
    Singleton singleton;
    TowerFactory towerFactory;

    public void UpdateName()
    {
        buttonName3.text = (singleton.towerThree.name + "   cost: " + singleton.towerThree.GetTowerCost().ToString());
    }
    // Use this for initialization
    void Start()
    {
        towerFactory = FindObjectOfType<TowerFactory>();
        singleton = FindObjectOfType<Singleton>();
        //try
        //{
        //    buttonName3.text = singleton.towerThree.name;
        //}
        //catch (Exception e)
        //{
        //    // no name, then it is unassigned as of yet.
        //    buttonName3.text = "Unassigned";
        //}

        if (singleton.towerThree != null)
        {
            buttonName3.text = (singleton.towerThree.name + "   cost: " + singleton.towerThree.GetTowerCost().ToString());
        }
        else
        {
            buttonName3.text = "Unassigned";
        }

    }

    public void BuildTower()
    {
        towerFactory.AddTower(singleton.towerThree);
    }
}
