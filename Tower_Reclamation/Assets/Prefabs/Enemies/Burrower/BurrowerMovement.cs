using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurrowerMovement : EnemyMovement {

    public bool recentlyHit = false;
    public float hitCountdown = 1f;
    public float currentHitCountdown = 0f;

    public bool burrowed = false;
    public float currentBurrowTime = 0f;
    public float burrowTime = 2f;

    public float currentDiggingTime = 0f;
    public float digTime = .25f;

    private float distanceToBurrow = 2f;

    protected bool canBurrow = true;
    protected Vector3 currentDigSite;

    public bool canBeHit = true;
    public float trialSpeedForDigging;

    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        canBeHit = true;
        digTime = .35f;
        burrowTime = 2f;
        distanceToBurrow = 3f;
        trialSpeedForDigging = (distanceToBurrow * ((1/digTime) * Time.deltaTime));
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (recentlyHit && !burrowed)
        {
            currentHitCountdown += 1 * Time.deltaTime; // Add this gets set to 0 bool is false, and start burrow *************
        }
        if (burrowed)
        {
            currentBurrowTime += 1 * Time.deltaTime;
        }

        if (chilled)
        {
            StartCoroutine(Chilled(chilledMultiplier));
        }

        float enemySpeedASecond = enemySpeed * Time.deltaTime;

        //Burrow if timed, otherwise move.

        if (currentHitCountdown > 1.0f && currentDiggingTime < digTime)
        {
            print("failing to dig?");
            //currentHitCountdown = 0f;
            // add the time to burrowed here, then subtrat in unburrow.  great way to manage it and withotu an extra if loop
            currentDiggingTime += 1 * Time.deltaTime;
            if (canBurrow)
            {
                Burrow();
            }
            if (currentDiggingTime >= digTime)
            {
                burrowed = true;
                heightOffset.y -= distanceToBurrow;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, (new Vector3(transform.position.x, (currentDigSite.y - distanceToBurrow - distanceToBurrow), transform.position.z) + heightOffset), trialSpeedForDigging);

        }
        else if (currentBurrowTime > burrowTime && currentDiggingTime > 0)
        {
            //print(currentBurrowTime + " > " + burrowTime + " && " + currentDiggingTime);
            currentDiggingTime -= 1 * Time.deltaTime;
            if (burrowed)
            {
                Unburrow();
            }
            if (currentDiggingTime <= 0)
            {
                currentBurrowTime = 0;
                canBurrow = true;
                burrowed = false;
                canBeHit = true;
                heightOffset.y += distanceToBurrow;
            }
            transform.position = Vector3.MoveTowards(transform.position, (new Vector3(transform.position.x, (currentDigSite.y + distanceToBurrow + distanceToBurrow), transform.position.z) + heightOffset), trialSpeedForDigging);

        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, (path[currentPathNode + 1].transform.position + heightOffset), enemySpeedASecond);
        }

        if (transform.position == path[currentPathNode + 1].transform.position + heightOffset)
        {
            if (transform.position == path[path.Count - 1].transform.position + heightOffset)
            {
                GetComponent<EnemyHealth>().GotToEnd();
                FindObjectOfType<MyHealth>().AnEnemyFinishedThePath();
            }
            else
            {
                // increments the path node (go to next one) and turns them if need be.
                ++currentPathNode;
                if ((path[currentPathNode].transform.position - path[currentPathNode + 1].transform.position).x > 1f)
                {
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }
                if ((path[currentPathNode].transform.position - path[currentPathNode + 1].transform.position).x < -1f)
                {
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                }
                if ((path[currentPathNode].transform.position - path[currentPathNode + 1].transform.position).z > 1f)
                {
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
                }
                if ((path[currentPathNode].transform.position - path[currentPathNode + 1].transform.position).z < -1f)
                {
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                }

                if (path[currentPathNode + 1].isSlimed)
                {
                    slimeMultiplier = 1.4f;
                }
                else
                {
                    slimeMultiplier = 1.0f;
                }

                // chilled is 0?
                enemySpeed = enemyBaseSpeed * chilledMultiplier * frenzyMultiplier * slimeMultiplier;
                print(enemySpeed + "is speed   " + chilledMultiplier + frenzyMultiplier + slimeMultiplier);

            }
        }


    }

    public void IWasHit()
    {
        if (canBeHit) {
            recentlyHit = true;
            print("burrower says ouch, I gots to go.");
            canBeHit = false;
        }
    }

    public void Unburrow()
    {
        print("Unburrowing!");
        burrowed = false;
        currentHitCountdown = 0;
        currentDigSite = transform.position;
    }

    public void Burrow()
    {
        
        print("burrowing");
        //burrowed = true;
        recentlyHit = false;
        canBurrow = false;
        currentDigSite = transform.position;
        
    }
}
