using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;

public class GoldManagement : MonoBehaviour {

    //inside enemy health dmg
    [SerializeField] public int goldCount = 150;
    private bool setGoldManually = false;
    public Text gold;
    float goldTimer = 0f;

    // Use this for initialization
    void Start () {
        if (!setGoldManually)
        {
            goldCount = 150;
        }
        GoldCounter();
    }
	
	// Update is called once per frame
	void Update () {
        //GoldCounter();
        goldTimer += Time.deltaTime;
        if (goldTimer > 2f)
        {
            goldTimer =- 2.0f;
            AddGold(1);
        }
    }

    public void SetGoldAmount(int newAmount)
    {
        goldCount = newAmount;
        setGoldManually = true;
        GoldCounter();
    }

    public void AddGold(float money)
    {
        // maybe do this so that goldcount is float, but onyl displays int? round to 2?

        goldCount = goldCount + (int)money;
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
