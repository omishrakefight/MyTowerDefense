using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class SaveAndLoad : MonoBehaviour {

    // .dat = .data, not needed could be anything
    const string filePath = "TowerInformation.dat";
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + filePath);

        PlayerTowerLog towersAvailable = new PlayerTowerLog();
        // initialize or w/e i want to do be4 sving

        bf.Serialize(file, towersAvailable);
        file.Close();

    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        // ?????????????????????????????? openWrite?
        FileStream file = File.OpenWrite(Application.persistentDataPath + filePath + FileMode.Open);
        PlayerTowerLog towerLog = (PlayerTowerLog)bf.Deserialize(file);
        file.Close();

        // initialize or create whatever wiht information now.
    }
}
