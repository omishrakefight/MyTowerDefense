using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LighteningTower : Tower {

    // Todo try to make this a physics.OverlapSphere.
    //[SerializeField] public SphereCollider attackAOE;
    [SerializeField] public float chargeTime = 8f;
    [SerializeField] public float currentChargeTime = 0;
    public bool isCharged = false;
    Singleton singleton;
    bool reducedCost = false;
    //public new int goldCost = 80;

    [SerializeField] protected Light charge;
    [SerializeField] protected ParticleSystem projectileParticle;
    [SerializeField] protected SphereCollider AOERange;

    List<EnemyMovement> targets;
    //paramteres of each tower
    //SphereCollider attackAOE;
    //float attackRange;
    //float chargeTime;
    //float currentChargeTime;
    //bool isCharged = false;

    //For tinker upgrades
    readonly new bool cantargettingModule = true;
    readonly new bool canAlloyReasearch = true;
    readonly new bool canSturdyTank = true;
    readonly new bool canHeavyShelling = false;
    readonly new bool canTowerEngineer = true;


    private GameObject target;
    private LineRenderer lineRend;

    private float arcLength = 1.25f;
    private float arcVariation = 1.25f;
    private float inaccuracy = 0.75f;
    private float timeOfZap = 0.35f;
    private float delayBetweenTargetJump = .10f;
    private float zapTimer;
    //Light charge;
    //ParticleSystem projectileParticle;
    //float towerDmg;
    //private float currentTowerDmg;
    //List<EnemyMovement> targets;

    // State of tower
    //[SerializeField] Transform targetEnemy;

    protected override void Start()
    {
        //ZapTarget(FindObjectOfType<EnemyMovement>().gameObject);
        lineRend = gameObject.GetComponent<LineRenderer>();
        zapTimer = 0;
        lineRend.SetVertexCount(1);
    }

    public override void DelayedStart()
    {
        TowerTypeExplanation = "The Lightning tower takes time storing ions.  When it is completely full, it starts sensing for nearby enemies. " +
            "Upon trigger, it releases the energy which arcs between all nearby enemies (which are oppositely charged) for massvie damage";


        chargeTime = 9f;
        singleton = Singleton.Instance;

        attackRange = 18;

        // i neeed the initialization to ge tthe turret specific stats, just make another function in here that checks and modifies, it doesnth ave to be in towerfactory.
        towerDmg = 75;
        //goldCost = (int)TowerCosts.LighteningTowerCost;

        if (!keepBuffed) { }


        if (keepBuffed)
        {
            // TODO make these additive, get the percent number, find it as an int (the difference) and add it to dmg so it wont double dip dmg boostes
            attackRange = attackRange * 1.3f;
            towerDmg = towerDmg * 1.2f;
            //attackAOE.radius = attackAOE.radius * 1.4f;
        }
        base.CheckUpgradesForTankTower(ref towerDmg, ref attackRange);
        currentTowerDmg = towerDmg;
        AOERange.radius = (attackRange * .60f);
    }

    public override void DetermineTowerHeadType(int towerInt)
    {
        switch (towerInt)
        {
            case (int)LightningHead.Basic:
                TowerAugmentExplanation = "The default tower module, with no modifiers.";
                //nothing;
                break;
            case (int)LightningHead.Static:

                TowerAugmentExplanation = "Tank time to max charge = +50% slower \n" +
                    "Tank charge bonus = +20% charge speed for each enemy nearby." +
                    "The Static augment puts less focus on storing electrical ions, and instead tries to harness it from the enemies.";

                break;
            default:
                TowerAugmentExplanation = "The default tower module, with no modifiers.";
                break;

        }
    }

    public override void DetermineTowerTypeBase(int towerInt)
    {
        switch (towerInt)
        {
            case (int)LightningBase.Basic:
                //nothing, normal settings?
                TowerBaseExplanation = "Basic base.";
                break;
            case (int)LightningBase.Rapid:
                float speedDecimalModifier = .30f;
                float damageDeimalModifier = .35f;
                float attackRangeDeimalModifier = .80f;


                print("Im doing rapid base");
                towerDmg = (towerDmg * .35f);
                currentTowerDmg = (currentTowerDmg * damageDeimalModifier);
                chargeTime = (chargeTime * speedDecimalModifier);
                //AOERange.radius = (AOERange.radius * .75f);
                attackRange = attackRange * attackRangeDeimalModifier;

                //TowerBaseExplanation = "Charge Speed = +" + ((int)((1 / speedDecimalModifier) * 100)).ToString() + "% \n";
                //TowerBaseExplanation = "Trigger Range = " + (AOERange.radius).ToString() + " \n";
                //TowerBaseExplanation = "Damage Range = -" + ((int)((1 - attackRange).ToString() + " \n";
                //TowerBaseExplanation += "Damage = -" + ((int)((1 - damageDeimalModifier) * 100)).ToString() + "% \n";
                TowerBaseExplanation = "Charge Speed = -" + (Mathf.RoundToInt((1 - speedDecimalModifier) * 100)).ToString() + "% \n";
                TowerBaseExplanation += "Trigger Range = " + (AOERange.radius).ToString() + " \n";
                TowerBaseExplanation += "Damage Range = -" + (Mathf.RoundToInt((1 - attackRangeDeimalModifier) * 100)).ToString() + "% \n";
                TowerBaseExplanation += "Damage = -" + (Mathf.RoundToInt((1 - damageDeimalModifier) * 100)).ToString() + "% \n";
                break;
            default:
                print("Default base, I am towerint of : " + towerInt);
                //nothing
                break;
        }
        AOERange.radius = (attackRange * .60f);

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
        //print(targets.Count);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isCharged)
        {
            //targets.Clear();
            targets = new List<EnemyMovement>();
            print("I am charged and enemies are nearby!!");
            CheckEnemyRange(targets);
            //var sceneEnemies = FindObjectsOfType<EnemyMovement>();
            for (int i = 0; i < targets.Count; i++)
            {
                try
                {
                    // Trigger lightning animation (targets available)
                    ZapTarget(other.gameObject);
                    //print("POW");
                    targets[i].GetComponent<EnemyHealth>().hitPoints -= towerDmg;
                    targets[i].GetComponent<EnemyHealth>().RefreshHealthBar();

                    if (targets[i].GetComponent<EnemyHealth>().hitPoints < 1)
                    {
                        targets[i].GetComponent<EnemyHealth>().KillsEnemyandAddsGold();
                    }
                } catch (Exception e)
                {
                    //Do nothing, enemy may have died in this time (cant find it)
                }
            }
            currentChargeTime = 0;
            isCharged = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentChargeTime < chargeTime)
        {
            currentChargeTime += Time.deltaTime;
            charge.intensity = currentChargeTime / chargeTime;
        }
        else
        {
            isCharged = true;
            //ExplosionDamage();
        }

        // Lightning line renderer

        //ZapTarget(FindObjectOfType<EnemyMovement>().gameObject);

        if (zapTimer > 0)
        {
            Vector3 lastPoint = transform.position;
            int i = 1;
            lineRend.SetPosition(0, transform.position);//make the origin of the LR the same as the transform
            foreach (EnemyMovement target in targets)
            {
                try
                {
                    while (Vector3.Distance(target.transform.position, lastPoint) > 3.0f)
                    {//was the last arc not touching the target?
                        lineRend.SetVertexCount(i + 1);//then we need a new vertex in our line renderer
                        Vector3 fwd = target.transform.position - lastPoint;//gives the direction to our target from the end of the last arc
                        fwd.Normalize();//makes the direction to scale
                        fwd = Randomize(fwd, inaccuracy);//we don't want a straight line to the target though
                        fwd *= UnityEngine.Random.Range(arcLength * arcVariation, arcLength);//nature is never too uniform
                        fwd += lastPoint;//point + distance * direction = new point. this is where our new arc ends
                        lineRend.SetPosition(i, fwd);//this tells the line renderer where to draw to
                        i++;
                        lastPoint = fwd;//so we know where we are starting from for the next arc
                    }
                    lineRend.SetVertexCount(i + 1);
                    lineRend.SetPosition(i, target.transform.position);
                    //lightTrace.TraceLight(gameObject.transform.position, target.transform.position);
                    zapTimer = zapTimer - Time.deltaTime;
                }
                catch (Exception)
                {
                    // nothing, enemy maybe died while bolt is still being drawn.
                }
            }
        }
        else
            lineRend.SetVertexCount(1);
    }




    public override void GetStringStats()
    {
        TowerStatsTxt = "Lightning Tower Stats \n" +
            "Attack Range = " + attackRange + "\n" +
            "Attack Damage = " + currentTowerDmg + "\n" +
            "Attack speed = " + chargeTime.ToString() + " second charge. \n" +
            "Damage Type = Lightning, instant. \n" +
            "Targetting = AOE centered on tower.";
    }

    public override float GetTowerCost()
    {
        float towerCost = 0;
        singleton = Singleton.Instance;

        towerCost = (int)TowerCosts.LighteningTowerCost;

        float percentToPay = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.alloyResearch);

        towerCost = towerCost * percentToPay;

        return towerCost;
    }

    new public void SetHead(Transform towerHead)
    {
        //Do nothing, this tower doesnt have a swivelHead so doesnt matter
    }



    //private LightningTrace lightTrace;


    // Use this for initialization
    //void Start()
    //{

    //    //lightTrace = gameObject.GetComponent<LightningTrace>();
    //}


    //-===================================================Lightning animation with line renderer
    // Update is called once per frame
    //void Update()
    //{

    //}

    private Vector3 Randomize(Vector3 newVector, float devation)
    {
        newVector += new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)) * devation;
        newVector.Normalize();
        return newVector;
    }

    public void ZapTarget(GameObject newTarget)
    {
        print("zap called");
        target = newTarget;
        zapTimer = timeOfZap;
    }

    //readonly bool cantargettingModule = true;
    //readonly bool canAlloyReasearch = true;
    //readonly bool canSturdyTank = true;
    //readonly bool canHeavyShelling = false;
    //readonly bool canTowerEngineer = true;
    //private void CheckWhichUpgradesAreApplicable(ref float towerDmg, ref float attackRange)
    //{
    //    float percentModifier = 1.0f;
    //    if (canHeavyShelling)
    //    {
    //        percentModifier = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.heavyShelling);
    //        //since most are a reduction and this is a dmg buff, i mius from 2 and multiply by difference.
    //        float multiplyFodder = 2.0f;
    //        percentModifier = multiplyFodder - percentModifier;
    //        float amountToAdd = (percentModifier * towerDmg);
    //        towerDmg += amountToAdd;
    //    }
    //    if(canSturdyTank)
    //    {
    //        percentModifier = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.pressurizedTank);
    //        //since most are a reduction and this is a  buff, i mius from 2 and multiply by difference.
    //        float multiplyFodder = 2.0f;
    //        percentModifier = multiplyFodder - percentModifier;
    //        float amountToAdd = (percentModifier * attackRange);
    //        //do overlap sphere and range is the diameter?  then attack range could work easily.
    //        attackRange += amountToAdd;
    //    }
    //    //throw new NotImplementedException();
    //}


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
