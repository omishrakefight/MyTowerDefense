using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TowerButton2 : MonoBehaviour {

    //[SerializeField] public Button button;
    [SerializeField] Text buttonName2;
    Singleton singleton;
    TowerFactory towerFactory;

    GameObject container;
    [SerializeField] GameObject obj1;
    [SerializeField] GameObject obj2;

    public void UpdateName()
    {
        buttonName2.text = (singleton.towerTwo.name + "   cost: " + singleton.towerTwo.GetTowerCost().ToString());
    }
    // Use this for initialization
    void Start()
    {
        towerFactory = FindObjectOfType<TowerFactory>();
        singleton = FindObjectOfType<Singleton>();
        if (singleton.towerTwo != null)
        {
            buttonName2.text = (singleton.towerTwo.name + "   cost: " + singleton.towerTwo.GetTowerCost().ToString());
        }
        else
        {
            buttonName2.text = "Unassigned";
        }
    }

    public void TestInstantiationUnderObj()
    {
        container = new GameObject();

        float headHeight = ((obj1.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y) * .93f); //This is to account for bigger meshes    // + (obj2.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y));
        //Instantiate(container, new Vector3(0, 0, 0), Quaternion.identity);
        var y = Instantiate(obj1, new Vector3(0,0,0), Quaternion.identity);
        var x = Instantiate(obj2, new Vector3(0, headHeight, 0), Quaternion.identity);
        x.transform.parent = container.transform;
        y.transform.parent = container.transform;


        //container.ad
    }

    public void BuildTower()
    {
        towerFactory.AddTower(singleton.towerTwo);
    }
}
