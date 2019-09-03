using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerMovement : EnemyMovement {

    // Use this for initialization
    override protected void Start () {
        base.Start();
        enemyBaseSpeed = 8f;
	}

    // Update is called once per frame
    override protected void Update () {
        base.Update();
	}
}
