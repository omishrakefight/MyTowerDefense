using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBombBurnAOE : MonoBehaviour {

    float burnDmg = 0f;
    float healReduction = 0f;
    float lifeCycle = 2f;
    float currentTime = 0f;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if (currentTime > lifeCycle)
        {
            Destroy(this.gameObject);
        }
	}

    public void Initialize(float _burnDmg, float sizeIncrease)
    {
        burnDmg = _burnDmg;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<EnemyHealth>())
        {
            other.GetComponentInParent<EnemyHealth>().CaughtFire(burnDmg, healReduction);
        }
    }
}
