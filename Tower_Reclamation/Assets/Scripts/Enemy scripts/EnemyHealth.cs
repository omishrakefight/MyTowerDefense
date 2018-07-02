using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] Collider collisionMesh;

    [SerializeField] ParticleSystem hitparticleprefab;
    [SerializeField] ParticleSystem deathPrefab;
    [SerializeField] ParticleSystem endPrefab;
    [SerializeField] AudioClip enemyHitAudio;
    [SerializeField] AudioClip enemyDiedAudio;

    [SerializeField] public float hitPoints = 60;

    float burnTime = 3f;
    [SerializeField] bool onFire = false;
    [SerializeField] float time = 0;
    float burnDmg;

    // Use this for initialization
    void Start ()
    {
        float healthModifier = FindObjectOfType<MyScore>().waveCount * 15;
        hitPoints += healthModifier;
    }

    private void Update()
    {
        if (onFire)
        {
            StartCoroutine(Burning(burnDmg));
        }
    }

    public IEnumerator Burning(float fireDmg)
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

    private void OnParticleCollision(GameObject other)
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

    void ProcessHit(GameObject other)
    {
        float dmg = 0;
        dmg = other.GetComponentInParent<Tower_Dmg>().towerDMG();
        hitPoints = hitPoints - dmg;
        hitparticleprefab.Play();
        //    print("Current hit points are : " + hitPoints);
    }

    public void GotToEnd()
    {
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        endPrefab.Play();
        KillEnemy();
    }

    

}
