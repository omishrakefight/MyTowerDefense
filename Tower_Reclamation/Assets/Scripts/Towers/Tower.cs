using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour {

    public int goldCost = 60;

    // paramteres of each tower
    [SerializeField] public float attackRange = 9f;
    protected float currentAttackRange = 0;
    


    [SerializeField] public float towerDmg = 30;
    [SerializeField] protected float currentTowerDmg = 30;

    List<EnemyMovement> targets;
    [SerializeField] public Transform targetEnemy;
    [SerializeField] protected Transform objectToPan;
    public EnemyHealth targetEnemyBody;

    // for tinker upgrades
    public bool canSilverWiring = false;
    public bool canAlloyReasearch = false;
    public bool canSturdyTank = false;
    public bool canHeavyShelling = false;
    public bool canTowerEngineer = false;

    // Use this for initialization
    // Buff info
    public bool keepBuffed = false;

    protected virtual void Start()
    {
        
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
            attackRange += currentAttackRange * .3f;
            towerDmg += currentTowerDmg * .2f;
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

    protected Transform GetClosest(Transform transformA, Transform transformB)
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

    public virtual int GetTowerCost()
    {
        return 1000;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void CheckWhichUpgradesAreApplicable(ref float towerDmg, ref float TankAOERange)
    {
        Singleton singleton = FindObjectOfType<Singleton>();
        float percentModifier = 1.0f;
        float multiplyFodder = 1.0f;
        float amountToAdd = 0f;
        print("Checking if tower can be upgraded...");
        //if (canHeavyShelling)
        //{
            percentModifier = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.heavyShelling);
            //since most are a reduction and this is a dmg buff, i mius from 2 and multiply by difference.
             multiplyFodder = 1.0f;
            percentModifier = multiplyFodder - percentModifier;
            amountToAdd = (percentModifier * towerDmg);
            towerDmg += amountToAdd;
        //}
        //if (canSturdyTank)
        //{
            print("Can heavy shelling, calculating");
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
}
