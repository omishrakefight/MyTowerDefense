using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelecter : MonoBehaviour
{

    [SerializeField] Dropdown towerBarrel;
    [SerializeField] Dropdown towerTurret;
    [SerializeField] Dropdown towerBase;

    Tower_Dmg newTower;
    Tower_Dmg decidedTower;
    [SerializeField] float turnSpeed = 6f;
    [SerializeField] Tower_Dmg rifledTowerPrefab;
    [SerializeField] Tower_Dmg flameTowerPrefab;
    [SerializeField] Tower_Dmg lightTowerPrefab;

    Vector3 towerPosition;
    Bounds bound;
    BoxCollider collider;

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            newTower = FindObjectOfType<Tower_Dmg>();

            //newTower.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);
            var dtx = Input.GetAxis("Mouse X") * turnSpeed;
            // var dty = Input.GetAxis("Mouse Y") * turnSpeed;
            var pivot = bound.center;

            newTower.transform.RotateAround(pivot, Vector3.up, dtx);
            // newTower.transform.RotateAround(pivot, Vector3.right, dty);
        }
    }

    public void ResetTowerPicture()
    {
        DestroyObject(newTower.gameObject);
        decidedTower = PickTower();
        newTower = Instantiate(decidedTower, towerPosition, Quaternion.identity);
        
        newTower.transform.localScale = new Vector3(.3f, .3f, .3f);
        print("summoned");
    }

    private Tower_Dmg PickTower()
    {
        if (towerTurret.value == 0)
        {
            print("Rifled Tower selected");
            decidedTower = rifledTowerPrefab;
        }

        if (towerTurret.value == 1)
        {
            print("Flame Turret selected");
            decidedTower = flameTowerPrefab;            
        }

        if (towerTurret.value == 2)
        {
            print("Light Turret selected");
            decidedTower = lightTowerPrefab;
        }


        return decidedTower;
    }
}
//towerBarrel.options.Clear();