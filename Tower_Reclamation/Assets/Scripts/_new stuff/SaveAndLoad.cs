using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveAndLoad : MonoBehaviour {
    private const string TagForBase = "Base Rooms Obj";
    public bool[] towerList;

    bool finishedLoading = false;
    SaveSerializedObject saver;
    PlayerTowerLog towerListObj;
    Singleton singleton;
    TinkerUpgrades tinkerUpgrades;
    //[SerializeField] ChooseNextMissionPath missionChoice;
    ChooseNextMissionPath missionChoice;

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
        missionChoice = GameObject.FindGameObjectWithTag(TagForBase).GetComponentInChildren<ChooseNextMissionPath>();
        towerListObj = GameObject.FindGameObjectWithTag(TagForBase).GetComponentInChildren<PlayerTowerLog>();// FindObjectOfType<PlayerTowerLog>();
        singleton = FindObjectOfType<Singleton>();
        tinkerUpgrades = FindObjectOfType<TinkerUpgrades>();
        //print(GameObject.FindGameObjectWithTag("TowerInfo"));

        towerList = towerListObj.SaveTowers();

        saver = new SaveSerializedObject();

        saver.SaveTowers(towerListObj.SaveTowers());
        saver.IsHasChosenATower(singleton.isHasLearnedATower);
        // need to convert back to a list when reading in. ?
        // ERROR IS BEACAUSE THE SCRIPT IS DISABLED IN SAVE WINDOW, THE CANVASSES ARE ALL DISABLED EXCEPT USED ONE.
        saver.SaveEnemyOptions(missionChoice.firstEnemySet.ToArray(), missionChoice.secondEnemySet.ToArray());

        saver.SaveTinkerRoomInfo(tinkerUpgrades.SaveCurrentUpgradeLevels(), tinkerUpgrades.SaveLearnableUpgrades(), tinkerUpgrades.SavePossibleOptions(), tinkerUpgrades.SaveHasPicked());  

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

    public void LoadSavedBase()
    {
        finishedLoading = false;
        IEnumerator load;
        load = LoadFromFile(false);

        StartCoroutine(load);
        if (finishedLoading) {
            StopCoroutine(load);
            print("shutdown!");
        }
    }

    public void LoadNewBase()
    {
        finishedLoading = false;
        IEnumerator load;
        load = LoadFromFile(true);

        StartCoroutine(load);
        if (finishedLoading)
        {
            StopCoroutine(load);
            print("shutdown!");
        }
    }

    public IEnumerator LoadFromFile(bool newBase)
    {
        // This is done in 3 steps, first, load file and initialize the singleton.
        // 2nd is to load base (singleton already has loaded the setting base needs)
        // 3rd is to do all the loading that requires the base active.

        SaveSerializedObject savedFile = null;
        FileStream file = null;
        try
        {
            file = File.Open(Application.persistentDataPath + "/TowerInformation.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            savedFile = (SaveSerializedObject)bf.Deserialize(file);

            if (!newBase)
            {
                singleton.isHasLearnedATower = savedFile.hasChosenATower;
            }
            
        }
        catch (Exception e1)
        {
            print(e1.Message);
        }
        finally
        {
            file.Close();
        }

        // Loading the base.
        AsyncOperation loadingBase;
        loadingBase = SceneManager.LoadSceneAsync("_Scenes/_Base");

        while (!loadingBase.isDone)
        {
            yield return new WaitForSeconds(.75f);
        }


        try
        {           
            try
            {
                GetReferences();

                var x = GameObject.FindGameObjectWithTag("TowerInfo");
                towerListObj = x.GetComponentInChildren<PlayerTowerLog>();


                //print(towerListObj);
                towerListObj.LoadTowers(savedFile.towerList);

                //if it is loading onld base, load these, if not get new ones.
                if (!newBase)
                {
                    missionChoice.LoadPathChoices(savedFile.enemyOption1List, savedFile.enemyOption2List);

                    // base is already loaded by this point.  Need to move this up or move the other down, by now it has already initialized the buttons on a 'not picked'
                    //setting.
                    

                    tinkerUpgrades.LoadInfo(savedFile.currentUpgradeLevels, savedFile.learnableUpgrades, savedFile.possibleOptions, savedFile.hasPicked);
                    tinkerUpgrades.AddToBackupList();
                }

                // ---- they can choose new path each time for now.
                //if (f.hasChosenEnemies)
                //{
                //    singleton.LoadEnemyList(f.enemyList);
                //    //missionChoice.   set the has chosen a path variable
                //    //-----------^^^^^^^^
                //}

                //towerList = towerLog.towerList; // initializing off of new object.

                // initialize or create whatever wiht information now.

            }
            catch (Exception e)
            {
                print("Failed! " + e.Message);
            }
            finally
            {
                

            }
        }
        catch(Exception z)
        {
            print("couldn't open file");
        }
        finishedLoading = true;

        yield return new WaitForSeconds(12f);
    }
}
