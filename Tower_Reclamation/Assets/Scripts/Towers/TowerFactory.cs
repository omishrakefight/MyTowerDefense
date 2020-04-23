using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerFactory : MonoBehaviour {  

    [SerializeField] RifledTower rifledTowerPrefab;
    [SerializeField] RifledTower assaultTowerPrefab;
    [SerializeField] Tower_Flame flameTowerPrefab;
    [SerializeField] LighteningTower lighteningTowerPrefab;
    [SerializeField] Tower_Plasma plasmaTowerPrefab;
    [SerializeField] Transform towerParentTransform;

    // For Lights and last waypoint
    [SerializeField] Waypoint lastWaypoint;
    Singleton singleton;


    private void Start()
    {
        singleton = FindObjectOfType<Singleton>();
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
            //CheckWhichUpgradesAreApplicable(tower);
        }
        else
        {
            print("Unable to build here.");
        }
    }

    public void CreateAndStackTower(Tower towerBase, GameObject towerHead, int baseType, int headType)
    {
        int currentGold = FindObjectOfType<GoldManagement>().CurrentGold();
        int cost = (int)FindGoldCost(towerBase);
        if (lastWaypoint.isAvailable && currentGold >= cost)
        {
            //var newTower = Instantiate(tower, lastWaypoint.transform.position, Quaternion.identity);
            GameObject newTower = StackTower(towerBase, towerHead, baseType, headType);
            newTower.transform.parent = towerParentTransform;
            lastWaypoint.isAvailable = false;
            FindObjectOfType<GoldManagement>().TowerCost(cost);
            if (lastWaypoint.CompareTag("Buff Tile"))
            {
                newTower.GetComponentInChildren<Tower>().TowerBuff();
            }
            //CheckWhichUpgradesAreApplicable(tower);
        }
        else
        {
            print("Unable to build here.");
        }
    }

    private GameObject StackTower(Tower towerBase, GameObject towerHead, int baseType, int headType)
    {
        var container = new GameObject();
        container.name = "Viewing Tower";
        container.transform.position = lastWaypoint.transform.position;

        float headHeight = ((towerBase.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.extents.y) * .95f); //This is to account for bigger meshes    // + (obj2.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y));
        //Instantiate(container, new Vector3(0, 0, 0), Quaternion.identity);
        var tBase = Instantiate(towerBase, lastWaypoint.transform.position, Quaternion.identity);
        // use this for the placement
        var tHead = Instantiate(towerHead, (lastWaypoint.transform.position + new Vector3(0, headHeight, 0)), Quaternion.identity); //new Vector3(0, headHeight, 0)
        tBase.transform.parent = container.transform;
        tHead.transform.parent = tBase.transform;
        tBase.DelayedStart();
        tBase.DetermineTowerHeadType(headType);
        tBase.DetermineTowerTypeBase(baseType);

        //not needed in base but w/e
        tBase.SetHead(tHead.transform);

        return container;
    }

    public float FindGoldCost(Tower tower)
    {
        int alloyResearchLevel = 0;
        float percentToPay = 100f;
        float cost = tower.GetTowerCost();
        //print(cost + " this is the cost from the overriden function of towercost!");

        // *************************I could generalize this, it doesnt matter really what upgrade it is as the output is a percent.***********************
        // put this in singleton call singleton . get percent BA M i know what any upgrade is, pass in the main thing like alloyresearch
        alloyResearchLevel = singleton.GetResearchLevel((int)TinkerUpgradeNumbers.alloyResearch);
        switch (alloyResearchLevel)
        {
            case 0:
                //this is nothing, havent researched yet
                break;
            case 1:
                percentToPay = (float)TinkerUpgradePercent.mark1;
                break;
            case 2:
                percentToPay = (float)TinkerUpgradePercent.mark2;
                break;
            case 3:
                percentToPay = (float)TinkerUpgradePercent.mark3;
                break;
            case 4:
                percentToPay = (float)TinkerUpgradePercent.mark4;
                break;
            default:
                Debug.Log("Error, case exceeded expected");
                print("Error, case exceeded expected");
                percentToPay = 1.0f;
                break;
        }
        //print("Percent to pay from local function" + percentToPay);

        percentToPay = singleton.GetPercentageModifier(alloyResearchLevel);
        //print("Percent to pay from singleton function" + percentToPay);

        cost = cost * percentToPay;
        return cost;
        //if (tower.buttonName.Contains("Rifled"))
        //{
        //    return (int)TowerCosts.RifledTowerCost;
        //}
        //else if (tower.buttonName.Contains("Assault"))
        //{
        //    return (int)TowerCosts.AssaultTowerCost;
        //}
        //else if (tower.buttonName.Contains("Flame"))
        //{
        //    return (int)TowerCosts.FlameTowerCost;
        //}
        //else if (tower.buttonName.Contains("Lightning"))
        //{
        //    return (int)TowerCosts.LighteningTowerCost;
        //}
        //else if (tower.buttonName.Contains("Slow"))
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

    public void AddPlasmaTurret()
    {
        int currentGold = FindObjectOfType<GoldManagement>().CurrentGold();
        if (lastWaypoint.isAvailable && currentGold >= plasmaTowerPrefab.goldCost)
        {
            var newTower = Instantiate(plasmaTowerPrefab, lastWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = towerParentTransform;
            lastWaypoint.isAvailable = false;
            FindObjectOfType<GoldManagement>().TowerCost(plasmaTowerPrefab.goldCost);
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