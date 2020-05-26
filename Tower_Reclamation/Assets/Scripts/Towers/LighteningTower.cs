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
    [SerializeField] protected SphereCollider AOERange;

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

    }

    public override void DelayedStart()
    {
        chargeTime = 9f;
        singleton = FindObjectOfType<Singleton>();
        if (singleton.silverWiring)
        {
            reducedCost = true;
        }
        attackRange = 18;

        // i neeed the initialization to ge tthe turret specific stats, just make another function in here that checks and modifies, it doesnth ave to be in towerfactory.
        towerDmg = 25;
        goldCost = (int)TowerCosts.LighteningTowerCost;

        if (!keepBuffed) { }


        if (keepBuffed)
        {
            // TODO make these additive, get the percent number, find it as an int (the difference) and add it to dmg so it wont double dip dmg boostes
            attackRange = attackRange * 1.3f;
            towerDmg = towerDmg * 1.2f;
            //attackAOE.radius = attackAOE.radius * 1.4f;
        }
        base.CheckWhichUpgradesAreApplicable(ref towerDmg, ref attackRange);
        currentTowerDmg = towerDmg;
        AOERange.radius = (attackRange * .60f);
    }


    public override void DetermineTowerTypeBase(int towerInt)
    {
        switch (towerInt)
        {
            case (int)LightningBase.Basic:
                //nothing, normal settings?
                break;
            case (int)LightningBase.Rapid:
                // alien base is +10%?
                print("Im doing rapid base");
                towerDmg = (towerDmg * .35f);
                currentTowerDmg = (currentTowerDmg * .35f);
                chargeTime = (chargeTime * .30f);
                //AOERange.radius = (AOERange.radius * .75f);
                attackRange = attackRange * .80f;
                break;
            default:
                print("Default base, I am towerint of : " + towerInt);
                //nothing
                break;
        }
        AOERange.radius = (attackRange * .60f);

    }

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
