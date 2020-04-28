using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomTowerBlueprints : MonoBehaviour {

    private int amountOfTowers;
    public int amountOfUndiscoveredTowers;
    bool[] towers;
    PlayerTowerLog towerLog;
    [SerializeField] TowerSelecter turretTypes;
    Singleton singleton;
    List<string> undiscoveredTowers = new List<string>();
    List<string> discoveredTowers = new List<string>();

    //buttons
    [Header("Button One")]
    [SerializeField] Button towerButtonOne;
    [SerializeField] Text buttonOneText;
    [SerializeField] Image buttonOneImage;

    [Header("Button Two")]
    [SerializeField] Button towerButtonTwo;
    [SerializeField] Text buttonTwoText;
    [SerializeField] Image buttonTwoImage;

    [Header("Button Three")]
    [SerializeField] Button towerButtonThree;
    [SerializeField] Text buttonThreeText;
    [SerializeField] Image buttonThreeImage;

    [Header("Images")]
    [SerializeField] Sprite FlameTowerPic;
    [SerializeField] Sprite PlasmaTowerPic;
    [SerializeField] Sprite SlowTowerPic;
    [SerializeField] Sprite AssaultTowerPic;
    [SerializeField] Sprite LightningTowerPic;
    [SerializeField] Sprite AlreadyKnown;



    bool towerOneInUse = false;
    bool towerTwoInUse = false;
    bool towerThreeInUse = false;

    private const string AssaultTower = "Its mechanically like the Rifled Tower, except the focus is on attack speed rather than accuracy.  " +
        "The shorter barrel and fire time lends to more of a spray and pray tactic better against big enemies than the smaller ones.";
    private const string FlameTower = "It’s a flamethrower attached to a tower.  " +
        "This tower has a short range area of effect attack that puts a DOT on enemies.  " +
        "Unfortunately, enemies are either burning or not so stacking is less effective.";
    private const string LightningTower = "Charges up ions that when released causes a centered lightning strike.  " +
        "This tower has a short range and slow fire time, having to charge up between each hit.  " +
        "When it does fire, it hits every enemy in a small circle around the tower.";
    private const string PlasmaTower = "This is a rail gun in functionality.  " +
        "It fires a shot that has very good pierce and is able to hit multiple enemies if lined up properly.  " +
        "The tower has a slow fire rate, having to charge each shot; however, the shots deal good damage and can hit multiple enemies.";
    private const string SlowTower = "A simpler tower idea founded on the discovery that these aliens are cold blooded.  " +
        "This tower sprays a slush mix into the air and spreads it out with giant spinning fan-blades.  " +
        "The purpose of this is to slow the aliens down as their blood temperature drops.";

    // Use this for initialization
    void Start()
    {
        //towerLog = GetComponent<PlayerTowerLog>();
        //print("got component!");
        //towers = towerLog.towers1;
        //amountOfTowers = towers.Length;
        //GetAmountOfUndiscoveredTowers();
    }

    // call this from PlayerTowerLog because this laods first and is reliant on that script.
    // also calledo nload.
    public void ManualStart()
    {
        towerLog = GetComponent<PlayerTowerLog>();
        singleton = FindObjectOfType<Singleton>();
        //print(towerLog+ "I am tower log!");
        towers = towerLog.towers1;
        amountOfTowers = towers.Length;
        GetAmountOfUndiscoveredTowers();
        //this populates the buttons and checks if you can learn
        CheckIfCanLearnMoreTowers();

        turretTypes.UpdateTowersAvailable(GetDiscoveredTowers());

    }

    public void CheckIfCanLearnMoreTowers()
    {
        //TODO change to a specific towers lost pic or something.
        if (singleton.isHasLearnedATower)
        {
            PickTowers();
            towerButtonOne.interactable = false;
            towerButtonTwo.interactable = false;
            towerButtonThree.interactable = false;
        }
        else
        {
            PickTowers();
        }
    }

    public void ButtonOne()
    {
        // how to get the reference to a booleanspot by a string buttonName
        LearnedANewTower(towerButtonOne.GetComponentInChildren<Text>().text);
        if (towerTwoInUse)
        {
            undiscoveredTowers.Add(towerButtonTwo.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
        if (towerThreeInUse)
        {
            undiscoveredTowers.Add(towerButtonThree.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
        //PickTowers();
        singleton.isHasLearnedATower = true;

        towerButtonThree.interactable = false;
        towerButtonTwo.interactable = false;
    }

    public void ButtonTwo()
    {
        // how to get the reference to a booleanspot by a string buttonName
        LearnedANewTower(towerButtonTwo.GetComponentInChildren<Text>().text);
        if (towerOneInUse)
        {
            undiscoveredTowers.Add(towerButtonOne.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
        if (towerThreeInUse)
        {
            undiscoveredTowers.Add(towerButtonThree.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
        //PickTowers();
        singleton.isHasLearnedATower = true;

        towerButtonOne.interactable = false;
        towerButtonThree.interactable = false;
    }

    public void ButtonThree()
    {
        // how to get the reference to a booleanspot by a string buttonName
        LearnedANewTower(towerButtonThree.GetComponentInChildren<Text>().text);
        if (towerTwoInUse)
        {
            undiscoveredTowers.Add(towerButtonTwo.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
        if (towerOneInUse)
        {
            undiscoveredTowers.Add(towerButtonOne.GetComponentInChildren<Text>().text);
            amountOfUndiscoveredTowers++;
        }
        //PickTowers();
        singleton.isHasLearnedATower = true;

        towerButtonOne.interactable = false;
        towerButtonTwo.interactable = false;
    }

    private List<string> GetDiscoveredTowers()
    {
       
        discoveredTowers.Clear();
        if (towers[(int)Towers.RifledTower] == true)
        {
            discoveredTowers.Add("RifledTower");
        }
        if (towers[(int)Towers.AssaultTower] == true)
        {
            discoveredTowers.Add("AssaultTower");
        }
        if (towers[(int)Towers.FlameTower] == true)
        {
            discoveredTowers.Add("FlameTower");
        }
        if (towers[(int)Towers.LighteningTower] == true)
        {
            discoveredTowers.Add("LighteningTower");
        }
        if (towers[(int)Towers.PlasmaTower] == true)
        {
            discoveredTowers.Add("PlasmaTower");
        }
        if (towers[(int)Towers.SlowTower] == true)
        {
            discoveredTowers.Add("SlowTower");
        }
        //print(discoveredTowers+ "discovered towers");
        return discoveredTowers;
    }

    private void GetAmountOfUndiscoveredTowers()
    {
        undiscoveredTowers.Clear();
        if (towers[(int)Towers.RifledTower] == false)
        {
            undiscoveredTowers.Add("RifledTower");
        }
        if (towers[(int)Towers.AssaultTower] == false)
        {
            undiscoveredTowers.Add("AssaultTower");
        }
        if (towers[(int)Towers.FlameTower] == false)
        {
            undiscoveredTowers.Add("FlameTower");
        }
        if (towers[(int)Towers.LighteningTower] == false)
        {
            undiscoveredTowers.Add("LighteningTower");
        }
        if (towers[(int)Towers.PlasmaTower] == false)
        {
            undiscoveredTowers.Add("PlasmaTower");
        }
        if (towers[(int)Towers.SlowTower] == false)
        {
            undiscoveredTowers.Add("SlowTower");
        }
        amountOfUndiscoveredTowers = undiscoveredTowers.Count;
    }

    public void LearnedANewTower(string buttonName)
    {
        //print("trying to learn " + buttonName);
        if (buttonName.Equals("RifledTower"))
        {
            towerLog.towers1[(int)Towers.RifledTower] = true;
            //print("tower[ " + (int)Towers.RifledTower + "] should be true");
        }
        else if (buttonName.Equals("AssaultTower"))
        {
            towerLog.towers1[(int)Towers.AssaultTower] = true;
            //print("tower[ " + (int)Towers.AssaultTower + "] should be true");

        }
        else if(buttonName.Equals("FlameTower"))
        {
            towerLog.towers1[(int)Towers.FlameTower] = true;
            //print("tower[ " + (int)Towers.FlameTower + "] should be true");

        }
        else if(buttonName.Equals("LighteningTower"))
        {
            towerLog.towers1[(int)Towers.LighteningTower] = true;
            //print("tower[ " + (int)Towers.LighteningTower + "] should be true");

        }
        else if(buttonName.Equals("PlasmaTower"))
        {
            towerLog.towers1[(int)Towers.PlasmaTower] = true;
            //print("tower[ " + (int)Towers.PlasmaTower + "] should be true");

        }
        else if(buttonName.Equals("SlowTower"))
        {
            towerLog.towers1[(int)Towers.SlowTower] = true;
            //print("tower[ " + (int)Towers.SlowTower + "] should be true");

        }

        turretTypes.UpdateTowersAvailable(GetDiscoveredTowers());
        
    }

    // is passed in towerButtonOne.GetComponentInChildren<Text>().text
    public string SetupNewButton(string towerTextDescription, ref Image image)
    {
        // since it is a pain to pass a reference to button.text, im just going to return the string i want.
        string towerDescription = "";
        if (towerTextDescription.Equals("RifledTower"))
        {
            towerLog.towers1[(int)Towers.RifledTower] = true;
            print("tower[ " + (int)Towers.RifledTower + "] should be true");
        }
        else if (towerTextDescription.Equals("AssaultTower"))
        {
            towerDescription = AssaultTower;
            image.sprite = AssaultTowerPic;

        }
        else if (towerTextDescription.Equals("FlameTower"))
        {
            towerDescription = FlameTower;
            image.sprite = FlameTowerPic;
        }
        else if (towerTextDescription.Equals("LighteningTower"))
        {
            towerDescription = LightningTower;
            image.sprite = LightningTowerPic;

        }
        else if (towerTextDescription.Equals("PlasmaTower"))
        {
            towerDescription = PlasmaTower;
            image.sprite = PlasmaTowerPic;

        }
        else if (towerTextDescription.Equals("SlowTower"))
        {
            towerDescription = SlowTower;
            image.sprite = SlowTowerPic;

        }
        else
        {
            towerDescription = "Already known.";
            image.sprite = AlreadyKnown;
        }

        return towerDescription;
    }

    // Update is called once per frame
    void Update()
    {
        //PickTowers();
    }

    public void PickTowers()
    {
        towerOneInUse = false;
        towerTwoInUse = false;
        towerThreeInUse = false;
        int limit = 3;
        //if(amountOfUndiscoveredTowers < 3)
        //{
        //    limit = amountOfUndiscoveredTowers;
        //}
        for(int x = 0; x < limit; x++)
        {
            int rando = UnityEngine.Random.Range(0, amountOfUndiscoveredTowers);
            if(x == 0)
            {
                if (amountOfUndiscoveredTowers == 0)
                {
                    towerButtonOne.GetComponentInChildren<Text>().text = "LOCKED";
                    buttonOneText.text = SetupNewButton("no new towers", ref buttonOneImage);

                }
                else
                {
                    towerButtonOne.GetComponentInChildren<Text>().text = undiscoveredTowers[rando];
                    buttonOneText.text = SetupNewButton(undiscoveredTowers[rando], ref buttonOneImage);
                    //SetupNewButton(buttonOneText.text, ref buttonOneImage);

                    undiscoveredTowers.RemoveAt(rando);
                    amountOfUndiscoveredTowers--;
                    towerOneInUse = true;
                    //print("x was 0");
                }
            }
            if (x == 1)
            {
                if (amountOfUndiscoveredTowers == 0)
                {
                    towerButtonTwo.GetComponentInChildren<Text>().text = "LOCKED";
                    buttonTwoText.text = SetupNewButton("no new towers", ref buttonTwoImage);
                }
                else
                {
                    towerButtonTwo.GetComponentInChildren<Text>().text = undiscoveredTowers[rando];
                    buttonTwoText.text = SetupNewButton(undiscoveredTowers[rando], ref buttonTwoImage);
                    //SetupNewButton(buttonTwoText.text, ref buttonTwoImage);

                    undiscoveredTowers.RemoveAt(rando);
                    amountOfUndiscoveredTowers--;
                    towerTwoInUse = true;
                    //print("x was 1");
                }
                           }
            if (x == 2)
            {
                if (amountOfUndiscoveredTowers == 0)
                {
                    towerButtonThree.GetComponentInChildren<Text>().text = "LOCKED";
                    buttonThreeText.text = SetupNewButton("no new towers", ref buttonThreeImage);
                }
                else
                {
                    towerButtonThree.GetComponentInChildren<Text>().text = undiscoveredTowers[rando];
                    buttonThreeText.text = SetupNewButton(undiscoveredTowers[rando], ref buttonThreeImage);
                    //SetupNewButton(buttonThreeText.text, ref buttonThreeImage);

                    undiscoveredTowers.RemoveAt(rando);
                    amountOfUndiscoveredTowers--;
                    towerThreeInUse = true;
                    //print("x was 2");
                }
            }
            //print(rando + " is rando number");
            //print(amountOfUndiscoveredTowers + " is undiscovered towers");
        }
        
    }

}
