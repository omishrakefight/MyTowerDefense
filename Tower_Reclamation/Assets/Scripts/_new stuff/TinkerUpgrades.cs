﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TinkerUpgrades : MonoBehaviour {

    bool isLoadedFromSave = false;

    List<int> currentUpgradeLevels = new List<int>();
    List<int> learnableUpgrades = new List<int>();
    List<int> pickedUpgrades = new List<int>();
    public bool selected = false;
    public static int numSelected;
    public int randomPick;
    public Color baseColor;

    // KYLE CHECK to see if I only need one of these, set it on a button, then custom set up each serialized field, same script though.
    [SerializeField] Text description;
    string selectedDescription = "";
    // maybe do a button
    [SerializeField] Text buttonName;
    // Use this for initialization

    // Have a single array that keeps track of the highest version IE [0, 0, 4, 1, 1], means no upgrades first two and a mark 4 on the third.  Have the array known.
    // maybe store that way for saves, but it is easier to utilize seperate.  have them as ints, and then have silver wiring = 0.  upgrade, silver wiring = 1.  
    // Base it off the ints, an do a switchh to keep it easier?
	void Start () {
        if (!isLoadedFromSave)
        {
            currentUpgradeLevels = new List<int>() { 1, 2, 0, 0, 0};
            learnableUpgrades = new List<int>() { 0, 1 };
        }

        PickTower();
        baseColor = buttonName.GetComponentInParent<Button>().GetComponent<Image>().color;

    }

    public void UpdateDescription()
    {
        description.text = selectedDescription;

        if (selected)
        {
            numSelected--;
            buttonName.GetComponentInParent<Button>().GetComponent<Image>().color = baseColor;
            selected = false;

        }
        else 
        {
            if (numSelected < 2)
            {
                numSelected++;
                buttonName.GetComponentInParent<Button>().GetComponent<Image>().color = Color.cyan;
                selected = true;
            }
        }
    }

    public void Selected()
    {

    }

    public void PickTower()
    {
        randomPick = Random.Range(0, learnableUpgrades.Count);
        int version = currentUpgradeLevels[randomPick] + 1;

        switch (randomPick)
        {
            case 0:
                buttonName.text = "Silver Wiring: Mark " + version.ToString();
                switch (version)
                {
                    case 1:
                        selectedDescription = silverWiringI;
                        break;
                    case 2:
                        selectedDescription = silverWiringII;
                        break;
                    case 3:
                        selectedDescription = silverWiringIII;
                        break;
                    case 4:
                        selectedDescription = silverWiringIV;
                        break;
                }
                break;
            case 1:
                buttonName.text = "Alloy Research: Mark " + version.ToString();
                switch (version)
                {
                    case 1:
                        selectedDescription = alloyReasearchI;
                        break;
                    case 2:
                        selectedDescription = alloyReasearchII;
                        break;
                    case 3:
                        selectedDescription = alloyReasearchIII;
                        break;
                    case 4:
                        selectedDescription = alloyReasearchIV;
                        break;
                }
                break;
        }
    }

    public void getWiringVersion(int version)
    {
        buttonName.text = "Silver Wiring: Mark " + version.ToString();
        switch (version)
        {
            case 0:
                description.text = silverWiringI;
                break;
            case 1:
                description.text = silverWiringII;
                break;
            case 2:
                description.text = silverWiringIII;
                break;
            case 3:
                description.text = silverWiringIV;
                break;
        }
    }

    public void getAlloyVersion(int version)
    {
        buttonName.text = "Alloy Research: Mark " + version.ToString();
        switch (version)
        {
            case 0:
                description.text = alloyReasearchI;
                break;
            case 1:
                description.text = alloyReasearchII;
                break;
            case 2:
                description.text = alloyReasearchIII;
                break;
            case 3:
                description.text = alloyReasearchIV;
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
    // hmm not bad if I count on getting 10 of these with 4 upgrades each, 40 upgrades is not bad.

    string silverWiringI = "With a more conductive wiring, faster processes are enabled.  The tower can acquire targets and shoot at them faster.";
    string alloyReasearchI = "By studying the art of alloy smelting, one can produce more quantity of the metals.  Increasing supply has lowered the cost.";
    string sturdyTankI = "With research into more compressed tanks, we can put the product inside under more pressure, spraying in a farthur area.";
    string heavyShellingI = "With study into the carpaces of the aliens, we have found out how to make the ballistics penetrate them more easily.";
    string towerEngineerI = "Through a better study of towers, the ramping cost of continuous upgrades is cheaper.";

    string silverWiringII = "With a more conductive wiring, faster processes are enabled.  The tower can acquire targets and shoot at them faster. Mark II";
    string alloyReasearchII = "By studying the art of alloy smelting, one can produce more quantity of the metals.  Increasing supply has lowered the cost.  Mark II";
    string sturdyTankII = "With research into more compressed tanks, we can put the product inside under more pressure, spraying in a farthur area.  Mark II";
    string heavyShellingII = "With study into the carpaces of the aliens, we have found out how to make the ballistics penetrate them more easily.  Mark II";
    string towerEngineerII = "Through a better study of towers, the ramping cost of continuous upgrades is cheaper.  Mark II";

    string silverWiringIII = "With a more conductive wiring, faster processes are enabled.  The tower can acquire targets and shoot at them faster.   Mark III";
    string alloyReasearchIII = "By studying the art of alloy smelting, one can produce more quantity of the metals.  Increasing supply has lowered the cost.   Mark III";
    string sturdyTankIII = "With research into more compressed tanks, we can put the product inside under more pressure, spraying in a farthur area.   Mark III";
    string heavyShellingIII = "With study into the carpaces of the aliens, we have found out how to make the ballistics penetrate them more easily.  Mark III";
    string towerEngineerIII = "Through a better study of towers, the ramping cost of continuous upgrades is cheaper.   Mark III";

    string silverWiringIV = "With a more conductive wiring, faster processes are enabled.  The tower can acquire targets and shoot at them faster.   Mark IV";
    string alloyReasearchIV = "By studying the art of alloy smelting, one can produce more quantity of the metals.  Increasing supply has lowered the cost.   Mark IV";
    string sturdyTankIV = "With research into more compressed tanks, we can put the product inside under more pressure, spraying in a farthur area.   Mark IV";
    string heavyShellingIV = "With study into the carpaces of the aliens, we have found out how to make the ballistics penetrate them more easily.   Mark IV";
    string towerEngineerIV = "Through a better study of towers, the ramping cost of continuous upgrades is cheaper.   Mark IV";
    //string x = "";
    //string x = "";
    //string x = "";
    //string x = "";
    string scavengerI = "Through an edept scavenging eye, you start with more tower resources.";
    string usableCart = "Finding a cart that was repairable, you can now cart some metal beams from site to site, increasing Base life.";
    string rifleI = "In desperate situation, people will do anything.  But YOU found the rifle so its yours right?";
    string tarI = "In desperation, people will do anything.  This shows them were the tar and fire is for when the aliens make it to your bases door.";

    // check level layout and monster quantity.  earn this?
    // maybe have final upgrades for if you complete a full set? like starting gold +++++ is start wiht a free tower?

}
