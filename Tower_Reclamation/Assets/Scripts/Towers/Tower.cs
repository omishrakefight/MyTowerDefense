using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour {

    public int goldCost = 60;

    // paramteres of each tower
    [SerializeField] public float attackRange = 10f;
    


    [SerializeField] public float towerDmg = 30;
    [SerializeField] protected float currentTowerDmg = 30;

    List<EnemyMovement> targets;
    [SerializeField] protected Transform targetEnemy;
    [SerializeField] protected Transform objectToPan;

    // Use this for initialization
    // Buff info
    public bool keepBuffed = false;

    protected virtual void Start()
    {
        // nothing if it is unbuffed 
        if (!keepBuffed)   {   }
        else
        {
            attackRange = attackRange * 1.4f;
            currentTowerDmg = currentTowerDmg * 1.2f;
        }
    }

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

    // returns how much dmg this tower does.
    public float Damage()
    {
        return currentTowerDmg;
    }


    protected void SetTargetEnemy()
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

    // Update is called once per frame
    void Update () {
		
	}
}
