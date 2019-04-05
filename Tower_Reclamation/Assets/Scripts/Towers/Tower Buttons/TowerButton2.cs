using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton2 : MonoBehaviour {

    //[SerializeField] public Button button;
    [SerializeField] Text buttonName2;
    Singleton singleton;
    TowerFactory towerFactory;

    public void UpdateName()
    {
        buttonName2.text = singleton.towerTwo.name;
    }
    // Use this for initialization
    void Start()
    {
        towerFactory = FindObjectOfType<TowerFactory>();
        singleton = FindObjectOfType<Singleton>();
        buttonName2.text = singleton.towerTwo.name;
    }

    public void BuildTower()
    {
        towerFactory.AddTower(singleton.towerTwo);
    }
}
