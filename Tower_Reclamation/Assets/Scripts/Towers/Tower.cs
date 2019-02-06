using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public int goldCost;

    // paramteres of each tower
    [SerializeField] public SphereCollider attackAOE;
    [SerializeField] public float attackRange = 10f;
    [SerializeField] public float chargeTime = 4f;
    [SerializeField] public float currentChargeTime = 0;
    public bool isCharged = false;

    [SerializeField] protected Light charge;
    [SerializeField] protected ParticleSystem projectileParticle;
    [SerializeField] public float towerDmg = 30;
    [SerializeField] protected float currentTowerDmg = 30;
    List<EnemyMovement> targets;

    // Use this for initialization
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

    // Update is called once per frame
    void Update () {
		
	}
}
