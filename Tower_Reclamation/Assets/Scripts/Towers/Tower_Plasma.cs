using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Plasma : Tower
{

    public float distanceToEnemyTest;
    public CapsuleCollider laser;
    [SerializeField] ParticleSystem spray;
    List<EnemyHealth> targets = new List<EnemyHealth>();
    Tower_PlasmaHead plasmaTargeter;

    float maxCharge;
    float currentChargeTime = 0f;
    bool canFire = false;

    //1.25 worked well
    float laserOnTime = .25f;
    float laserCurrentTime = 0f;
    bool laserIsOn = false;

    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        maxCharge = 4f;
        goldCost = (int)TowerCosts.PlasmaTowerCost;
        attackRange = 30;
        towerDmg = 18;
        CheckAndApplyBuff();
        //laser = transform.GetComponentInChildren<CapsuleCollider>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!canFire)
        {
            currentChargeTime += 1 * Time.deltaTime;
            if (currentChargeTime > maxCharge)
            {
                canFire = true;
                currentChargeTime = 0f;
            }
        }

        if (laserIsOn)
        {
            //spray.emission.SetBurst(1);
            laserCurrentTime += 1 * Time.deltaTime;
            if (laserCurrentTime > laserOnTime)
            {
                //get list first, before turning off object.
                GetListOfEnemies();

                laserCurrentTime = 0f;
                canFire = false;
                laserIsOn = false;


                spray.Emit(10);

                HitEnemies();
                // hit then clear them
                targets.Clear();
                plasmaTargeter.ClearEnemies();
                laser.gameObject.SetActive(false);
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

    public void HitEnemies()
    {
        print(targets.Count + " enemies in list");
        foreach (EnemyHealth enemy in targets)
        {
            enemy.HitByNonProjectile(towerDmg);
        }
    }

    public void GetListOfEnemies()
    {
        plasmaTargeter = GetComponentInChildren<Tower_PlasmaHead>();

        targets = plasmaTargeter.getEnemies();
        print("targets " + targets.Count);
    }


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
        distanceToEnemyTest = distanceToEnemy;
    }

    private void Shoot(bool isActive)
    {
        if (canFire && isActive)
        {
            laser.gameObject.SetActive(true);
            laserIsOn = true;
        }
    }

    public override int GetTowerCost()
    {
        int towerCost = 0;

        towerCost = (int)TowerCosts.PlasmaTowerCost;
        return towerCost;
    }
}
