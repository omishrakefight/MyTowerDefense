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
    public void AddTower()
    {
        int currentGold = FindObjectOfType<GoldManagement>().CurrentGold();
        if (lastWaypoint.isAvailable && currentGold >= 60)
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

    public void AddRifledTower()
    {
        int currentGold = FindObjectOfType<GoldManagement>().CurrentGold();
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
            print("hi" + assaultTowerPrefab.goldCost);
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