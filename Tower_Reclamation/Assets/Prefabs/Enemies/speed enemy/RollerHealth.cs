﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerHealth : EnemyHealth {

    public bool canDodge;
    protected float dodgeTimer = 0f;
	// Use this for initialization
	override protected void Start () {
        base.Start();
        canDodge = true;
        hitPointsMax = hitPointsMax * .65f;
        hitPoints = hitPointsMax;
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
        dodgeTimer += Time.deltaTime;
        if (!canDodge)
        {
            if (dodgeTimer > 6.0f)
            {
                canDodge = true;
            }
        }
	}


    override protected void ProcessHit(GameObject other)
    {
        float dmg = 0;
        if (canDodge)
        {
            // todo add dodge effect / sound / W/E
            canDodge = false;
            dodgeTimer = 0;
        }
        else
        {
            string towerName = "";
            dmg = other.GetComponentInParent<Tower>().Damage(ref towerName);
            Singleton.AddTowerDamage(towerName, dmg);
            hitPoints = hitPoints - dmg;
            healthImage.fillAmount = (hitPoints / hitPointsMax);
                
            hitparticleprefab.Play();
        }
        
        //    print("Current hit points are : " + hitPoints);
    }

}
