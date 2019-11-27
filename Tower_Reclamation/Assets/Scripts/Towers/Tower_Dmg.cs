using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Dmg : MonoBehaviour {

    public float dmg = 0;
    bool flameTower = false;
    bool rifledTower = false;
    int tower = 0;

    // Use this for initialization
    void Start () {
        if (GetComponent<RifledTower>())
        {
            rifledTower = true;
            tower = 0;
            //dmg = GetComponent<RifledTower>().Damage();
        }

        if (GetComponent<Tower_Flame>())
        {
            flameTower = true;
            tower = 1;
            //dmg = GetComponent<Flame_AOE>().Damage();
        }

        if (GetComponent<LighteningTower>())
        {
            flameTower = true;
            tower = 2;
            //dmg = GetComponent<LighteningTower>().Damage();
        }

        if (GetComponent<Tower_Plasma>())
        {
            flameTower = true;
            tower = 3;
            //dmg = GetComponent<Tower_Plasma>().Damage();
        }
    }
	
    // this reduces the amount of checks needed.
    public float towerDMG()
    {
        switch(tower)
        {
            case 0:
                dmg = GetComponent<RifledTower>().Damage();
                break;
            case 1:
                dmg = GetComponent<Flame_AOE>().Damage();
                break;
            case 2:
                dmg = GetComponent<LighteningTower>().Damage();
                break;
            case 3:
                dmg = GetComponent<Tower_Plasma>().Damage();
                break;

        }

        return dmg;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
