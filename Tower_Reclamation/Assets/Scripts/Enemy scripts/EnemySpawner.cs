using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    [Range(0.1f, 120f)]
    [SerializeField]
    float secondsBetweenSpawns = 2.00f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemiesLocation;
    [SerializeField] AudioClip enemySpawnAudio;
    [SerializeField] Text win;

    public bool stillAlive = true;
    bool currentlySpawning = false;
    public int level = 1;
    int monstersSpawned = 0;

    float timeBetweenWaves = 12f;
    float waveTimer;
    [SerializeField] Slider slider;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitBetweenWaves());
        slider.maxValue = timeBetweenWaves;
        win.enabled = false;
    }

    IEnumerator ContinualSpawnEnemies()
    {
        // resets the timer when a wave is spawned.
        waveTimer = 0;

        while (monstersSpawned < 6 && stillAlive && level < 5)
        {
            currentlySpawning = true;
            var enemySpawnLoc = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemySpawnLoc.transform.parent = enemiesLocation;
            monstersSpawned++;
            FindObjectOfType<MyScore>().ScoreUpOne();
            GetComponent<AudioSource>().PlayOneShot(enemySpawnAudio);
            yield return new WaitForSeconds(secondsBetweenSpawns);

        }
        currentlySpawning = false;
        ++level;
        monstersSpawned = 0;
        if (level == 5)
        {
            while(FindObjectsOfType<EnemyMovement>().Length > 0)
            {
                yield return new WaitForSeconds(1);
            }
            if(stillAlive)
                win.enabled = true;
        }
        
        yield return StartCoroutine(WaitBetweenWaves());
    }

    IEnumerator WaitBetweenWaves()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        if (!currentlySpawning)
        {
            yield return StartCoroutine(ContinualSpawnEnemies());
        }
    }

    
    void Update()
    {
        if (stillAlive && !currentlySpawning)
        {
            waveTimer += 1 * Time.deltaTime;
        }
        slider.value = waveTimer;


        if (Input.GetKeyDown(KeyCode.Space) && !currentlySpawning)
        {
            waveTimer = 0;
            StartCoroutine(ContinualSpawnEnemies());
        }
    }

}

// continue seperation add HP back to serialized field.




// bad for loop
//  monstersSpawned = 0; monstersSpawned < 5; monstersSpawned ++