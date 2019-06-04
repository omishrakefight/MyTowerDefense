using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float enemySpeed = 5.75f;

    [SerializeField] float enemyBaseSpeed = 5.75f;

    public bool chilled = false;
    public float chillTimer = 1f;
    public float timer = 0f;
    public float chilledMultiplier;

    int currentPathNode = 0;


    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GivePath();
        transform.position = path[0].transform.position;


        // StartCoroutine(FollowWaypoints(path));


    }


    // Update is called once per frame
    void Update()
    {
        if (chilled)
        {
            StartCoroutine(Chilled(chilledMultiplier));
        }

        List<Waypoint> path = FindNextNode();
        float enemySpeedASecond = enemySpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, path[currentPathNode + 1].transform.position, enemySpeedASecond);

        if (transform.position == path[currentPathNode + 1].transform.position)
        {
            if (transform.position == path[path.Count - 1].transform.position)
            {
                GetComponent<EnemyHealth>().GotToEnd();
                FindObjectOfType<MyHealth>().AnEnemyFinishedThePath();
            }
            else
            {
                ++currentPathNode;
            }
        }


    }

    private List<Waypoint> FindNextNode()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GivePath();
        return path;
    }

    public IEnumerator Chilled(float chilledMultiplier)
    {

        if (chilled && timer < chillTimer)
        {
            timer += 1 * Time.deltaTime;
            enemySpeed = enemyBaseSpeed * chilledMultiplier;
        }
        else
        {
            chilled = false;
            enemySpeed = enemyBaseSpeed;
        }

        yield return new WaitForSeconds(1f);
    }

    public void gotChilled(float chilledMultiplier)
    {
        chilled = true;
        this.chilledMultiplier = chilledMultiplier;
        timer = 0;
    }
}

//private void OnTriggerStay(Collider other)
//{
//    if (other.gameObject.GetComponentInParent<EnemyHealth>())
//    {
//        other.GetComponentInParent<EnemyHealth>().CaughtFire(currentTowerDmg);
//    }
//}


