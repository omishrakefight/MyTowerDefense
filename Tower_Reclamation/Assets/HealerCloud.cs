using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerCloud : MonoBehaviour {


    float healPercent = 7.0f;

    float lifeTime = 4f;
    float counter = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if(counter > lifeTime)
        {
            Destroy(this);
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<EnemyHealth>())
        {
            other.gameObject.GetComponentInParent<EnemyHealth>().HealingBuffed(healPercent);
            //print("Feast upon my blood and heal thyself!!!!!!!!!!");
        }
    }

    public void SetHealFactor(float healPercent)
    {
        this.healPercent = healPercent;
    }
}
