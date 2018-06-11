using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighteningTower : MonoBehaviour {

    // paramteres of each tower
    [SerializeField] SphereCollider attackAOE;
    [SerializeField] float attackRange = 8f;
    [SerializeField] float baseAttackRange;
    [SerializeField] float chargeTime = 4f;
    [SerializeField] float currentChargeTime = 0;
    bool isCharged = false;

    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] float towerDmg = 30;
    [SerializeField] private float currentTowerDmg = 30;

    // State of tower
    [SerializeField] Transform targetEnemy;

    // Buff info
    bool keepBuffed = false;

    void Start()
    {
        if (!keepBuffed)
        {
            baseAttackRange = attackRange;
        }
    }

    public float Damage()
    {
        return currentTowerDmg;
    }

    //Waypoint baseWaypoint    For if i pass it here
    public void TowerBuff()
    {
        baseAttackRange = attackRange;
        attackRange = attackRange * 1.4f;
        currentTowerDmg = currentTowerDmg * 1.2f;

        keepBuffed = true;
    }



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


    // Update is called once per frame
    void Update()
    {
        if (currentChargeTime < chargeTime)
        {
            currentChargeTime += Time.deltaTime;
        }
        else
        {
            isCharged = true;
        }

        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (isCharged && distanceToEnemy <= attackRange)
        {
            var sceneEnemies = FindObjectsOfType<EnemyMovement>();
            if (sceneEnemies.Length == 0) { return; }
            foreach(EnemyMovement enemy in sceneEnemies)
            {
                // check for range and add to a list of targets for explosion.
            }

           // foreach (EnemyMovement enemy in attackRange)
        }
        else
        {
            SetTargetEnemy();
        }



        /*
        if (targetEnemy)
        {
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
            SetTargetEnemy();
        }
        */
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyMovement>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyMovement testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
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
