using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerFactory : MonoBehaviour {  

    [SerializeField] RifledTower rifledTowerPrefab;
    [SerializeField] RifledTower assaultTowerPrefab;
    [SerializeField] Tower_Flame flameTowerPrefab;
    [SerializeField] LighteningTower lighteningTowerPrefab;
    [SerializeField] Transform towerParentTransform;

    // For Lights and last waypoint
    [SerializeField] Waypoint lastWaypoint;


    private void Start()
    {
        // this is how I will change the tower summons.
    }


    /// <summary>
    ///  Gold cost === how do I find a variable on an object not yet instantiated?  for finding tower gold cost.
    /// </summary>
    public void AddTower(Tower tower)
    {
        int currentGold = FindObjectOfType<GoldManagement>().CurrentGold();
        int cost = (int)FindGoldCost(tower);
        if (lastWaypoint.isAvailable && currentGold >= cost)
        {
            var newTower = Instantiate(tower, lastWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = towerParentTransform;
            lastWaypoint.isAvailable = false;
            FindObjectOfType<GoldManagement>().TowerCost(cost);
            if (lastWaypoint.CompareTag("Buff Tile"))
            {
                newTower.TowerBuff();
            }
            //print("tower name is : " + tower.name);
            //print("tower is : " + tower);
        }
        else
        {
            print("Unable to build here.");
        }
    }

    public float FindGoldCost(Tower tower)
    {
        float cost = tower.GetTowerCost();
        print(cost + " this is the cost from the overriden function of towercost!");

        return cost;
        //if (tower.name.Contains("Rifled"))
        //{
        //    return (int)TowerCosts.RifledTowerCost;
        //}
        //else if (tower.name.Contains("Assault"))
        //{
        //    return (int)TowerCosts.AssaultTowerCost;
        //}
        //else if (tower.name.Contains("Flame"))
        //{
        //    return (int)TowerCosts.FlameTowerCost;
        //}
        //else if (tower.name.Contains("Lightning"))
        //{
        //    return (int)TowerCosts.LighteningTowerCost;
        //}
        //else if (tower.name.Contains("Slow"))
        //{
        //    return (int)TowerCosts.SlowTowerCost;
        //}

        return 999;
    }

    public void AddRifledTower()
    {
        int currentGold = FindObjectOfType<GoldManagement>().CurrentGold();

        // this was used to get access to inactive game objects, but upon load it was working correctly anyways.... w/e
        //rifledTowerPrefab.GetComponentsInChildren<>(true)
        //GetComponentInChildren<RifledTower>(true).goldCost
        if (lastWaypoint.isAvailable && currentGold >= rifledTowerPrefab.goldCost)
        {
            var newTower = Instantiate(rifledTowerPrefab, lastWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = towerParentTransform;
            lastWaypoint.isAvailable = false;
            FindObjectOfType<GoldManagement>().TowerCost(rifledTowerPrefab.goldCost);
            if (lastWaypoint.CompareTag("Buff Tile"))
            {
                newTower.TowerBuff();
            }
        }
        else
        {
            print("Unable to build here.");
        }
    }

    public void AddAssaultTower()
    {
        int currentGold = FindObjectOfType<GoldManagement>().CurrentGold();
        if (lastWaypoint.isAvailable && currentGold >= assaultTowerPrefab.goldCost)
        {
            var newTower = Instantiate(assaultTowerPrefab, lastWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = towerParentTransform;
            lastWaypoint.isAvailable = false;
            print("hi" + GetComponentInChildren<RifledTower>(true).goldCost);
            FindObjectOfType<GoldManagement>().TowerCost(assaultTowerPrefab.goldCost);
            if (lastWaypoint.CompareTag("Buff Tile"))
            {
                newTower.TowerBuff();
            }
        }
        else
        {
            print("Unable to build here.");
        }
    }

    public void AddLighteningTower()
    {
        int currentGold = FindObjectOfType<GoldManagement>().CurrentGold();
        if (lastWaypoint.isAvailable && currentGold >= lighteningTowerPrefab.goldCost)
        {
            var newTower = Instantiate(lighteningTowerPrefab, lastWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = towerParentTransform;
            lastWaypoint.isAvailable = false;
            FindObjectOfType<GoldManagement>().TowerCost(lighteningTowerPrefab.goldCost);
            if (lastWaypoint.CompareTag("Buff Tile"))
            {
                newTower.TowerBuff();
            }
        }
        else
        {
            print("Unable to build here.");
        }
    }

    public void AddFlameTower()
    {
        int currentGold = FindObjectOfType<GoldManagement>().CurrentGold();
        if (lastWaypoint.isAvailable && currentGold >= flameTowerPrefab.goldCost)
        {
            var newTower = Instantiate(flameTowerPrefab, lastWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = towerParentTransform;
            lastWaypoint.isAvailable = false;
            FindObjectOfType<GoldManagement>().TowerCost(flameTowerPrefab.goldCost);
            if (lastWaypoint.CompareTag("Buff Tile"))
            {
                 newTower.TowerBuff();
            }
        }
        else
        {
            print("Unable to build here.");
        }
    }


    public void LastWaypointClicked(Waypoint waypoint)
    {
        lastWaypoint = waypoint;

    }


}

//Button baseTowerButton = Canvas.FindObjectOfType<Button>();
//baseTowerButton.transform.position = this.transform.position;