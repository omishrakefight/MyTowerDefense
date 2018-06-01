using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] Towers rifledTowerPrefab;
    [SerializeField] Towers assaultTowerPrefab;
    [SerializeField] Tower_Flame flameTowerPrefab;
    [SerializeField] Transform towerParentTransform;

    // For Lights and last waypoint
    [SerializeField] Waypoint lastWaypoint;
    [SerializeField] Light waypointSpotLight;
    Light oldWaypointLight;

    public void AddRifledTower()
    {
        int currentGold = FindObjectOfType<GoldManagement>().CurrentGold();
        if (lastWaypoint.isAvailable && currentGold >= 60)
        {
            var newTower = Instantiate(rifledTowerPrefab, lastWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = towerParentTransform;
            lastWaypoint.isAvailable = false;
            FindObjectOfType<GoldManagement>().TowerCost();
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
        if (lastWaypoint.isAvailable && currentGold >= 60)
        {
            var newTower = Instantiate(assaultTowerPrefab, lastWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = towerParentTransform;
            lastWaypoint.isAvailable = false;
            FindObjectOfType<GoldManagement>().TowerCost();
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
        if (lastWaypoint.isAvailable && currentGold >= 60)
        {
            var newTower = Instantiate(flameTowerPrefab, lastWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = towerParentTransform;
            lastWaypoint.isAvailable = false;
            FindObjectOfType<GoldManagement>().TowerCost();
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
        Destroy(oldWaypointLight);
        lastWaypoint = waypoint;
        Vector3 lightHeightAdjustment = new Vector3(0f, 16f, 0);
        Light currentWaypointLight = Instantiate(waypointSpotLight, waypoint.transform.position + lightHeightAdjustment, Quaternion.Euler(90, 0, 0));
        oldWaypointLight = currentWaypointLight;
    }


}

//Button baseTowerButton = Canvas.FindObjectOfType<Button>();
//baseTowerButton.transform.position = this.transform.position;