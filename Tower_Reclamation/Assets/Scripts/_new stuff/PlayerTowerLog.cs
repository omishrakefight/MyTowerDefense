using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTowerLog : MonoBehaviour {
    /// <summary>
    /// MAYBE MAKE THIS INTO A DICTIONARY OR LIST ANYWAYS.
    /// SAVE IT AS AN ARRAY (FINE) BUT USE IT AS A DYNAMIC CONTAINER FOR ACTUAL GAME USE.
    /// MAKES IT EASIER TO ADD AND REMOVE.
    /// </summary>
    //Dictionary<string, bool> towers;
    public bool[] towers1;

    //int numberOfTowers = 6;

    //basic starting tower
    //bool hasRifled = true;

    //bool hasAssaultTower = false;
    //bool hasFlameTurret = false;
    //bool hasLighteningTower = false;
    //bool hasPlasmaTurret = false;
    //bool hasSlowTurret = false;


    // Use this for initialization
    void Start()
    {
        towers1 = new bool[] 
        {
            true,  // Rifled Tower
            false, // Assault Tower
            false, // Flame Tower
            false, // Lightening Tower
            false, // Plasma Tower
            false  // Slow Tower
        };
        print("loaded " + towers1.Length + " towers");
        GetComponent<RandomTowerBlueprints>().ManualStart();
        //gana pull this from saved file hopefully.
        //towers.Add("hasRifled", true);
        //towers.Add("hasFlameTower", false);
        //towers.Add("hasLighteningTower", false);
        //towers.Add("hasPlasmaTower", false);
        //towers.Add("hasSlowTower", false);
        //towers.Add("hasAssaultTower", false);

    }

    public void GetTowersFromGame()
    {
        // pull them in instead of just having the dumb bools above.
    }

    public bool[] SaveTowers()
    {
        return towers1;
    }

    public void LoadTowers(bool[] loadedTowers)
    {
        this.towers1 = loadedTowers;
        GetComponent<RandomTowerBlueprints>().ManualStart();
    }

    //public Dictionary<string, bool> SaveTowers()
    //{
    //    return towers;
    //}

    //public void LoadTowers(Dictionary<string, bool> loadedTowers)
    //{
    //    this.towers = loadedTowers;
    //}

    // Update is called once per frame
    void Update()
    {

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