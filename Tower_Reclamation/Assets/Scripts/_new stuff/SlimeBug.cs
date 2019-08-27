using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBug : MonoBehaviour {

    [SerializeField] Slime slime;

    float slimeMultiplier = 1.4f;
    // Use this for initialization
    void Start()
    {
        GetComponent<EnemyMovement>().willSlime = true;
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SpawnSlime(Vector3 oldLoc, Vector3 newLoc) // average them together for the inbetween
    {
        Vector3 slimeLocation = new Vector3(((oldLoc.x + newLoc.x) / 2), ((oldLoc.y + newLoc.y) / 2), ((oldLoc.z + newLoc.z) / 2));
        Instantiate(slime, slimeLocation, Quaternion.identity);
    }
}
