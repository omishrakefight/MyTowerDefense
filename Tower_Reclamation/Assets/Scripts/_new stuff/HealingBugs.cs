using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBugs : MonoBehaviour {


    /// <summary>
    /// IDEA!  Maybe he has increased movespeed, but slows down to the speed
    ///         of nearby enemies to walk with them.  On death does a healing cloud.
    ///         turn enemyhealth abstract and implement it with specialties.... or add
    ///         a specialty health thing... or a generic implementation.
    ///         
    ///     Maybe he does a heal for 2.5% max life each second, if enemy is full life then they get half of it as 'armor'
    ///     and then on death it does double that for 10 secs, so 5% each sec.
    ///     --- maybe stronger? .08?
    /// </summary>

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DeathHealAura();
    }

    float healPercent = .05f;

    void DeathHealAura()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 30f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.GetComponentInParent<EnemyHealth>())
            {
                hitColliders[i].gameObject.GetComponentInParent<EnemyHealth>().HealingBuffed(healPercent);
                //print("Feast upon my blood and heal thyself!!!!!!!!!!");
            }
            i++;
        }
    }
}
