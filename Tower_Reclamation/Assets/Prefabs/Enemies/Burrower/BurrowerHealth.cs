using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurrowerHealth : EnemyHealth {

    protected BurrowerMovement burrowerMove;
    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        burrowerMove = GetComponent<BurrowerMovement>();
        hitPoints = hitPoints * .5f;
        hitPointsMax = hitPoints;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }


    public void TellMovementToStartBurrow()
    {
        burrowerMove.IWasHit();
    }

    override public void HitByNonProjectile(float damage)
    {
        float dmg = damage;
        hitPoints = hitPoints - dmg;
        TellMovementToStartBurrow();
        hitparticleprefab.Play();

        if (hitPoints <= 0)
        {
            //Adds gold upon death, then deletes the enemy.
            KillsEnemyandAddsGold();
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(enemyHitAudio);
        }
    }

    override protected void OnParticleCollision(GameObject other)
    {
        ProcessHit(other);
        TellMovementToStartBurrow();
        if (hitPoints <= 0)
        {
            //Adds gold upon death, then deletes the enemy.
            KillsEnemyandAddsGold();
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(enemyHitAudio);
        }
    }

    override public IEnumerator Burning(float fireDmg)
    {
        if (hitPoints < 1)
        {
            KillsEnemyandAddsGold();
        }
        if (onFire && time < burnTime)
        {

            time += 1 * Time.deltaTime;
            hitPoints -= burnDmg * Time.deltaTime;
            TellMovementToStartBurrow();
        }
        else
        {
            onFire = false;
        }
        yield return new WaitForSeconds(1f);
    }

}
