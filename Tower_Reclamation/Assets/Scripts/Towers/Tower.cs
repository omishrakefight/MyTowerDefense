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
    [SerializeField] public Transform targetEnemy;
    [SerializeField] protected Transform objectToPan;
    public EnemyHealth targetEnemyBody;

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
        // nothing if it is unbuffed 
        if (!keepBuffed) { }
        else
        {
            attackRange = attackRange * 1.4f;
            currentTowerDmg = towerDmg * 1.2f;
        }
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
}
