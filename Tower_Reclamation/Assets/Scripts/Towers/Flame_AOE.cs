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

    readonly new bool cantargettingModule = true;
    readonly new bool canAlloyReasearch = true;
    readonly new bool canSturdyTank = true;
    readonly new bool canHeavyShelling = false;
    readonly new bool canTowerEngineer = true;

    bool keepBuffed = false;

    void Start()
    {

    }

    public void DelayedStart(bool keepBuffed)
    {
        towerDmg = 12;
        currentTowerDmg = 12;
        float rangeModifier = 1.0f;
        // 1 is shelling, 2 is tank.
        print(towerDmg + "  prebuff    " + rangeModifier);
        GetComponentInParent<Tower_Flame>().CheckUpgradesForTankTower(ref towerDmg, ref rangeModifier);
        print(towerDmg + "  postbuff    " + rangeModifier);


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
            //currentAttackRange = flameAOE.radius;
        }
        else
        {
//  This needs to be moved to tower buff, might need to re-set the variables.  The problem here is that this does not get set to buffed until too late.
            //30% bonus to range
            currentAttackRange += baseAttackRange * .3f;
            currentAttackWidth += baseAttackWidth * .3f;
            currentTowerDmg = currentTowerDmg * 1.2f;
        }

        flameAOE.height = currentAttackWidth;
        flameAOE.radius = currentAttackRange;

        //after initial setup bonuses, set them equal at a 'base value' this way ingame values and resets work easily.
        baseAttackRange = currentAttackRange;
        baseAttackWidth = currentAttackWidth;
    }

    public void BuffRange(float rangeBuff)
    {
        Vector3 NewCapsulCenter;

        currentAttackRange = flameAOE.radius;
        baseAttackRange = flameAOE.radius;
        currentAttackWidth = flameAOE.height;
        baseAttackWidth = flameAOE.height;
        // CHANGE THIS TO +=? that way .30 works instead of = *x.  then the others is more consistent.
        //logic test
        print("buff Range: The base range is " + currentAttackRange + " and the modifier bonus is " + rangeBuff);
        currentAttackRange += (currentAttackRange * rangeBuff);
        print("buff Range: After buff the range is " + currentAttackRange);
        currentAttackWidth += (currentAttackWidth * rangeBuff);
        flameAOE.height = currentAttackWidth;
        flameAOE.radius = currentAttackRange;

        baseAttackRange = currentAttackRange;
        baseAttackWidth = currentAttackWidth;
        NewCapsulCenter = flameAOE.center;  //= (currentAttackWidth / 2);
        NewCapsulCenter.z = (currentAttackWidth / 2);
        flameAOE.center = NewCapsulCenter;
    }

    public void ChangeParticleTime(float timePercent)
    {

        var particleLifetime = projectileParticle.main;
        particleLifetime.startLifetimeMultiplier = (particleLifetime.startLifetimeMultiplier * timePercent);
        particleLifetime = projectileParticleTwo.main;

        particleLifetime.startLifetimeMultiplier = (particleLifetime.startLifetimeMultiplier * timePercent);

        particleLifetime = projectileParticleThree.main;
        particleLifetime.startLifetimeMultiplier = (particleLifetime.startLifetimeMultiplier * timePercent);
    }

    public float SetTowerTypeFlameThrower()
    {
        Vector3 NewCapsulCenter;

        currentAttackWidth = flameAOE.radius;
        baseAttackWidth = flameAOE.radius;
        currentAttackRange = flameAOE.height;
        baseAttackRange = flameAOE.height;
        //print("Flame current attack range = " + currentAttackRange);

        NewCapsulCenter = flameAOE.center;  //= (currentAttackWidth / 2);
        NewCapsulCenter.z = (currentAttackRange / 2);
        flameAOE.center = NewCapsulCenter;

        return (currentAttackRange / 2); // Divide by 2 because the scale is .5
    }
    // i need to add a switch for the head toype, the flamethrower needs to flip range with width since its lengthwise

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
