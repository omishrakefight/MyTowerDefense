using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum Towers
{
    RifledTower = 0,
    AssaultTower = 1,
    FlameTower = 2,
    LighteningTower = 3,
    PlasmaTower = 4,
    SlowTower = 5
}

public enum TowerCosts
{
    RifledTowerCost = 50,
    AssaultTowerCost = 50,
    FlameTowerCost = 60,
    LighteningTowerCost = 80,
    PlasmaTowerCost = 70,
    SlowTowerCost = 60
}
