using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;

public class GoldManagement : MonoBehaviour {

    //inside enemy health dmg
    [SerializeField] public int goldCount = 120;
    public Text gold;

    // Use this for initialization
    void Start () {
        goldCount = 120;
        GoldCounter();
    }
	
	// Update is called once per frame
	void Update () {
        //GoldCounter();
    }

    public void AddGold()
    {
        goldCount = goldCount + 10;
        GoldCounter();
    }

    public void GoldCounter()
    {
        gold.text = "Gold :  " + goldCount;
    }

    public int CurrentGold()
    {
        return goldCount;
    }
    // change this to take in an int and minus tower cost (input int)
    public void TowerCost(int towerCost)
    {
        goldCount -= towerCost;
        //print("i just bough a tower i am now " + goldCount);
        GoldCounter();
    }
}
