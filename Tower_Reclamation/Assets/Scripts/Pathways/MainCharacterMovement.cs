using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMovement : MonoBehaviour {

    [SerializeField] float enemySpeed = 1.48f;



    int currentPathNode = 0;


    void Start()
    {
        MainCharacterPathfinder pathFinder = GetComponent<MainCharacterPathfinder>();
        var path = pathFinder.GivePath();
        transform.position = path[0].transform.position;


        // StartCoroutine(FollowWaypoints(path));


    }


    // Update is called once per frame
    void Update()
    {
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
}
