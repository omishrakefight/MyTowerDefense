using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveAndLoad : MonoBehaviour {

     public bool[] towerList;

    SaveSerializedObject saver;
    PlayerTowerLog towerListObj;
    Singleton singleton;
    [SerializeField] ChooseNextMissionPath missionChoice;
    // create a new serializable object and then just import / export into it THEN serialize it here.
    // Use this for initialization
    void Start()
    {
        //missionChoice = FindObjectOfType<ChooseNextMissionPath>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetReferences()
    {

        towerListObj = GameObject.FindGameObjectWithTag("TowerInfo").GetComponentInChildren<PlayerTowerLog>();// FindObjectOfType<PlayerTowerLog>();
        singleton = FindObjectOfType<Singleton>();
        print(GameObject.FindGameObjectWithTag("TowerInfo"));

        towerList = towerListObj.SaveTowers();

        saver = new SaveSerializedObject();

        saver.SaveTowers(towerListObj.SaveTowers());
        saver.IsHasChosenATower(singleton.isHasLearnedATower);
        // need to convert back to a list when reading in. ?
        // ERROR IS BEACAUSE THE SCRIPT IS DISABLED IN SAVE WINDOW, THE CANVASSES ARE ALL DISABLED EXCEPT USED ONE.
        saver.SaveEnemyOptions(missionChoice.firstEnemySet.ToArray(), missionChoice.secondEnemySet.ToArray());

        if (missionChoice.isHasChosen)
        {
            saver.IsHasChosenEnemies(true);
            saver.SaveEnemiesChosen(singleton.enemyList.ToArray());
        }
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
        try
        {
            FileStream file = File.Open(Application.persistentDataPath + "/TowerInformation.dat", FileMode.Open);
            try
            {
                print(GameObject.FindGameObjectWithTag("TowerInfo"));

                var x = GameObject.FindGameObjectWithTag("TowerInfo");
                towerListObj = x.GetComponentInChildren<PlayerTowerLog>();
                print(x.GetComponentInChildren<PlayerTowerLog>());

                BinaryFormatter bf = new BinaryFormatter();
                // ?????????????????????????????? openWrite?
                

                SaveSerializedObject f = (SaveSerializedObject)bf.Deserialize(file);
                print(towerListObj);
                towerListObj.LoadTowers(f.towerList);
                missionChoice.LoadPathChoices(f.enemyOption1List, f.enemyOption2List);
                singleton.isHasLearnedATower = f.hasChosenATower;


                // ---- they can choose new path each time for now.
                //if (f.hasChosenEnemies)
                //{
                //    singleton.LoadEnemyList(f.enemyList);
                //    //missionChoice.   set the has chosen a path variable
                //    //-----------^^^^^^^^
                //}


                //towerList = towerLog.towerList; // initializing off of new object.
                foreach (bool tower in f.towerList)
                {
                    print(tower);
                }
                // initialize or create whatever wiht information now.

                // load base location
                SceneManager.LoadSceneAsync("_Scenes/_Base");
            }
            catch (Exception e)
            {
                print("Failed! " + e.Message);
            }
            finally
            {
                file.Close();
            }
        }
        catch(Exception z)
        {
            print("couldn't open file");
        }
    }
}
