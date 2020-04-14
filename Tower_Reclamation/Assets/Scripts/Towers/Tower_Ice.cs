using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Ice : Tower {

    [SerializeField] Light blueLight;
    public float range;
    Singleton singleton;
    // Use this for initialization

    readonly new bool canSilverWiring = true;
    readonly new bool canAlloyReasearch = true;
    readonly new bool canSturdyTank = true;
    readonly new bool canHeavyShelling = false;
    readonly new bool canTowerEngineer = true;

    protected float notAProjectileTurret = 0f;

    override protected void Start()
    {
        range = blueLight.range;
        goldCost = (int)TowerCosts.SlowTowerCost;
        base.CheckWhichUpgradesAreApplicable(ref notAProjectileTurret, ref range);
    }

    // Update is called once per frame
    void Update()
    {
        // maybe hard code it? try wihtout the 1/2
        ChillAura(this.transform.position, range);
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
                hitColliders[i].gameObject.GetComponentInParent<EnemyMovement>().gotChilled(.5f);
                print("ive got something in my sights.");
            }
            i++;
        }
    }


    public override int GetTowerCost()
    {
        int towerCost = 0;

        towerCost = (int)TowerCosts.SlowTowerCost;
        singleton = FindObjectOfType<Singleton>();

        if (singleton.silverWiring)
        {
            towerCost = Mathf.RoundToInt(towerCost * (float)((int)TinkerUpgradePercent.mark1 / 100f));
        }

        return towerCost;
    }

    new public void SetHead(Transform towerHead)
    {
        //Do nothing, this tower doesnt have a swivelHead so doesnt matter
    }
}
