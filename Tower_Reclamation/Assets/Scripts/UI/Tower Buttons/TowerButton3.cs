using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerButton3 : MonoBehaviour {

    //[SerializeField] public Button button;
    [SerializeField] Text buttonName3;
    Singleton singleton;
    TowerFactory towerFactory;

    public void UpdateName()
    {
        buttonName3.text = singleton.towerThree.name;
    }
    // Use this for initialization
    void Start()
    {
        towerFactory = FindObjectOfType<TowerFactory>();
        singleton = FindObjectOfType<Singleton>();
        buttonName3.text = singleton.towerThree.name;
    }

    public void BuildTower()
    {
        towerFactory.AddTower(singleton.towerThree);
    }
}
