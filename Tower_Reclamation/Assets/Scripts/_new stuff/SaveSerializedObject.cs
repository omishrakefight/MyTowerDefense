using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SaveSerializedObject : MonoBehaviour {

    public bool[] towerList;

    public SaveSerializedObject(bool[] towerListSaves)
    {
        towerList = towerListSaves;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
