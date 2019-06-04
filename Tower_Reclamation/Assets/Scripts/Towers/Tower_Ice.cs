using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Ice : Tower {

    [SerializeField] Light blueLight;
    public float test;
    // Use this for initialization
    void Start()
    {
        test = blueLight.range;
        goldCost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // maybe hard code it? try wihtout the 1/2
        ChillAura(this.transform.position, (blueLight.range));
    }


    void ChillAura(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.GetComponentInParent<EnemyHealth>())
            {
                //hitColliders[i].SendMessage("AddDamage");
                hitColliders[i].gameObject.GetComponentInParent<EnemyMovement>().gotChilled(.5f);
                print("ive got something in my sights.");
            }
            i++;
        }
    }
}
