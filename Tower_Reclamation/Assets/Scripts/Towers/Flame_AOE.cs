using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame_AOE : MonoBehaviour {


    public float towerDmg = 4;
    public float currentTowerDmg = 4;

    [SerializeField] float currentAttackRange;
    [SerializeField] float baseAttackRange;
    [SerializeField] float currentAttackWidth;
    [SerializeField] float baseAttackWidth;
    [SerializeField] CapsuleCollider flameAOE;

    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] ParticleSystem projectileParticleTwo;
    [SerializeField] ParticleSystem projectileParticleThree;

    readonly new bool canSilverWiring = true;
    readonly new bool canAlloyReasearch = true;
    readonly new bool canSturdyTank = true;
    readonly new bool canHeavyShelling = false;
    readonly new bool canTowerEngineer = true;

    bool keepBuffed = false;

    void Start()
    {
        float doesntUse = 0f;
        float rangeModifier = 1.0f;
        // 1 is shelling, 2 is tank.
        GetComponentInParent<Tower_Flame>().CheckWhichUpgradesAreApplicable(ref doesntUse, ref rangeModifier);

        currentAttackRange = flameAOE.radius;
        baseAttackRange = flameAOE.radius;
        currentAttackWidth = flameAOE.height;
        baseAttackWidth = flameAOE.height;

        var particleLifetime = projectileParticle.main;
        particleLifetime.startLifetimeMultiplier = .5f;
        particleLifetime = projectileParticleTwo.main;

        particleLifetime.startLifetimeMultiplier = .5f;

        particleLifetime = projectileParticleThree.main;
        particleLifetime.startLifetimeMultiplier = .5f;

        //logic test
        print("_F The base range is " + currentAttackRange + " and the modifier bonus is " + rangeModifier);
        currentAttackRange = (currentAttackRange * rangeModifier);
        print("_F After buff the range is " + currentAttackRange);
        currentAttackWidth = (currentAttackWidth * rangeModifier);

        if (!keepBuffed)
        {
            currentAttackRange = flameAOE.radius;
        }
        else
        {
            //currentAttackRange = currentAttackRange * 1.3f;
            //currentAttackWidth = currentAttackWidth * 1.3f;

            //30% bonus to range
            currentAttackRange += baseAttackRange * .3f;
            currentAttackWidth += baseAttackWidth * .3f;
            currentTowerDmg = currentTowerDmg * 1.2f;
            flameAOE.height = currentAttackWidth;
            flameAOE.radius = currentAttackRange;
        }
        //after initial setup bonuses, set them equal at a 'base value' this way ingame values and resets work easily.
        baseAttackRange = currentAttackRange;
        baseAttackWidth = currentAttackWidth;
    }

    public void BuffRange(float rangeBuff)
    {

        currentAttackRange = flameAOE.radius;
        baseAttackRange = flameAOE.radius;
        currentAttackWidth = flameAOE.height;
        baseAttackWidth = flameAOE.height;

        //logic test
        print("buff Range: The base range is " + currentAttackRange + " and the modifier bonus is " + rangeBuff);
        currentAttackRange = (currentAttackRange * rangeBuff);
        print("buff Range: After buff the range is " + currentAttackRange);
        currentAttackWidth = (currentAttackWidth * rangeBuff);
        flameAOE.height = currentAttackWidth;
        flameAOE.radius = currentAttackRange;

        baseAttackRange = currentAttackRange;
        baseAttackWidth = currentAttackWidth;
    }

    public void TowerBuff()
    {
        // called by Tower_Flame.
        keepBuffed = true;
    }


    public float Damage()
    {
        return currentTowerDmg;
    }

    public void Shoot(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
        var emissionModuleTwo = projectileParticleTwo.emission;
        emissionModuleTwo.enabled = isActive;
        var emissionModuleThree = projectileParticleThree.emission;
        emissionModuleThree.enabled = isActive;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<EnemyHealth>())
        {
            other.GetComponentInParent<EnemyHealth>().CaughtFire(currentTowerDmg);
        }
    }

}
