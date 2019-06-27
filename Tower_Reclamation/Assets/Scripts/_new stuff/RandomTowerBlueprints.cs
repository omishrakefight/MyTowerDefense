using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTowerBlueprints : MonoBehaviour {

    private int amountOfTowers = 3;
    private int amountOfUndiscoveredTowers = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PickTowers();
    }

    public void PickTowers()
    {
        print(Random.Range(0, amountOfUndiscoveredTowers));
    }

}
