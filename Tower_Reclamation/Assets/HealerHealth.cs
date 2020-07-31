using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerHealth : EnemyHealth {

	// Use this for initialization
	void Start () {
        base.Start();
        hitPoints = hitPoints * 1.1f;
        hitPointsMax = hitPoints;
        goldForMyHead = goldForMyHead * 2.1f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
