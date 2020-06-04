using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifledTower : Tower {

    // paramteres of each tower

    //[SerializeField] float attackRange = 32f;
    //public new int goldCost = 50;

    ParticleSystem projectileParticle = null;
    ParticleSystem.EmissionModule emission;
    Singleton singleton;
    //[SerializeField] float towerDmg = 12;
    //[SerializeField] private float currentTowerDmg = 12;
    // State of tower

    
    // Buff info
    //bool keepBuffed = false;
    readonly new bool cantargettingModule = true;
    readonly new bool canAlloyReasearch = true;
    readonly new bool canSturdyTank = false;
    readonly new bool canHeavyShelling = true;
    readonly new bool canTowerEngineer = true;

    protected float notATankTower = 0f;
    protected float minRange = 0f;

    // minrange = 0, set it if sniper?
    override protected void Start () {

        goldCost = (int)TowerCosts.RifledTowerCost;
       
	}

    override public void DelayedStart()
    {
        base.Start();
        minRange = 0f;
        towerDmg = 5f;
        currentTowerDmg = 5f;
        currentAttackRange = attackRange;
        base.CheckUpgradesForRifledTower(ref towerDmg, ref attackRange);
        CheckAndApplyBuff();
        currentTowerDmg = towerDmg;
        currentAttackRange = attackRange;
        projectileParticle = GetComponentInChildren<ParticleSystem>();
        emission = projectileParticle.emission;
    }

    public override void DetermineTowerTypeBase(int towerInt)
    {

        switch (towerInt)
        {
            case (int)RifledBase.Basic:
                //nothing, normal settings?
                break;
            case (int)RifledBase.Rapid:
                // alien base is +10%?
                print("Im doing rapid base");
                float changeAmount = 0;
                changeAmount = towerDmg * .15f;
                currentTowerDmg -= changeAmount;

                changeAmount = attackRange * .05f;
                currentAttackRange -= changeAmount;

                emission.rateOverTime = (emission.rateOverTime.constant * 1.3f);
                break;
            default:
                print("Default base, I am towerint of : " + towerInt);
                //nothing
                break;
        }
    }

    public override void DetermineTowerHeadType(int towerInt)
    {
        // base emissio nrate of 1 second
        switch (towerInt)
        {
            case (int)RifledHead.Basic:
                emission.rateOverTime = (emission.rateOverTime.constant * 2f);
                //nothing;
                break;
            case (int)RifledHead.Sniper:
                currentAttackRange = attackRange * 1.75f;
                minRange = attackRange / 3;
                emission.rateOverTime = (emission.rateOverTime.constant / 3.5f);
                towerDmg = towerDmg * 5;
                currentTowerDmg = towerDmg;
                break;
            default:
                emission.rateOverTime = (emission.rateOverTime.constant * 2f);
                break;
        }
    }
    //todo  check towerBuffs - is it in start? does it need a method? sync light tower, Tower.cs and others so its consistent.

    //Waypoint baseWaypoint    For if i pass it here
    //public void TowerBuff()
    //{
    //    attackRange = attackRange * 1.4f;
    //    currentTowerDmg = currentTowerDmg * 1.2f;

    //    keepBuffed = true;
    //}



    //public void TowerUpgrade()
    //{
    //   // attackRange = attackRange * (1.0 + .2 * timesBuffed)

    //   attackRange = attackRange * 1.2f;

    //    if (keepBuffed)
    //    {
    //        TowerBuff();
    //    }
    //}


    // Update is called once per frame
    void Update () {
        // add in here about having a priority target or something
        if (projectileParticle == null)
        {
            return; // not initiallized yet
        }


        if (preferedEnemyBody != null && preferedEnemyBody != targetEnemyBody)
        {
            float distanceToPreferedEnemy = Vector3.Distance(preferedEnemyBody.gameObject.transform.position, gameObject.transform.position);
            if ((distanceToPreferedEnemy <= currentAttackRange && targetEnemyBody.isTargetable) && (distanceToPreferedEnemy >= minRange))
            {
                print(preferedEnemyBody.gameObject.name);
                targetEnemyBody = preferedEnemyBody;
                targetEnemy = preferedEnemyBody.gameObject.transform;
            }
        }

        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
            SetTargetEnemy();
        }
	}

    override protected Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distanceToA = Vector3.Distance(transform.position, transformA.position);
        var distanceToB = Vector3.Distance(transform.position, transformB.position);

        // Before testing, if the current closest is too close, always forfeit it.
        if (distanceToA < minRange)
        {
            return transformB;
        }
        if (distanceToB < minRange)
        {
            return transformA;
        }

        // test ranges and get closest.
        if (distanceToA <= distanceToB)
        {
            return transformA;
        }
        else
        {
            return transformB;
        }
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);

        if (distanceToEnemy <= currentAttackRange && targetEnemyBody.isTargetable && distanceToEnemy >= minRange)
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

    public override float GetTowerCost()
    {
        float towerCost = 0;
        singleton = FindObjectOfType<Singleton>();

        towerCost = (int)TowerCosts.RifledTowerCost;

        float percentToPay = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.alloyResearch);

        towerCost = towerCost * percentToPay;

        return towerCost;
    }

}
