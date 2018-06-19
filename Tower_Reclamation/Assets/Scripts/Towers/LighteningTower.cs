using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighteningTower : MonoBehaviour {

    // paramteres of each tower
    [SerializeField] SphereCollider attackAOE;
    [SerializeField] float attackRange = 10f;
    [SerializeField] float chargeTime = 4f;
    [SerializeField] float currentChargeTime = 0;
    bool isCharged = false;

    [SerializeField] Light charge;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] float towerDmg = 30;
    [SerializeField] private float currentTowerDmg = 30;
    List<EnemyMovement> targets;

    // State of tower
    [SerializeField] Transform targetEnemy;

    // Buff info
    bool keepBuffed = false;

    void Start()
    {
        if (!keepBuffed)
        {
        }
        if (keepBuffed)
        {
            attackRange = attackRange * 1.4f;
            currentTowerDmg = currentTowerDmg * 1.2f;
            attackAOE.radius = attackAOE.radius * 1.4f;
        }
    }

    public float Damage()
    {
        return currentTowerDmg;
    }

    //Waypoint baseWaypoint    For if i pass it here
    public void TowerBuff()
    {
        keepBuffed = true;
    }



    public void TowerUpgrade()
    {
        // attackRange = attackRange * (1.0 + .2 * timesBuffed)

        attackRange = attackRange * 1.2f;

        if (keepBuffed)
        {
            TowerBuff();
        }
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
        print(targets.Count);
    }

    private void OnTriggerStay(Collider other)
    {
 
        if (isCharged)
        {
            List<EnemyMovement> targets = new List<EnemyMovement>();
            print("I am charged and enemies are nearby!!");
            CheckEnemyRange(targets);
            var sceneEnemies = FindObjectsOfType<EnemyMovement>();
            for (int i = 0; i < targets.Count; i++)
            {
                print("POW");
                targets[i].GetComponent<EnemyHealth>().hitPoints -= towerDmg;
                if (targets[i].GetComponent<EnemyHealth>().hitPoints < 1)
                {
                    targets[i].GetComponent<EnemyHealth>().KillsEnemyandAddsGold();
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
            charge.intensity = currentChargeTime / 3.33f;
        }
        else
        {
            isCharged = true;
        }

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
