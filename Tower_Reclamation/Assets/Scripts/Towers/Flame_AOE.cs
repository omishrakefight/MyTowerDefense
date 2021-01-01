using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame_AOE : MonoBehaviour {

    public float mortarExplosionDmg = 15f;
    public float towerDmg = 4;
    public float currentTowerDmg = 4;
    public float healReduction = 0f;
    protected Tower_Flame towerBase;
    string TowerTypeName;

    [SerializeField] float currentAttackRange;
    [SerializeField] float baseAttackRange;
    [SerializeField] float currentAttackWidth;
    [SerializeField] float baseAttackWidth;
    [SerializeField] CapsuleCollider flameAOE;

    [SerializeField] MortarShell projectile;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] ParticleSystem projectileParticleTwo;
    [SerializeField] ParticleSystem projectileParticleThree;

    readonly new bool cantargettingModule = true;
    readonly new bool canAlloyReasearch = true;
    readonly new bool canSturdyTank = true;
    readonly new bool canHeavyShelling = false;
    readonly new bool canTowerEngineer = true;

    bool keepBuffed = false;

    private int headType = -1;

    void Start()
    {
        TowerTypeName = "Flame Tower";
    }

    public void DelayedStart(bool keepBuffed, int _baseType, int _headType)
    {
        towerBase = GetComponentInParent<Tower_Flame>();
        headType = _headType;

        // this is used in the base flame class, not needed here.
        float filler = 0.0f;
        healReduction = 0f;
        towerDmg = 12;
        currentTowerDmg = 12;
        float rangeModifier = 1.0f;
        // 1 is shelling, 2 is tank.
        print(towerDmg + "  prebuff    " + rangeModifier);
        GetComponentInParent<Tower_Flame>().CheckUpgradesForTankTower(ref towerDmg, ref rangeModifier, ref filler);
        print(towerDmg + "  postbuff    " + rangeModifier);

        // this is defaulting values PRE tinker room buffs
        switch (headType)
        {
            case (int)FlameHead.Mortar:
                currentAttackRange = 30;
                baseAttackRange = 30;
                break;
            default:
                // this needs to happen
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
                break;
        }


        //This is tinker room buffs.
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
            currentAttackRange += baseAttackRange * .25f;
            currentAttackWidth += baseAttackWidth * .25f;
            currentTowerDmg = currentTowerDmg * 1.2f;
        }

        switch (headType)
        {
            // TODO make this fluctuate size
            case (int)FlameHead.Mortar:
                currentAttackRange = 30;
                baseAttackRange = 30;
                break;
            default:
                // increase the range based off tinker room / special tile buffs.
                flameAOE.height = currentAttackWidth;
                flameAOE.radius = currentAttackRange;
                break;
        }



        //after initial setup bonuses, set them equal at a 'base value' this way ingame values and resets work easily.
        baseAttackRange = currentAttackRange;
        baseAttackWidth = currentAttackWidth;
        towerBase.SetNewTowerDmg(currentTowerDmg);
    }

    public void BuffDamage(float dmgPercent)
    {
        currentTowerDmg += (towerDmg * dmgPercent);
    }

    public void SetHeadType(int type)
    {
        headType = type;
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
        towerBase.SetNewTowerDmg(currentTowerDmg);
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

        float halfedRange = (currentAttackRange / 2);
        return halfedRange; // Divide by 2 because the scale is .5
    }
    // i need to add a switch for the head toype, the flamethrower needs to flip range with width since its lengthwise

    public void TowerBuff()
    {
        // called by Tower_Flame.
        keepBuffed = true;
    }


    public float Damage(ref string towerName)
    {
        towerName = TowerTypeName;
        return currentTowerDmg;
    }

    public void Shoot(bool isActive)
    {
        switch (headType)
        {
            case (int)FlameHead.Basic:
                TurnFlameSprayActive(isActive);
                break;

            case (int)FlameHead.FlameThrower:
                TurnFlameSprayActive(isActive);
                break;

            case (int)FlameHead.Mortar:
                // using shoootmortar
                //Instantiate(projectile, this.transform.position, Quaternion.identity);
                //projectile.Instantiate(enemyTransform);
                break;

            default:
                break;
        }
    }

    public void ShootMortar(Transform enemyTransform)
    {
        MortarShell Projectile = Instantiate(projectile, this.transform.position, Quaternion.identity);
        Projectile.Instantiate(enemyTransform, 45, currentTowerDmg, TowerTypeName, 10);
    }

    public void TurnFlameSprayActive(bool isActive)
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
            other.GetComponentInParent<EnemyHealth>().CaughtFire(currentTowerDmg, healReduction);
        }
    }

    public float GetTowerRange()
    {
        switch (headType)
        {
            case (int)FlameHead.Basic:
                return currentAttackRange;

            case (int)FlameHead.FlameThrower:
                return (currentAttackRange / 2);

            case (int)FlameHead.Mortar:
                return (currentAttackRange);

            default:
                return currentAttackRange;
        }
    }

}
