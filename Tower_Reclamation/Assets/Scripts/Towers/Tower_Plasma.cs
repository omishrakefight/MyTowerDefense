using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Plasma : Tower
{

    public float distanceToEnemyTest;
    public CapsuleCollider laser;
    [SerializeField] ParticleSystem spray;
    List<EnemyHealth> targetsList = new List<EnemyHealth>();
    Tower_PlasmaHead plasmaTargeter;
    LineRenderer lineRenderer;

    float crystalDmgInterval = .25f;
    float crystalCurrentBeamTime = 0f;
    float crystalCurrentChargeTime = 0f;
    private int crystalDMGPerStage = 3;
    int crystalBaseMaxDmg = 9;
    int crystalBeamLevel = 0;
    Transform targetLastShot = null;
    ParticleSystem beamParticles;
    float minTowerDmg = 15;
    float maxTowerDmg = 30f;

    float maxCharge;
    float currentChargeTime = 0f;
    bool canFire = false;

    //1.25 worked well
    float laserOnTime = .25f;
    float laserCurrentTime = 0f;
    bool laserIsOn = false;

    private int headType = 0;

    Singleton singleton;

    // Use this for initialization
    override protected void Start()
    {
        base.Start();

        //laser = transform.GetComponentInChildren<CapsuleCollider>();
        TowerTypeName = "Plasma Tower";
    }


    public override void DelayedStart()
    {
        TowerTypeExplanation = "The plasma tower is the only turret that has a strong enough hit to pierce the target completely.  " +
            "This heavy shot comes at a slight cost to accuracy: not enough to entirely miss the target, but it does make it a gamble on where you hit them, " +
            "and in direct correlation, how much damage the shot causes.";

        maxCharge = 4f;
        goldCost = (int)TowerCosts.PlasmaTowerCost;
        attackRange = 30;
        towerDmg = 18;
        base.CheckUpgradesForRifledTower(ref towerDmg, ref attackRange);
        CheckAndApplyBuff();
    }


    // Update is called once per frame
    void Update()
    {

        if (preferedEnemyBody != null && preferedEnemyBody != targetEnemyBody)
        {
            float distanceToPreferedEnemy = Vector3.Distance(preferedEnemyBody.gameObject.transform.position, gameObject.transform.position);
            if (distanceToPreferedEnemy <= attackRange && targetEnemyBody.isTargetable)
            {
                targetEnemyBody = preferedEnemyBody;
                targetEnemy = preferedEnemyBody.gameObject.transform;
            }
        }
        switch (headType)
        {
            case (int)PlasmaHead.Basic:
                TowerChargedShotAttacks();
                break;
            case (int)PlasmaHead.Crystal:
                if (targetEnemy)
                {
                    objectToPan.LookAt(targetEnemy);
                    FireAtEnemyWithCrystal();
                }
                else
                {
                    Shoot(false);
                    SetTargetEnemy();
                    // split tower, i have this same code twice...
                    //crystalBeamLevel = 0;
                    //crystalCurrentChargeTime = 0f;
                    //maxTowerDmg = crystalBaseMaxDmg;
                    //lineRenderer.widthMultiplier = 1.00f;
                    //beamParticles.enableEmission = false;
                }


                break;
        }
        
    }

    private void TowerChargedShotAttacks()
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
                targetsList.Clear();
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
        print(targetsList.Count + " enemies in list");
        foreach (EnemyHealth enemy in targetsList)
        {
            try
            {
                enemy.HitByNonProjectile(towerDmg, TowerTypeName);
            } catch(Exception e)
            {
                print("problem hitting the guy " + enemy.name);
                // nothing it may have died since being in the list.
            }
            
        }
    }


    public void GetListOfEnemies()
    {
        plasmaTargeter = GetComponentInChildren<Tower_PlasmaHead>();

        targetsList = plasmaTargeter.getEnemies();
        print("targets " + targetsList.Count);
    }


    public override void DetermineTowerTypeBase(int towerInt)
    {
        switch (towerInt)
        {
            case (int)PlasmaBase.Basic:
                //nothing, normal settings?
                TowerBaseExplanation = "Basic base.";
                break;
            //case (int)PlasmaBase.:
            //    TowerBaseExplanation = "Industrial base.";
            //    break;
            default:
                print("Default base, I am towerint of : " + towerInt);
                //nothing
                break;
        }
    }


    public override void DetermineTowerHeadType(int towerInt)
    {
        switch (towerInt)
        {
            case (int)PlasmaHead.Basic:
                headType = (int)PlasmaHead.Basic;
                TowerAugmentExplanation = "The default head of the Plasma Turret.  Hits in a line for randomised damage.";
                minTowerDmg = 30;
                maxTowerDmg = 90;
                //nothing;
                break;
            case (int)PlasmaHead.Crystal:
                // base is .25 ats so 4-12 DPS, maybe add 1 max per channel buff, ends at 4-20 dmg.  which is 8, 10, 12 DPS  but bad at target swapps.
                //Sounds balanced, avg is 12 DPS. but considering the ramp-up time and randomness, i think its good.
                // maybe make these a co-routine for stacks falling off.
                headType = (int)PlasmaHead.Crystal;
                crystalDmgInterval = .25f;
                lineRenderer = GetComponentInChildren<LineRenderer>();
                lineRenderer.SetPosition(0, (gameObject.transform.position + new Vector3(0, 5.5f, 0)));
                lineRenderer.useWorldSpace = true;
                beamParticles = GetComponentInChildren<ParticleSystem>();
                beamParticles.enableEmission = false;
                lineRenderer.enabled = false;
                TowerAugmentExplanation = "The crystal head of the Plasma Turret.  Amplifies the effects for a single target.";
                minTowerDmg = 3f;
                maxTowerDmg = crystalBaseMaxDmg; //3
                //nothing;
                break;
            default:
                TowerAugmentExplanation = "The default head of the Plasma Turret.  Hits in a line for randomised damage.";
                break;
        }
    }


    public override void GetStringStats()
    {
        TowerStatsTxt = "Plasma Tower Stats \n" +
            "Attack Range = " + attackRange + "\n" +
            "Attack Damage = " + towerDmg + " \n" +
            "Attack Speed = This Tower charges over " + maxCharge  + " seconds \n" +
            "Targetting = Piercing shot through target.";
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


    private void FireAtEnemyWithCrystal()
    {

        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange)
        {
            //Check if this is the same target as before.
            if (targetEnemy != targetLastShot)
            {
                targetLastShot = targetEnemy;

                //if I target swap, reset dmg and timers.
                crystalBeamLevel = 0;
                maxTowerDmg = crystalBaseMaxDmg;
                crystalCurrentChargeTime = 0f;
                lineRenderer.widthMultiplier = 1.00f;
                beamParticles.enableEmission = false;
            }

            //Shoot now
            ShootWithCrystal(true);
        }
        else
        {
            ShootWithCrystal(false);
            SetTargetEnemy();
        }
        distanceToEnemyTest = distanceToEnemy;
    }

    // a better way to swap this? so not setting true every frame?
    private void ShootWithCrystal(bool isActive)
    {
        if (isActive)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(1, (targetEnemy.transform.position));

            crystalCurrentBeamTime += (1 * Time.deltaTime);

            // 0, 1, 2;
            if(crystalBeamLevel < 2)
            {
                crystalCurrentChargeTime += (1 * Time.deltaTime);

                if ((int)crystalCurrentChargeTime != crystalBeamLevel)
                {
                    if (crystalCurrentChargeTime < 2.0f)
                    {
                        crystalBeamLevel++;
                        maxTowerDmg += crystalDMGPerStage;
                        lineRenderer.widthMultiplier = 1.30f;
                        // add in dmg increase, and in the target swap function restet both the beam level and the charge timer.
                    } else
                    {
                        crystalBeamLevel++;
                        maxTowerDmg += crystalDMGPerStage;
                        lineRenderer.widthMultiplier = 1.60f;
                    }
                }
            }
            if (crystalCurrentBeamTime > .25f)
            {
                crystalCurrentBeamTime = (crystalCurrentBeamTime % .25f);
                float towerDmg = UnityEngine.Random.Range(1, maxTowerDmg);
                //print("Plasma beam dmg = " + towerDmg);
                //TODO NEED TO CHANGE this needs to only get the enemy health on TARGET CHANGE way too process intensive to get 4 times a second.
                try
                {
                    targetEnemyBody.HitByNonProjectile(towerDmg, TowerTypeName); // .hitPoints -= towerDmg;
                }
                catch (Exception e)
                {
                    //Enemy died during sending dmg, no problem.
                }
                //Shifted this to the enemy to refresh health and give gold / die
                //targetEnemyBody.RefreshHealthBar();
                //if (targetEnemyBody.hitPoints < 1)
                //{
                //    try
                //    {
                //        targetEnemyBody.KillsEnemyandAddsGold();
                //    }
                //    catch (Exception ex)
                //    {
                //        print("Exception killing enemy  skipping now");
                //    }
                    
                //}
            }
            //finally look at the last point
            beamParticles.enableEmission = true;
            beamParticles.transform.position = targetEnemyBody.transform.position;
            beamParticles.transform.LookAt(this.transform);
            //laser.gameObject.SetActive(true);

        } else
        {
            lineRenderer.enabled = false;
            beamParticles.enableEmission = false;
        }
    }

    private void Shoot(bool isActive)
    {
        if (canFire && isActive)
        {
            laser.gameObject.SetActive(true);
            laserIsOn = true;
        }
    }


    public override float GetTowerCost()
    {
        float towerCost = 0;
        singleton = Singleton.Instance;

        towerCost = (int)TowerCosts.PlasmaTowerCost;

        float percentToPay = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.alloyResearch);

        towerCost = towerCost * percentToPay;

        return towerCost;
    }

}
