using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Plasma : Tower {

    public float distanceToEnemyTest;
    public CapsuleCollider laser;
    [SerializeField] ParticleSystem spray;

    float chargeTime = 0f;
    bool canFire = false;

    //1.25 worked well
    float laserOnTime = .25f;
    float laserCurrentTime = 0f;
    bool laserIsOn = false;

    // Use this for initialization
    void Start () {
        base.Start();
        goldCost = 50;
        attackRange = 20;
        towerDmg = 20;
        //laser = transform.GetComponentInChildren<CapsuleCollider>();

	}

    // Update is called once per frame
    void Update()
    {
        if (!canFire)
        {
            chargeTime += 1 * Time.deltaTime;
            if (chargeTime > 5.0f)
            {
                canFire = true;
                chargeTime = 0f;
            }
        }

        if (laserIsOn)
        {
            //spray.emission.SetBurst(1);
            laserCurrentTime += 1 * Time.deltaTime;
            if (laserCurrentTime > laserOnTime)
            {
                laserIsOn = false;
                laserCurrentTime = 0f;
                canFire = false;
                laser.gameObject.SetActive(false);
                spray.Emit(10);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<EnemyHealth>())
        {
            //deal dmg instead.
            //other.GetComponentInParent<EnemyHealth>().CaughtFire(currentTowerDmg);
        }
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
        if (canFire)
        {
            laser.gameObject.SetActive(true);
            laserIsOn = true;
        }
    }
}
