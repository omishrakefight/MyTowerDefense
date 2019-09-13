using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyHealth : MonoBehaviour {

    [SerializeField] protected Collider collisionMesh;

    [SerializeField] protected ParticleSystem hitparticleprefab;
    [SerializeField] protected ParticleSystem deathPrefab;
    [SerializeField] protected ParticleSystem endPrefab;
    [SerializeField] protected AudioClip enemyHitAudio;
    [SerializeField] protected AudioClip enemyDiedAudio;

    [SerializeField] public float hitPoints = 60;
    [SerializeField] public float hitPointsMax;

    protected float burnTime = 3f;
    [SerializeField] protected bool onFire = false;
    [SerializeField] protected float time = 0;
    protected float burnDmg;

    protected bool healing = false;
    protected float healTimer = 1f;
    protected float healTime = 0f;
    protected float healPercent;

    // Use this for initialization
    protected virtual void Start()
    {
        float healthModifier = FindObjectOfType<CurrentWave>().waveCount * 15;
        hitPoints += healthModifier;
        hitPointsMax = hitPoints;
    }

    protected virtual void Update()
    {
        if (onFire)
        {
            StartCoroutine(Burning(burnDmg));
        }
        if (healing)
        {
            StartCoroutine(Healing(healPercent));
        }
    }

    public virtual IEnumerator Burning(float fireDmg)
    {
        if (hitPoints < 1)
        {
            KillsEnemyandAddsGold();
        }
        if (onFire && time < burnTime)
        {

            time += 1 * Time.deltaTime;
            hitPoints -= burnDmg * Time.deltaTime;
        }
        else
        {
            onFire = false;
        }
        yield return new WaitForSeconds(1f);
    }

    public void CaughtFire(float fireDmg)
    {
        onFire = true;
        time = 0;
        burnDmg = fireDmg;
    }

    protected virtual void OnParticleCollision(GameObject other)
    {
        ProcessHit(other);
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

    public void KillsEnemyandAddsGold()
    {
        FindObjectOfType<GoldManagement>().AddGold();

        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(enemyDiedAudio, Camera.main.transform.position);
        KillEnemy();
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }

    protected virtual void ProcessHit(GameObject other)
    {
        float dmg = 0;
        dmg = other.GetComponentInParent<Tower_Dmg>().towerDMG();
        hitPoints = hitPoints - dmg;
        hitparticleprefab.Play();
            print("Current hit points are : " + hitPoints);
    }

    public virtual void HitByNonProjectile(float damage)
    {
        float dmg = damage;
        hitPoints = hitPoints - dmg;
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

    public void HealingBuffed(float healPercent)
    {
        this.healPercent = healPercent;
        healTime = 0f;
        healing = true;
        //float healPerTick = (healPercent * hitPoints);

    }

        public IEnumerator Healing(float healPercent)
    {
        float healPerTick = (healPercent * hitPointsMax);
        //print("HPT: " + healPerTick + " HPerc: " + healPercent + " HPM: " + hitPointsMax);

        print("trying to heal...");
        if (healing && healTime < healTimer)
        {
            time += 1 * Time.deltaTime;
            // if he is full or more its hald effective as armor, otherwise full heal.
            if (hitPoints >= hitPointsMax)
            {
                hitPoints += (healPerTick * Time.deltaTime) / 2;
            }
            else
            {
                hitPoints += (healPerTick * Time.deltaTime);
            }

            //print("I healed " + healPerTick + " HP!" + "   | healPercent: " + healPercent);
        }
        else
        {
            healing = false;
        }
        yield return new WaitForSeconds(1f);
    }

    public void GotToEnd()
    {
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        endPrefab.Play();
        KillEnemy();
    }




}
