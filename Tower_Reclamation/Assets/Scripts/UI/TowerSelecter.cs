using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelecter : MonoBehaviour {

    [SerializeField] Dropdown towerBarrel;
    [SerializeField] Dropdown towerTurret;
    [SerializeField] Dropdown towerBase;

    Towers newTower;
    [SerializeField] float speed = 75f;
    [SerializeField] Towers rifledTowerPrefab;

    Vector3 towerPosition;
    Bounds bound;
    BoxCollider collider;

    // Use this for initialization
    void Start () {
        towerPosition = new Vector3(5.2f, -1f, -2.70f);
        towerBase.value = 0;
        towerTurret.value = 0;
        towerBarrel.value = 0;

        

		if (towerBarrel.value == 0 && towerTurret.value == 0 && towerBase.value == 0)
        {
            print("reading dropdown");
            newTower = Instantiate(rifledTowerPrefab, towerPosition, Quaternion.identity);
            newTower.transform.localScale = new Vector3(.3f, .3f, .3f);

        }
        collider = newTower.GetComponent<BoxCollider>();
        bound = collider.bounds;


    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            //newTower.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);
            var dtx = Input.GetAxis("Mouse X") * speed;
           // var dty = Input.GetAxis("Mouse Y") * speed;
            var pivot = bound.center;

            newTower.transform.RotateAround(pivot, Vector3.up, dtx);
           // newTower.transform.RotateAround(pivot, Vector3.right, dty);
            
        }
        
	}

}
