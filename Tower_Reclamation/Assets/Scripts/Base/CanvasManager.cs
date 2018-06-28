using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {


    [SerializeField] Canvas computerBase;
    [SerializeField] Canvas turretFactory;
    [SerializeField] Canvas engineerer;
    [SerializeField] Canvas meetingRoom;
    [SerializeField] Canvas generalUpgrades;
    Canvas currentActive;

    // Use this for initialization
    void Start () {
        turretFactory.gameObject.SetActive(false);
        computerBase.gameObject.SetActive(false);
        engineerer.gameObject.SetActive(false);
        generalUpgrades.gameObject.SetActive(false);
        meetingRoom.gameObject.SetActive(true);
        currentActive = meetingRoom;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
