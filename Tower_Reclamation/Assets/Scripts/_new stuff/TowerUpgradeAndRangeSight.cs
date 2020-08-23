using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TowerUpgradeAndRangeSight : MonoBehaviour {

    //Pro
    [Range(0, 100)]
    public int segments = 50;
    //[Range(0, 5)]
    //public float xradius = 5;
    //[Range(0, 5)]
    //public float zradius = 5;

    [Range(5, 25)]
    public float radius = 5;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        // have it use world space and get the objects worldspace.
        line.positionCount= (segments + 1);
        line.useWorldSpace = false;
        //CreatePoints();
    }

    public void CreatePoints(Tower towerToDrawRangeAround )
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        Vector3 towerTransform = towerToDrawRangeAround.transform.position;

        for (int i = 0; i < (segments + 1); i++)
        {
            //x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            //z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            x = ((Mathf.Sin(Mathf.Deg2Rad * angle) * radius) + towerTransform.x);
            z = ((Mathf.Cos(Mathf.Deg2Rad * angle) * radius) + towerTransform.z);

            line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / segments);
        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
