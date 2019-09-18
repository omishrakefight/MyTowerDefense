using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifledTower : Tower {

    // paramteres of each tower

    //[SerializeField] float attackRange = 32f;
    //public new int goldCost = 50;

    [SerializeField] ParticleSystem projectileParticle;
    //[SerializeField] float towerDmg = 12;
    //[SerializeField] private float currentTowerDmg = 12;

    // State of tower


    // Buff info
    //bool keepBuffed = false;

    override protected void Start () {
        base.Start();
        goldCost = 100;
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
}
