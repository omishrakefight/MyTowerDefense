using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerButton1 : MonoBehaviour {

    //[SerializeField] public Button button;
    [SerializeField] Text buttonName1;
    Singleton singleton;
    TowerFactory towerFactory;

    public void UpdateName()
    {
        towerFactory = FindObjectOfType<TowerFactory>();
        buttonName1.text = (singleton.towerOne.name + "   cost: " + singleton.towerOne.GetTowerCost().ToString());
    }
    // Use this for initialization
    void Start()
    {
        towerFactory = FindObjectOfType<TowerFactory>();
        singleton = FindObjectOfType<Singleton>();
        if (singleton.towerOne != null)
        {
            buttonName1.text = (singleton.towerOne.name + "   cost: " + singleton.towerOne.GetTowerCost().ToString());
        } else
        {
            buttonName1.text = "Unassigned";
        }
    }

    public void BuildTower()
    {
        towerFactory = FindObjectOfType<TowerFactory>();
        towerFactory.AddTower(singleton.towerOne);
    }
}
