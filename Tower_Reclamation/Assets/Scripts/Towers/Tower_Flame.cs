using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Flame : Tower {

    // paramteres of each tower
    //[SerializeField] Transform objectToPan;
    //[SerializeField] float attackRange = 15f;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] ParticleSystem projectileParticleTwo;
    [SerializeField] ParticleSystem projectileParticleThree;

    Singleton singleton;
    // float particleLifetime;
    // float currentParticleLifetime;
    //public new int goldCost = 60;

    // State of tower
    //[SerializeField] Transform targetEnemy;

    // Buff info
    //bool keepBuffed = false;

    override protected void Start()
    {
        goldCost = (int)TowerCosts.FlameTowerCost;
        // nothing if it is unbuffed 
        if (!keepBuffed) { }
        else
        {
            var particleLifetime = projectileParticle.main;
            particleLifetime.startLifetimeMultiplier = .5f;

            particleLifetime = projectileParticleTwo.main;
            particleLifetime.startLifetimeMultiplier = .5f;

            particleLifetime = projectileParticleThree.main;
            particleLifetime.startLifetimeMultiplier = .5f;

            //.startLifetimeMultiplier;
            //  currentParticleLifetime = particleLifetime;
            // projectileParticle.main.startLifetimeMultiplier = currentParticleLifetime;

            attackRange = attackRange * 1.3f;
            GetComponentInChildren<Flame_AOE>().TowerBuff();

            keepBuffed = true;
        }
    }

    //  The actual Dmg applier is on the head of the turret with the capsul collider.


    //Waypoint baseWaypoint    For if i pass it here
    //public void TowerBuff()
    //{
       
    //}


    /*
     public void TowerUpgrade()
     {
         // Upgrade before multiplying
         baseAttackRange += 10;
         attackRange = baseAttackRange;

         if (keepBuffed)
         {
             TowerBuff();
         }
     }
     */

    // Update is called once per frame
    void Update () {
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy.position);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
            SetTargetEnemy();
        }
	}

    //private void SetTargetEnemy()
    //{
    //    var sceneEnemies = FindObjectsOfType<EnemyMovement>();
    //    if (sceneEnemies.Length == 0) { return; }

    //    Transform closestEnemy = sceneEnemies[0].transform;

    //    foreach (EnemyMovement testEnemy in sceneEnemies)
    //    {
    //        closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
    //    }

    //    targetEnemy = closestEnemy;
    //}
    
    //private Transform GetClosest(Transform transformA, Transform transformB)
    //{
    //    var distanceToA = Vector3.Distance(transform.position, transformA.position);
    //    var distanceToB = Vector3.Distance(transform.position, transformB.position);

    //    if (distanceToA <= distanceToB)
    //    {
    //        return transformA;
    //    }
    //    else
    //    {
    //        return transformB;
    //    }
    //}

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
        var emissionModuleTwo = projectileParticleTwo.emission;
        emissionModuleTwo.enabled = isActive;
        var emissionModuleThree = projectileParticleThree.emission;
        emissionModuleThree.enabled = isActive;
    }


    public override int GetTowerCost()
    {
        int towerCost = 0;

        towerCost = (int)TowerCosts.FlameTowerCost;
        singleton = FindObjectOfType<Singleton>();

        if (singleton.silverWiring)
        {
            towerCost = Mathf.RoundToInt(towerCost * (float)((int)TinkerUpgradePercent.mark1 / 100f));
        }

        return towerCost;
    }

}
