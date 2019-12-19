using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurrowerHealth : EnemyHealth {

    protected BurrowerMovement burrowerMove;
    protected bool burrowed = false;
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

    public void Burrowed()
    {
        burrowed = true;
    }
    public void Unburrowed()
    {
        burrowed = false;
    }

    public void TellMovementToStartBurrow()
    {
        burrowerMove.IWasHit();
    }

    override public void HitByNonProjectile(float damage)
    {
        if (burrowed) // cant shoot me im underground bitch.
        {
            return;
        }

        float dmg = damage;
        hitPoints = hitPoints - dmg;
        healthImage.fillAmount = (hitPoints / hitPointsMax);
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
        if (burrowed) // cant shoot me im underground bitch.
        {
            return;
        }

        ProcessHit(other);
        healthImage.fillAmount = (hitPoints / hitPointsMax);
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
        //burning underground sucks, but ill allow it ATM.
        if (hitPoints < 1)
        {
            KillsEnemyandAddsGold();
        }
        if (onFire && time < burnTime)
        {

            time += 1 * Time.deltaTime;
            hitPoints -= burnDmg * Time.deltaTime;
            TellMovementToStartBurrow();
            healthImage.fillAmount = (hitPoints / hitPointsMax);
        }
        else
        {
            onFire = false;
        }
        yield return new WaitForSeconds(1f);
    }

}
