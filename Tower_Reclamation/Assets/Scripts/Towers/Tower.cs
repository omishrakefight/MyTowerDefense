using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour {

    public int goldCost = 60;

    // paramteres of each tower
    [SerializeField] public float attackRange = 9f;
    protected float currentAttackRange = 0;

    protected string TowerTypeExplanation = "";
    protected string TowerAugmentExplanation = "";
    protected string TowerBaseExplanation = "";
    protected string TowerBaseFlavorTxt = "";
    protected string TowerStatsTxt = "";


    [SerializeField] public float towerDmg = 30;
    [SerializeField] protected float currentTowerDmg = 30;

    List<EnemyMovement> targets;
    [SerializeField] public Transform targetEnemy;
    [SerializeField] protected Transform objectToPan;
    public EnemyHealth targetEnemyBody;
    public EnemyHealth preferedEnemyBody = null;

    // for tinker upgrades
    public bool cantargettingModule = false;
    public bool canAlloyReasearch = false;
    public bool canSturdyTank = false;
    public bool canHeavyShelling = false;
    public bool canTowerEngineer = false;

    // Use this for initialization
    // Buff info
    public bool keepBuffed = false;

    protected virtual void Start()
    {
        //preferedEnemyBody = FindObjectOfType<Singleton>().preferedTargetEnemy;
    }

    public void TowerBuff()
    {
        keepBuffed = true;
    }

    public void CheckAndApplyBuff()
    {
        //currentAttackRange = attackRange;
        // nothing if it is unbuffed 
        if (!keepBuffed) { }
        else
        {
            print(towerDmg + " = towerdmg, current = " + currentTowerDmg);
            attackRange += (currentAttackRange * .2f);
            towerDmg += (currentTowerDmg * .2f);
            print("towerDMG after 2nd buff " + towerDmg);
        }
    }

    public void TowerUpgrade()
    {
        // attackRange = attackRange * (1.0 + .2 * timesBuffed)

        //attackRange = attackRange * 1.2f;

        if (keepBuffed)
        {
            TowerBuff();
        }
    }

    // returns how much dmg this tower does.
    public float Damage()
    {
        return currentTowerDmg;
    }


    public void SetTargetEnemy()
    {
        bool getNext = false;
        var sceneEnemies = FindObjectsOfType<EnemyHealth>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform; // change default?
        // This meanas grab next in comparisons
        if (!sceneEnemies[0].isTargetable)
        {
            getNext = true;
        }

        foreach (EnemyHealth testEnemy in sceneEnemies)
        {
            if (testEnemy.isTargetable) // checking for health script enabled
            {
                if (getNext)
                {
                    closestEnemy = testEnemy.transform;
                }

                closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
                targetEnemyBody = testEnemy;
            }
        }

        targetEnemy = closestEnemy;
    }

    protected virtual Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distanceToA = Vector3.Distance(transform.position, transformA.position);
        var distanceToB = Vector3.Distance(transform.position, transformB.position);

        if (distanceToA <= distanceToB)
        {
            return transformA;
        }
        else
        {
            return transformB;
        }
    }

    public virtual float GetTowerCost()
    {
        return 1000f;
    }

    // Update is called once per frame
    void Update () {
		
	}


    public void CheckUpgradesForTankTower(ref float towerDmg, ref float TankAOERange)
    {
        Singleton singleton = Singleton.Instance;
        float percentModifier = 1.0f;
        float multiplyFodder = 1.0f;
        float amountToAdd = 0f;

        float baseTowerDmg = towerDmg;
        float baseTankAOERange = TankAOERange;

        percentModifier = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.pressurizedTank);

        // Pressurized tank is 75% dmg 25% range.  AOE towers get more AOE than dmg.
        percentModifier = multiplyFodder - percentModifier;
        amountToAdd = ((percentModifier * baseTowerDmg) * .75f);
        towerDmg += amountToAdd;

        amountToAdd = ((percentModifier * baseTankAOERange) * .25f);
        TankAOERange += amountToAdd;

        percentModifier = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.targettingModule);
        percentModifier = multiplyFodder - percentModifier;
        amountToAdd = (percentModifier * baseTankAOERange);
        TankAOERange += amountToAdd;
    }

    public void CheckUpgradesForRifledTower(ref float towerDmg, ref float towerRange)
    {
        Singleton singleton = Singleton.Instance;
        float percentModifier = 1.0f;
        float multiplyFodder = 1.0f;
        float amountToAdd = 0f;

        float baseTowerDmg = towerDmg;
        float baseTowerRange = towerRange;

        percentModifier = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.heavyShelling);

        // Heavy shelling is more dmg oriented and gets full value.
        percentModifier = multiplyFodder - percentModifier;
        amountToAdd = (percentModifier * baseTowerDmg);
        towerDmg += amountToAdd;

        percentModifier = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.targettingModule);
        percentModifier = multiplyFodder - percentModifier;
        amountToAdd = (percentModifier * baseTowerRange);
        towerRange += amountToAdd;
    }

    // split this into 2, one for rifled towers and one for tank towers.
    // increase this, tanks increase dmg, shelling increases dmg for bullets, and silver wiring can do range?
    public void CheckWhichUpgradesAreApplicable(ref float towerDmg, ref float TankAOERange)
    {
        Singleton singleton = Singleton.Instance;
        float percentModifier = 1.0f;
        float multiplyFodder = 1.0f;
        float amountToAdd = 0f;
        //print("Checking if tower can be upgraded. . .");
        //if (canHeavyShelling)
        //{
            percentModifier = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.heavyShelling);
            //since most are a reduction and this is a dmg buff, i mius from 2 and multiply by difference.
             multiplyFodder = 1.0f;
            percentModifier = multiplyFodder - percentModifier;
        //print("percentModifier = " + percentModifier + ", and percent * tower dmg = " + (percentModifier * towerDmg));
            amountToAdd = (percentModifier * towerDmg);
            towerDmg += amountToAdd;
        //}
        //if (canSturdyTank)
        //{
            //print("Can heavy shelling, calculating");
            percentModifier = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.pressurizedTank);
            //since most are a reduction and this is a  buff, i mius from 2 and multiply by difference.
             multiplyFodder = 1.0f;
            percentModifier = multiplyFodder - percentModifier;
            amountToAdd = (percentModifier * TankAOERange);
            //do overlap sphere and range is the diameter?  then attack range could work easily.
            TankAOERange += amountToAdd;
        //}
        //throw new NotImplementedException();
    }

    public virtual void GetStringStats()
    {
        // this is overriden in each tower.
        TowerStatsTxt = "";
    }

    public string GetTowerStatsExplanation()
    {
        return TowerStatsTxt;
    }
    public string GetTypeExplanation()
    {
        return TowerTypeExplanation;
    }
    public string GetAugmentExplanation()
    {
        return TowerAugmentExplanation;
    }
    public string GetBaseExplanation()
    {
        print("I am in the tower. name: " + gameObject.name);
        return TowerBaseExplanation;
    }
    public string GetBaseFlavorTxt()
    {
        return TowerBaseFlavorTxt;
    }

    public void SetHead(Transform towerHead)
    {
        objectToPan = towerHead;
    }

    public virtual void DetermineTowerTypeBase(int towerInt)
    {
        // Just a basic overrriden function, same method signature though so I can work off 'Tower' script type.
    }
    public virtual void DetermineTowerHeadType(int towerInt)
    {
    }
    public virtual void DelayedStart()
    {
    }
}
