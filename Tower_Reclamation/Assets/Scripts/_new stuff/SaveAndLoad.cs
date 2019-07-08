using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SaveAndLoad : MonoBehaviour {

     public bool[] towerList;

    SaveSerializedObject saver;
    PlayerTowerLog towerListObj;
    // create a new serializable object and then just import / export into it THEN serialize it here.
    // Use this for initialization
    void Start()
    {
        towerListObj = FindObjectOfType<PlayerTowerLog>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetReferences()
    {

        towerList = towerListObj.SaveTowers();

        saver = new SaveSerializedObject(towerListObj.SaveTowers());
        //saver = GetComponent<SaveSerializedObject>();

        saver.towerList = towerListObj.SaveTowers();

    }
    public void Save()
    {
        GetReferences();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/TowerInformation.dat");

        //PlayerTowerLog towersAvailable = new PlayerTowerLog();
        // initialize or w/e i want to do be4 sving

        bf.Serialize(file, saver); // this is whats serialized.
        file.Close();

    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        // ?????????????????????????????? openWrite?
        FileStream file = File.Open(Application.persistentDataPath + "/TowerInformation.dat", FileMode.Open);
        //SaveAndLoad towerLog = (SaveAndLoad)bf.Deserialize(file);
        //SaveSerializedObject towerLog = (SaveSerializedObject)bf.Deserialize(file);
        //bool[] bools = (bool[])bf.Deserialize(file);
        SaveSerializedObject f = (SaveSerializedObject)bf.Deserialize(file);
        towerListObj.LoadTowers(f.towerList);

        file.Close();
        //towerList = towerLog.towerList; // initializing off of new object.
        foreach (bool tower in f.towerList)
        {
            print(tower);
        }
        // initialize or create whatever wiht information now.
    }
}
