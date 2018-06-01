using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [Range(0.1f, 120f)]
    [SerializeField]
    float secondsBetweenSpawns = 2.00f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemiesLocation;
    [SerializeField] AudioClip enemySpawnAudio;

    public bool stillAlive = true;
    bool currentlySpawning = false;
    public int level = 0;
    int monstersSpawned = 0;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitBetweenWaves());
        //StartCoroutine(ContinualSpawnEnemies());
    }

    IEnumerator ContinualSpawnEnemies()
    {
        while (monstersSpawned < 5 && stillAlive)
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
        yield return StartCoroutine(WaitBetweenWaves());
    }

    IEnumerator WaitBetweenWaves()
    {
        yield return new WaitForSeconds(15);
        if (!currentlySpawning)
        {
            yield return StartCoroutine(ContinualSpawnEnemies());
        }
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !currentlySpawning)
        {
            StartCoroutine(ContinualSpawnEnemies());
        }
    }

}

// continue seperation add HP back to serialized field.




// bad for loop
//  monstersSpawned = 0; monstersSpawned < 5; monstersSpawned ++