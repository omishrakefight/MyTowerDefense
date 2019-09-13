using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublesHealth : EnemyHealth {

	// Use this for initialization
	override protected void Start () {
        base.Start();
        hitPoints = (.6f * hitPoints);
        hitPointsMax = hitPoints;
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
	}
}
