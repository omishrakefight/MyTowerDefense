using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelecter : MonoBehaviour
{
    [Header("Tower Blueprint")]
    [SerializeField] Dropdown towerBarrel;
    [SerializeField] Dropdown towerTurret;
    [SerializeField] Dropdown towerBase;

    Tower newTower;
    Tower decidedTower;
    GameObject tower = null;

    [Header("Rifle Towers")]
    [SerializeField] Tower basicRifledTower;


    [Header("Flame Towers")]
    [SerializeField] Tower basicFlameTower;
    [SerializeField] Tower tallFlameTower;
    [SerializeField] Tower heavyFlameTower;
    [SerializeField] Tower lightFlameTower;
    [SerializeField] Tower alienFlameTower;


    [Header("Lightening Towers")]
    [SerializeField] Tower basicLightTower;

    [Header("Plasma Towers")]
    [SerializeField] Tower basicPlasmaTower;

    [Header("Ice Towers")]
    [SerializeField] Tower basicIceTower;

    float turnSpeed = 6f;

    [SerializeField] GameObject towerPlaceholder;
    Vector3 towerPosition;
    Bounds bound;
    BoxCollider collider;

    [Header("Flame Base")]
    [SerializeField] Tower basicFlameTowerBase;
    [SerializeField] Tower tallFlameTowerBase;
    [SerializeField] Tower heavyFlameTowerBase;
    [SerializeField] Tower lightFlameTowerBase;
    [SerializeField] Tower alienFlameTowerBase;

    [Header("Flame Head")]
    [SerializeField] GameObject basicFlameTowerHead;
    [SerializeField] GameObject flameThrowerFlameTowerHead;

    [Header("Rifle Tower Base")]
    [SerializeField] Tower basicRifledTowerBase;

    [Header("Rifle Tower Head")]
    [SerializeField] GameObject basicRifledTowerHead;

    [Header("Plasma Tower Base")]
    [SerializeField] Tower basicPlasmaTowerBase;

    [Header("Plasma Tower Head")]
    [SerializeField] GameObject basicPlasmaTowerHead;

    [Header("Lightening Tower Base")]
    [SerializeField] Tower basicLightTowerBase;

    [Header("Ice Tower Base")]
    [SerializeField] Tower basicIceTowerBase;

    private bool changingTowerType = false;
    

    Singleton singleton;
    // Use this for initialization
    void Start()
    {
        // this value is for the turret room only sandbox.
        //towerPosition = new Vector3(5.2f, -1f, -2.70f);

        //this value is for the base turret room.
        towerPosition = towerPlaceholder.transform.position;
        //towerPosition = new Vector3(50.0f, 76f, -123.0f);

        towerBase.value = 0;
        towerTurret.value = 0;
        towerBarrel.value = 0;


        if (towerBarrel.value == 0 && towerTurret.value == 0 && towerBase.value == 0)
        {
            //print("reading dropdown");
            //newTower = Instantiate(basicRifledTower, towerPosition, Quaternion.identity);
            //newTower.transform.localScale = new Vector3(.3f, .3f, .3f);
            //tower = new GameObject();
            ResetTowerPicture();

        }
        collider = tower.GetComponentInChildren<BoxCollider>();
        bound = collider.bounds;

        //UpdateTowersAvailable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            //newTower = FindObjectOfType<Tower>();

            //newTower.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);
            var dtx = Input.GetAxis("Mouse X") * turnSpeed;
            // var dty = Input.GetAxis("Mouse Y") * turnSpeed;
            var pivot = bound.center;

            if (tower != null)
            {
                tower.transform.RotateAround(pivot, Vector3.up, dtx);
            } else
            {
                newTower.transform.RotateAround(pivot, Vector3.up, dtx);
            }

            // newTower.transform.RotateAround(pivot, Vector3.right, dty);
        }
    }

    public void ResetNumbersOnBaseChange()
    {
        changingTowerType = true;
        towerBarrel.value = 0;
        towerBase.value = 0;

        SetTowerBaseAndHead();
        ResetTowerPicture();
        changingTowerType = false;

        //test();
    }

    //private IEnumerator test()
    //{
    //    changingTowerType = true;
    //    SetTowerBaseAndHead();
    //    ResetTowerPicture();
    //    changingTowerType = false;

    //    yield return null;
    //}

    // Sent from RandomTowerBlueprints.  it calls this function.
    public void UpdateTowersAvailable(List<string> towersKnown)
    {
        towerTurret.ClearOptions();
        //foreach (string t in towersKnown)
        //{
        //    print(t + "You should see me!!");
        //}
        towerTurret.AddOptions(towersKnown);

        //print("I worked and " + towersKnown.Count);
    }

    public void ResetTowerPicture()
    {
        Tower towerBase = null;
        GameObject towerHead = null;
        int fodder1 = 0, fodder2 = 0;

        if (tower == null)
        {
           // DestroyObject(newTower.gameObject);
        } else
        {
            DestroyObject(tower.gameObject);
        }

        decidedTower = PickTower(ref towerBase, ref towerHead, ref fodder1, ref fodder2);

        SpawnTowerForViewing(towerPosition, towerBase, towerHead);

        tower.transform.localScale = new Vector3(.3f, .3f, .3f);
    }

    // plug this into the vector 3 for position, instead of the defaulted 0,0,0
    public void SpawnTowerForViewing(Vector3 position, Tower towerBase, GameObject towerHead)
    {
        var container = new GameObject();
        container.name = "Viewing Tower";
        container.transform.position = position;

        //  FOR FUTURE  
        // maybe do a case statement, that returns a vector 3.  This fills in the instantiation place.
        // this can be done with the generic being the 'head' location.  This case, though, allows for more tower combinations. 
        // IE this case could supply 'back attachment' for lightening tower.  That or I could make them 1 part only, light towers are full peices only?


        // FOR NOW  I could go into the Tower Selecter and make that one function first.  I get info from there, so it needs to work first (also fastest to test.
        // I CAN hardcode stuff i dont have yet with this hack.  For slow and light tower, put it as an empty object.  It will get added, not throw an excepttion  AND be invisible and take low power.
        print(towerBase.name);
        float headHeight = ((towerBase.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.extents.y) * .94f); //This is to account for bigger meshes    // + (obj2.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y));
        var tBase = Instantiate(towerBase, position, Quaternion.identity);
        // use this for the placement
        var tHead = Instantiate(towerHead, (position + new Vector3(0, headHeight, 0)), Quaternion.identity); //new Vector3(0, headHeight, 0)
        tBase.transform.parent = container.transform;
        tHead.transform.parent = tBase.transform;

        //not needed in base but w/e
        tBase.SetHead(tHead.transform);

        tower = container;
    }



    public Tower SetTowerBaseAndHead()
    {
        List<Dropdown.OptionData> list = towerTurret.options;

        string tower = list[towerTurret.value].text;//towerTurret.options(towerTurret.value).text;
        if (tower.Equals("RifledTower"))
        {
            //print("Rifled Tower selected");
            FocusRifledTowers();
            //StartCoroutine(FocusRifledTowerss());
            //decidedTower = basicRifledTower;
        }
        if (tower.Equals("AssaultTower"))
        {
            FocusAssaultTowers();
        }
        if (tower.Equals("FlameTower"))
        {
            FocusFireTowers();
        }
        if (tower.Equals("LighteningTower"))
        {
            FocusLighteningTowers();
        }
        if (tower.Equals("PlasmaTower"))
        {
            FocusPlasmaTowers();
        }
        if (tower.Equals("SlowTower"))
        {
            FocusSlowTowers();
        }

        return decidedTower;
    }


    private void FocusFireTowers()
    {
        towerBase.ClearOptions();
        towerBarrel.ClearOptions();
        List<string> fireBarrels = new List<string> { "Basic Barrel", "Flame Thrower" };
        List<string> fireBases = new List<string> { "Basic Base", "Tall Base", "Heavy Base", "Light Base", "Alien Base" };
        towerBarrel.AddOptions(fireBarrels);
        towerBarrel.RefreshShownValue();
        towerBase.AddOptions(fireBases);
        towerBase.RefreshShownValue();
    }

    private void FocusRifledTowers()
    {
        //towerBarrel.value = 0;
        //towerBase.value = 0;
        towerBase.ClearOptions();
        towerBarrel.ClearOptions();
        List<string> Barrels = new List<string> { "Basic Barrel" };
        List<string> rifledBases = new List<string> { "Basic Base" };
        towerBarrel.AddOptions(Barrels);
        towerBarrel.RefreshShownValue();
        towerBase.AddOptions(rifledBases);
        towerBase.RefreshShownValue();
    }
    //private IEnumerator FocusRifledTowerss()
    //{
    //    //towerBarrel.value = 0;
    //    //towerBase.value = 0;
    //    try
    //    {
    //        towerBarrel.ClearOptions();
    //        List<string> Barrels = new List<string> { "Basic Barrel" };
    //        towerBarrel.AddOptions(Barrels);
    //        towerBarrel.value = 0;
    //        towerBarrel.Select();
    //        //towerBarrel.RefreshShownValue();

    //    } catch(Exception e)
    //    {
    //        print(e.Message);
    //    }


    //    towerBase.ClearOptions();

    //    //yield return new WaitForSeconds(.15f);
    //    //print("Waited the .15!");
    //    //yield return new WaitForEndOfFrame();
    //    List<string> rifledBases = new List<string> { "Basic Base" };
    //    towerBase.AddOptions(rifledBases);
    //    towerBase.value = 0;
    //    towerBase.Select();
    //    //towerBase.RefreshShownValue();
    //    towerBarrel.value = 0;
    //    towerBase.value = 0;
    //    print("finished resetting");
    //    yield return null;
    //}

    private void FocusAssaultTowers() 
    {
        towerBase.ClearOptions();
        towerBarrel.ClearOptions();
        List<string> Barrels = new List<string> { "Basic Barrel" };
        List<string> assaultBases = new List<string> { "Basic Base" };
        towerBarrel.AddOptions(Barrels);
        towerBarrel.RefreshShownValue();
        towerBase.AddOptions(assaultBases);
        towerBase.RefreshShownValue();
    }

    private void FocusLighteningTowers()
    {
        towerBase.ClearOptions();
        towerBarrel.ClearOptions();
        List<string> Barrels = new List<string> { "Basic Barrel" };
        List<string> lighteningBases = new List<string> { "Basic Base" };
        towerBarrel.AddOptions(Barrels);
        towerBarrel.RefreshShownValue();
        towerBase.AddOptions(lighteningBases);
        towerBase.RefreshShownValue();
    }

    private void FocusSlowTowers()
    {
        towerBase.ClearOptions();
        towerBarrel.ClearOptions();
        List<string> Barrels = new List<string> { "Basic Barrel" };
        List<string> slowBases = new List<string> { "Basic Base" };
        towerBarrel.AddOptions(Barrels);
        towerBarrel.RefreshShownValue();
        towerBase.AddOptions(slowBases);
        towerBase.RefreshShownValue();
        print("finished resetting the slow tower");
    }

    private void FocusPlasmaTowers()
    {
        towerBase.ClearOptions();
        towerBarrel.ClearOptions();
        List<string> Barrels = new List<string> { "Basic Barrel" };
        List<string> plasmaBases = new List<string> { "Basic Base" };
        towerBarrel.AddOptions(Barrels);
        towerBarrel.RefreshShownValue();
        towerBase.AddOptions(plasmaBases);
        towerBase.RefreshShownValue();
    }

    /// <summary>
    /// Since FindTower() in singleton is the one I need, I put the proxy here
    /// to cover the broken reference when towerroom is deleted on load.
    /// </summary>
    /// more precisely, The button references the new singleton which kills itself on spawning in, then this is a blnak reference.
    public void ProxyFindTower()
    {
        singleton = FindObjectOfType<Singleton>();
        singleton.FindTower();
    }


    public Tower PickTower(ref Tower turretBase, ref GameObject towerHead, ref int baseType, ref int towerBarrelType)
    {
        List<Dropdown.OptionData> list = towerTurret.options;
        //for (int i = 0; i < list.Count; i++)
        //{
        if (changingTowerType)
        {
            baseType = 0;
            towerBarrelType = 0;
        } else
        {
            baseType = towerBase.value;
            towerBarrelType = towerBarrel.value;
        }

        print("3 Base:" + towerBase.value + " Barrel:" + towerBarrel.value);


        string tower = list[towerTurret.value].text;//towerTurret.options(towerTurret.value).text;

        if (tower.Equals("RifledTower"))
        {
            //print("Rifled Tower selected");
            FocusRifledTowers(ref turretBase, ref towerHead, towerBarrelType, baseType);
        }
        if (tower.Equals("AssaultTower"))
        {
            //print("AssaultTower Turret selected");
            FocusAssaultTowers(ref turretBase, ref towerHead, towerBarrelType, baseType);
        }
        if (tower.Equals("FlameTower"))
        {
            //print("Flame Turret selected");
            FocusFireTowers(ref turretBase, ref towerHead, towerBarrelType, baseType);
            //PickFireTower();
        }
        if (tower.Equals("LighteningTower"))
        {
            //print("Light Turret selected");
            FocusLighteningTowers(ref turretBase, ref towerHead, towerBarrelType, baseType);
        }
        if (tower.Equals("PlasmaTower"))
        {
            //print("PlasmaTower Turret selected");
            FocusPlasmaTowers(ref turretBase, ref towerHead, towerBarrelType, baseType);
        }
        if (tower.Equals("SlowTower"))
        {
            //print("SlowTower Turret selected");
            FocusSlowTowers(ref turretBase, ref towerHead, towerBarrelType, baseType);
        }

        return decidedTower;
    }


    private void FocusFireTowers(ref Tower turretBase, ref GameObject turretHead, int barrelVal, int baseVal)
    {
        switch (barrelVal)
        {
            case (int)FlameHead.Basic:
                turretHead = basicFlameTowerHead;
                break;
            case (int)FlameHead.FlameThrower:
                turretHead = flameThrowerFlameTowerHead;
                break;
            default:
                print("Error with selecting fire Barrel, value is appearing as : " + towerBarrel.value);
                break;
        }

        switch (towerBase.value)
        {
            case (int)FlameBase.Basic:
                turretBase = basicFlameTowerBase;
                break;
            case (int)FlameBase.Tall:
                turretBase = tallFlameTowerBase;
                break;
            case (int)FlameBase.Heavy:
                turretBase = heavyFlameTowerBase;
                break;
            case (int)FlameBase.Light:
                turretBase = lightFlameTowerBase;
                break;
            case (int)FlameBase.Alien:
                turretBase = alienFlameTowerBase;
                break;
            default:
                print("Error with selecting fire Base, value is appearing as : " + towerBase.value);
                break;
        }
    }

    private void FocusRifledTowers(ref Tower turretBase, ref GameObject turretHead, int barrelVal, int baseVal)
    {
        print("4 Base:" + towerBase.value + " Barrel:" + towerBarrel.value);

        switch (barrelVal)
        {
            case (int)RifledHead.Basic:
                turretHead = basicRifledTowerHead;
                break;
            default:
                turretHead = basicRifledTowerHead;

                print("Error with selecting Barrel, value is appearing as : " + towerBarrel.value);
                break;
        }

        switch (baseVal)
        {
            case (int)RifledBase.Basic:
                turretBase = basicRifledTowerBase;
                break;
            default:
                print("Error with selecting  Base, value is appearing as : " + towerBase.value);
                break;
        }
    }

    private void FocusAssaultTowers(ref Tower turretBase, ref GameObject turretHead, int barrelVal, int baseVal)
    {
        switch (barrelVal)
        {
            case (int)RifledHead.Basic:
                turretHead = basicRifledTowerHead;
                break;
            default:
                print("Error with selecting fire Barrel, value is appearing as : " + towerBarrel.value);
                break;
        }

        switch (baseVal)
        {
            case (int)RifledBase.Basic:
                turretBase = basicRifledTowerBase;
                break;
            default:
                print("Error with selecting fire Base, value is appearing as : " + towerBase.value);
                break;
        }
    }

    private void FocusLighteningTowers(ref Tower turretBase, ref GameObject turretHead, int barrelVal, int baseVal)
    {
        switch (barrelVal)
        {
            case (int)LightningHead.Basic:
                turretHead = new GameObject();
                break;
            default:
                print("Error with selecting  Barrel, value is appearing as : " + towerBarrel.value);
                break;
        }

        switch (baseVal)
        {
            case (int)LightningBase.Basic:
                turretBase = basicLightTowerBase;
                break;
            default:
                print("Error with selecting  Base, value is appearing as : " + towerBase.value);
                break;
        }
    }

    private void FocusSlowTowers(ref Tower turretBase, ref GameObject turretHead, int barrelVal, int baseVal)
    {
        switch (barrelVal)
        {
            case (int)IceHead.Basic:
                turretHead = new GameObject();
                break;
            default:
                print("Error with selecting  Barrel, value is appearing as : " + towerBarrel.value);
                break;
        }

        switch (baseVal)
        {
            case (int)IceBase.Basic:
                turretBase = basicIceTowerBase;
                break;
            default:
                print("Error with selecting  Base, value is appearing as : " + towerBase.value);
                break;
        }
    }

    private void FocusPlasmaTowers(ref Tower turretBase, ref GameObject turretHead, int barrelVal, int baseVal)
    {
        switch (barrelVal)
        {
            case (int)PlasmaHead.Basic:
                turretHead = basicPlasmaTowerHead;
                break;
            default:
                print("Error with selecting  Barrel, value is appearing as : " + towerBarrel.value);
                break;
        }

        switch (baseVal)
        {
            case (int)PlasmaBase.Basic:
                turretBase = basicPlasmaTowerBase;
                break;
            default:
                print("Error with selecting  Base, value is appearing as : " + towerBase.value);
                break;
        }
    }

}
//towerBarrel.options.Clear();