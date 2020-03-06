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

public enum Layer
{
    Tower = 8,
    Waypoint = 9,
    Enemy = 10,
    RaycastEndStop = -1

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

public enum TinkerUpgradePercent
{
    mark1 = 92,
    mark2 = 84,
    mark3 = 76,
    mark4 = 68
}

public enum TinkerUpgradeNumbers //silver o, alloy 1, pressurized tank 2, heavy shelling 3, tower engineer 4
{
    silverWiring = 0,
    alloyResearch = 1,
    pressurizedTank = 2,
    heavyShelling = 3,
    towerEngineer = 4
}
