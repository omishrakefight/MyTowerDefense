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
    PlayerTowerLog _towerListObj;
    Singleton _singleton;
    TinkerUpgrades _tinkerUpgrades;
    //[SerializeField] ChooseNextMissionPath missionChoice;
    ChooseNextMissionPath _missionChoice;

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
        _missionChoice = GameObject.FindGameObjectWithTag(TagForBase).GetComponentInChildren<ChooseNextMissionPath>();
        _towerListObj = GameObject.FindGameObjectWithTag(TagForBase).GetComponentInChildren<PlayerTowerLog>();// FindObjectOfType<PlayerTowerLog>();
        _singleton = FindObjectOfType<Singleton>();
        _tinkerUpgrades = FindObjectOfType<TinkerUpgrades>();
    }

    private void FillInSaveObject()
    {
        towerList = _towerListObj.SaveTowers();

        saver = new SaveSerializedObject();

        saver.SaveTowers(_towerListObj.SaveTowers());
        saver.IsHasChosenATower(_singleton.isHasLearnedATower);
        // need to convert back to a list when reading in. ?
        // ERROR IS BEACAUSE THE SCRIPT IS DISABLED IN SAVE WINDOW, THE CANVASSES ARE ALL DISABLED EXCEPT USED ONE.
        saver.SaveEnemyOptions(_missionChoice.firstEnemySet.ToArray(), _missionChoice.secondEnemySet.ToArray());

        saver.SaveTinkerRoomInfo(_tinkerUpgrades.SaveCurrentUpgradeLevels(), _tinkerUpgrades.SaveLearnableUpgrades(), _tinkerUpgrades.SavePossibleOptions(), _tinkerUpgrades.SaveHasPicked());

        if (_missionChoice.isHasChosen)
        {
            saver.IsHasChosenEnemies(true);
            saver.SaveEnemiesChosen(_singleton.enemyList.ToArray());
        }

        saver.towerList = _towerListObj.SaveTowers();
    }

    public void Save()
    {
        GetReferences();
        FillInSaveObject();

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
                _singleton.isHasLearnedATower = savedFile.hasChosenATower;
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
            yield return new WaitForSeconds(.50f);
        }


        // set everything that needs an active base.  
        try
        {           
            try
            {
                // need the references of base objects AFTER load
                GetReferences();

                var x = GameObject.FindGameObjectWithTag("TowerInfo");
                _towerListObj = x.GetComponentInChildren<PlayerTowerLog>();

                _towerListObj.LoadTowers(savedFile.towerList);

                //if it is loading old base, load these, if not get new ones.
                if (!newBase)
                {
                    _missionChoice.LoadPathChoices(savedFile.enemyOption1List, savedFile.enemyOption2List);
                    
                    _tinkerUpgrades.LoadInfo(savedFile.currentUpgradeLevels, savedFile.learnableUpgrades, savedFile.possibleOptions, savedFile.hasPicked);
                    _tinkerUpgrades.AddToBackupList();
                }

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
