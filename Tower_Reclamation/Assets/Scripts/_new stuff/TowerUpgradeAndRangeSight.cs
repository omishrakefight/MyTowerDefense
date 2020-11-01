using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    Tower _tower = null;

    [SerializeField] GameObject upgradePanel;

    [SerializeField] Text textInfo;

    [SerializeField] Text btnInfoOne;
    [SerializeField] Text btnInfoTwo;
    [SerializeField] Text btnInfoThree;

    [SerializeField] Button upgradeOne;
    [SerializeField] Button upgradeTwo;
    [SerializeField] Button upgradeThree; // I will have a function that takes in parameters like strings.  This will initialize the texts, buttons to what to do
    // This object is generic, the towers pass in the info to initialize / tell it what can be upgraded.

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;

        // have it use world space and get the objects worldspace.
        line.positionCount= (segments + 1);
        line.useWorldSpace = true;
        //CreatePoints();
        HideInfoPanel();
    }

    public void CreatePoints(Tower towerToDrawRangeAround )
    {
        line.enabled = true;
        radius =  towerToDrawRangeAround.GetAttackRange();
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

    public void DestroyRangeCircle()
    {
        line.enabled = false;
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitializeFromTower(Tower tower)
    {
        _tower = tower;
        string proxy1 = "", proxy2 = "", proxy3 = "", proxyInfo = "";
        tower.InitializeUpgradeOptionTexts(ref proxy1, ref proxy2, ref proxy3, ref proxyInfo);
        btnInfoOne.text = proxy1;
        btnInfoTwo.text = proxy2;
        btnInfoThree.text = proxy3;

        textInfo.text = proxyInfo;
        ShowInfoPanel();
    }

    public void HideInfoPanel()
    {
        upgradePanel.SetActive(false);
        _tower = null;
    }
    public void ShowInfoPanel()
    {
        upgradePanel.SetActive(true);
    }

    public void ButtonOne()
    {
        // add in here the ref tto button description....
        string proxyInfo = "";
        string buttonUpgradeInfo = "";
        _tower.UpgradeBtnOne(ref proxyInfo, ref buttonUpgradeInfo);
        textInfo.text = proxyInfo;
        btnInfoOne.text = buttonUpgradeInfo;
        ShowInfoPanel();
    }
    public void ButtonTwo()
    {
        string proxyInfo = "";
        _tower.UpgradeBtnTwo(ref proxyInfo);
        textInfo.text = proxyInfo;
        ShowInfoPanel();
    }
    public void ButtonThree()
    {
        string proxyInfo = "";
        _tower.UpgradeBtnThree(ref proxyInfo);
        textInfo.text = proxyInfo;
        ShowInfoPanel();
    }
}
