using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_PlasmaHead : MonoBehaviour {

    public CapsuleCollider laser;

    bool checkedInsideRange = false;
    List<EnemyHealth> enemies = new List<EnemyHealth>();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<EnemyHealth>())
        {
            if (!enemies.Contains(other.gameObject.GetComponentInParent<EnemyHealth>()))
            {
                print("found someone");
                enemies.Add(other.gameObject.GetComponentInParent<EnemyHealth>());
                print("added them");
            }
        }
    }
}
