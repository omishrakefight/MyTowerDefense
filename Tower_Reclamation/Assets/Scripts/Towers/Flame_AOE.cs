using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame_AOE : MonoBehaviour {


    [SerializeField] float towerDmg = 5;
    [SerializeField] private float currentTowerDmg = 5;

    [SerializeField] float currentAttackRange;
    [SerializeField] float currentAttackWidth;
    [SerializeField] CapsuleCollider flameAOE;

    bool keepBuffed = false;

    void Start()
    {
        if (!keepBuffed)
        {
            currentAttackRange = flameAOE.radius;

        }
    }

    public void TowerBuff()
    {
        currentAttackRange = flameAOE.radius;
        currentAttackWidth = flameAOE.height;

        currentTowerDmg = currentTowerDmg * 1.2f;
        currentAttackRange = currentAttackRange * 1.5f;
        currentAttackWidth = currentAttackWidth * 1.5f;
        flameAOE.height = currentAttackWidth;
        flameAOE.radius = currentAttackRange;

        keepBuffed = true;
    }


    public float Damage()
    {
        return currentTowerDmg;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<EnemyHealth>())
        {
            other.GetComponentInParent<EnemyHealth>().CaughtFire(currentTowerDmg);
        }
    }

}
