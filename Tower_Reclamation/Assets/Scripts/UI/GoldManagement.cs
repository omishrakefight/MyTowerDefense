using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;

public class GoldManagement : MonoBehaviour {

    //inside enemy health dmg
    [SerializeField] public int goldCount = 100;
    public Text gold;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        GoldCounter();
    }

    public void AddGold()
    {
        goldCount = goldCount + 10;
    }

    public void GoldCounter()
    {
        gold.text = "Gold :  " + goldCount.ToString();
    }

    public int CurrentGold()
    {
        return goldCount;
    }
    public void TowerCost()
    {
        goldCount -= 60;
    }
}
