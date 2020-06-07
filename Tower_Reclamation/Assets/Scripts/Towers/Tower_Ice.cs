using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Ice : Tower {

    [SerializeField] Light blueLight;
    public float range;
    protected float preFlippedChillAmount = 0f;
    protected float chillAmount = 0f;
    Singleton singleton;
    // Use this for initialization

    readonly new bool cantargettingModule = true;
    readonly new bool canAlloyReasearch = true;
    readonly new bool canSturdyTank = true;
    readonly new bool canHeavyShelling = false;
    readonly new bool canTowerEngineer = true;

    protected float notAProjectileTurret = 0f;

    override protected void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // maybe hard code it? try wihtout the 1/2
        ChillAura(this.transform.position, range);
    }

    public override void DelayedStart()
    {
        preFlippedChillAmount = .33f;
        range = blueLight.range;
        goldCost = (int)TowerCosts.SlowTowerCost;
        base.CheckUpgradesForTankTower(ref chillAmount, ref range);

        chillAmount = 1f - preFlippedChillAmount;
    }


    void ChillAura(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.GetComponentInParent<EnemyHealth>())
            {
                //hitColliders[i].SendMessage("AddDamage");
                hitColliders[i].gameObject.GetComponentInParent<EnemyMovement>().gotChilled(chillAmount);
                print("ive got something in my sights.");
            }
            i++;
        }
    }


    public override float GetTowerCost()
    {
        float towerCost = 0;
        singleton = Singleton.Instance;

        towerCost = (int)TowerCosts.SlowTowerCost;

        float percentToPay = singleton.GetPercentageModifier((int)TinkerUpgradeNumbers.alloyResearch);

        towerCost = towerCost * percentToPay;

        return towerCost;
    }

    new public void SetHead(Transform towerHead)
    {
        //Do nothing, this tower doesnt have a swivelHead so doesnt matter
    }
}
