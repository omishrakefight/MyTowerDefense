using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifledTower : Tower {

    // paramteres of each tower

    //[SerializeField] float attackRange = 32f;
    //public new int goldCost = 50;

    ParticleSystem projectileParticle = null;
    Singleton singleton;
    //[SerializeField] float towerDmg = 12;
    //[SerializeField] private float currentTowerDmg = 12;
    // State of tower

    
    // Buff info
    //bool keepBuffed = false;
    readonly new bool canSilverWiring = true;
    readonly new bool canAlloyReasearch = true;
    readonly new bool canSturdyTank = false;
    readonly new bool canHeavyShelling = true;
    readonly new bool canTowerEngineer = true;

    protected float notATankTower = 0f;


    override protected void Start () {
        base.Start();
        towerDmg = 9f;
        currentTowerDmg = 9f;
        currentAttackRange = attackRange;
        goldCost = (int)TowerCosts.RifledTowerCost;
        base.CheckWhichUpgradesAreApplicable(ref towerDmg, ref notATankTower);
        CheckAndApplyBuff();
        currentTowerDmg = towerDmg;
        currentAttackRange = attackRange;
	}

    override public void DelayedStart()
    {
        projectileParticle = GetComponentInChildren<ParticleSystem>();
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
            if (distanceToPreferedEnemy <= attackRange && targetEnemyBody.isTargetable)
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


    private void FireAtEnemy()
    {


        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);

        if (distanceToEnemy <= attackRange && targetEnemyBody.isTargetable)
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

    public override int GetTowerCost()
    {
        int towerCost = 0;

        towerCost = (int)TowerCosts.RifledTowerCost;
        singleton = FindObjectOfType<Singleton>();

        if (singleton.silverWiring)
        {
            towerCost = Mathf.RoundToInt(towerCost * (float)((int)TinkerUpgradePercent.mark1 / 100f));
        }

        return towerCost;
    }

}
