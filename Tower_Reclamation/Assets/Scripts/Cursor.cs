using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    Raycasting raycasting;
	// Use this for initialization
	void Start () {
        raycasting = GetComponent<Raycasting>();
	}
	
	// Update is called once per frame
	void Update () {
        print(raycasting.LayerHit);
	}
}
