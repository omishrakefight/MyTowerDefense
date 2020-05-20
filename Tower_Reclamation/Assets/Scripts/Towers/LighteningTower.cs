using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LighteningTower : Tower {

    // Todo try to make this a physics.OverlapSphere.
    //[SerializeField] public SphereCollider attackAOE;
    [SerializeField] public float chargeTime = 8f;
    [SerializeField] public float currentChargeTime = 0;
    public bool isCharged = false;
    Singleton singleton;
    bool reducedCost = false;
    //public new int goldCost = 80;

    [SerializeField] protected Light charge;
    [SerializeField] protected ParticleSystem projectileParticle;

    //paramteres of each tower
    //SphereCollider attackAOE;
    //float attackRange;
    //float chargeTime;
    //float currentChargeTime;
    //bool isCharged = false;

    //For tinker upgrades
    readonly new bool canSilverWiring = true;
    readonly new bool canAlloyReasearch = true;
    readonly new bool canSturdyTank = true;
    readonly new bool canHeavyShelling = false;
    readonly new bool canTowerEngineer = true;

    //Light charge;
    //ParticleSystem projectileParticle;
    //float towerDmg;
    //private float currentTowerDmg;
    //List<EnemyMovement> targets;

    // State of tower
    //[SerializeField] Transform targetEnemy;

    protected override void Start()
    {
        singleton = FindObjectOfType<Singleton>();
        if (singleton.silverWiring)
        {
            reducedCost = true;
        }
        attackRange = 10;

        // i neeed the initialization to ge tthe turret specific stats, just make another function in here that checks and modifies, it doesnth ave to be in towerfactory.
        towerDmg = 30;
        goldCost = (int)TowerCosts.LighteningTowerCost;

        if (!keepBuffed)   {    }


        if (keepBuffed)
        {
            // TODO make these additive, get the percent number, find it as an int (the difference) and add it to dmg so it wont double dip dmg boostes
            attackRange = attackRange * 1.3f;
            towerDmg = towerDmg * 1.2f;
            //attackAOE.radius = attackAOE.radius * 1.4f;
        }
        base.CheckWhichUpgradesAreApplicable(ref towerDmg, ref attackRange);
        currentTowerDmg = towerDmg;
    }

    //readonly bool canSilverWiring = true;
    //readonly bool canAlloyReasearch = true;
    //readonly bool canSturdyTank = true;
    //readonly bool canHeavyShelling = false;
    //readonly bool canTowerEngineer = true;
    //private void CheckWhichUpgradesAreApplicable(ref float towerDmg, ref float attackRange)
    //{
    //    float percentModifier = 1.0f;
    //    if (canHeavyShelling)
    //    {
    //        percentModifier = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.heavyShelling);
    //        //since most are a reduction and this is a dmg buff, i mius from 2 and multiply by difference.
    //        float multiplyFodder = 2.0f;
    //        percentModifier = multiplyFodder - percentModifier;
    //        float amountToAdd = (percentModifier * towerDmg);
    //        towerDmg += amountToAdd;
    //    }
    //    if(canSturdyTank)
    //    {
    //        percentModifier = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.pressurizedTank);
    //        //since most are a reduction and this is a  buff, i mius from 2 and multiply by difference.
    //        float multiplyFodder = 2.0f;
    //        percentModifier = multiplyFodder - percentModifier;
    //        float amountToAdd = (percentModifier * attackRange);
    //        //do overlap sphere and range is the diameter?  then attack range could work easily.
    //        attackRange += amountToAdd;
    //    }
    //    //throw new NotImplementedException();
    //}

    /// <summary>
    ///  *****************************************Change layer to ignore raycast fixes targetting bug.   ***************************** need to make sure i can click children though for tower upgrades.
    /// </summary>
    /// <param buttonName="targets"></param>


    //Waypoint baseWaypoint    For if i pass it here

    //void ExplosionDamage()
    //{
    //    //  10 is the enemy layer
    //    Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, attackRange, 11);
    //    int i = 0;
    //    while (i < hitColliders.Length && hitColliders.Length > 0)
    //    {
    //        try
    //        {
    //            EnemyHealth enemyHealth = null;
    //            print("new collider goes pow");
    //            i++;
    //            currentChargeTime = 0f;
    //            isCharged = false;
    //            if (hitColliders[i].GetComponentInParent<EnemyHealth>() != null)
    //            {
    //                enemyHealth = hitColliders[i].GetComponentInParent<EnemyHealth>();
    //            }
    //            else if (hitColliders[i].GetComponentInChildren<EnemyHealth>() != null)
    //            {
    //                enemyHealth = hitColliders[i].GetComponentInChildren<EnemyHealth>();
    //            }
    //            else
    //            {
    //                print(hitColliders[i].name);
    //                continue;
    //            }
    //            enemyHealth.HitByNonProjectile(towerDmg);
    //            if (enemyHealth.hitPoints < 1)
    //            {
    //                enemyHealth.KillsEnemyandAddsGold();
    //            }
    //        } catch(Exception e)
    //        {
    //            // nothing, the enemy may have died in the list.
    //        }
    //    }
    //}

    private void CheckEnemyRange(List<EnemyMovement> targets)
    {
        
        var sceneEnemies = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in sceneEnemies)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < attackRange)
            {
                targets.Add(enemy);
            }
        }
        //print(targets.Count);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isCharged)
        {
            List<EnemyMovement> targets = new List<EnemyMovement>();
            print("I am charged and enemies are nearby!!");
            CheckEnemyRange(targets);
            //var sceneEnemies = FindObjectsOfType<EnemyMovement>();
            for (int i = 0; i < targets.Count; i++)
            {
                try
                {
                    //print("POW");
                    targets[i].GetComponent<EnemyHealth>().hitPoints -= towerDmg;
                    targets[i].GetComponent<EnemyHealth>().RefreshHealthBar();

                    if (targets[i].GetComponent<EnemyHealth>().hitPoints < 1)
                    {
                        targets[i].GetComponent<EnemyHealth>().KillsEnemyandAddsGold();
                    }
                } catch (Exception e)
                {
                    //Do nothing, enemy may have died in this time (cant find it)
                }
            }
            currentChargeTime = 0;
            isCharged = false;
            targets.Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentChargeTime < chargeTime)
        {
            currentChargeTime += Time.deltaTime;
            charge.intensity = currentChargeTime / chargeTime;
        }
        else
        {
            isCharged = true;
            //ExplosionDamage();
        }

    }

    public override int GetTowerCost()
    {
        int towerCost = 0;

        towerCost = (int)TowerCosts.LighteningTowerCost;
        singleton = FindObjectOfType<Singleton>();

        if (singleton.silverWiring)
        {
            towerCost = Mathf.RoundToInt(towerCost * (float)((int)TinkerUpgradePercent.mark1 / 100f));
        }

        return towerCost;
    }

    new public void SetHead(Transform towerHead)
    {
        //Do nothing, this tower doesnt have a swivelHead so doesnt matter
    }


    /*
        private void FireAtEnemy()
        {
            float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
            if (distanceToEnemy <= attackRange)
            {
                Shoot(true);
            }
            else
            {
                Shoot(false);
                SetTargetEnemy();
            }
        }

        private void Shoot(bool isActive)
        {
            var emissionModule = projectileParticle.emission;
            emissionModule.enabled = isActive;
        }
        */
}
