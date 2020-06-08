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
    private const string savedDataFileName = "/TowerInformation.dat";
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
        _singleton = Singleton.Instance;
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
        saver.currentLevel = _singleton.level;
    }

    public void Save()
    {
        GetReferences();
        FillInSaveObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + savedDataFileName);

        // initialize or w/e i want to do be4 sving

        bf.Serialize(file, saver); // this is whats serialized.
        file.Close();
    }

    public void ContinueGame()
    {
        if (File.Exists(Application.persistentDataPath + savedDataFileName))
        {
            LoadSavedBase();
        } else
        {
            SceneManager.LoadSceneAsync("_Scenes/Base Exit Doorway");
        }
    }



    public void GameStartShell()
    {
        ClearGameInfo();
        finishedLoading = false;
        IEnumerator start;
        start = GameStart();

        StartCoroutine(start);
        if (finishedLoading)
        {
            StopCoroutine(start);
            print("Loaded!!");
        }
    }

    private IEnumerator GameStart()
    {
        AsyncOperation loadingBase;
        loadingBase = SceneManager.LoadSceneAsync("_Scenes/Base Exit Doorway");

        while (!loadingBase.isDone)
        {
            yield return new WaitForSeconds(.50f);
        }
        yield return new WaitForSeconds(.50f);
        print("FinishedLoading!");
        //SceneManager.LoadSceneAsync("_Scenes/Base Exit Doorway");
    }

    public void LoadNewGameBase()
    {

        AsyncOperation loadingBase;
        loadingBase = SceneManager.LoadSceneAsync("_Scenes/_Base");
        //finishedLoading = false;
        //IEnumerator load;
        //load = LoadFromFile((int)LoadBase.NewGame);

        //StartCoroutine(load);
        //if (finishedLoading)
        //{
        //    StopCoroutine(load);
        //    print("shutdown!");
    }

    public void LoadSavedBase()
    {
        finishedLoading = false;
        IEnumerator load;
        load = LoadFromFile(true);  //(int)LoadBase.LoadInUseBase);

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
        load = LoadFromFile(false);  //(int)LoadBase.LoadANewBase);

        StartCoroutine(load);
        if (finishedLoading)
        {
            StopCoroutine(load);
            print("shutdown!");
        }
    }

    /// <summary>
    /// This clears the saved game information, readying it for a new game.
    /// </summary>
    public void ClearGameInfo()
    {
        if (File.Exists(Application.persistentDataPath + savedDataFileName))
        {
            File.Delete(Application.persistentDataPath + savedDataFileName);
        }
    }

    public IEnumerator LoadFromFile(bool isLoadingFromFile)
    {
        // This is done in 3 steps, first, load file and initialize the singleton.
        // 2nd is to load base (singleton already has loaded the setting base needs)
        // 3rd is to do all the loading that requires the base active.

        SaveSerializedObject savedFile = null;
        _singleton = Singleton.Instance;
        FileStream file = null;
        try
        {
            file = File.Open(Application.persistentDataPath + savedDataFileName, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            savedFile = (SaveSerializedObject)bf.Deserialize(file);

            // Singleton is loaded FIRST, that way it is initialized to what I need at the time of the other files loading.
            if (isLoadingFromFile)
            {
                _singleton.isHasLearnedATower = savedFile.hasChosenATower;
                _singleton.SetLevel(savedFile.currentLevel);
            }           
        }
        catch (Exception e1)
        {
            print(e1.Message);
        }
        finally
        {
            try
            {
                file.Close();
            }
            catch (Exception e2)
            {
                // Do nothing, in this case the file may not exist to be closed.
            }
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

                // This is outside because I want to load all learned towers on return, but RESET that things are learned, so I can again for new base.
                var x = GameObject.FindGameObjectWithTag("TowerInfo");
                _towerListObj = x.GetComponentInChildren<PlayerTowerLog>();

                _towerListObj.LoadTowers(savedFile.towerList);
                //_tinkerUpgrades.LoadInfoAndSavedOptions(savedFile.currentUpgradeLevels, savedFile.learnableUpgrades, savedFile.possibleOptions, savedFile.hasPicked, true);
                _tinkerUpgrades.AddToBackupList();

                //if it is loading old base, load these, if not get new ones.
                if (isLoadingFromFile)
                {
                    _missionChoice.LoadPathChoices(savedFile.enemyOption1List, savedFile.enemyOption2List);
                    _tinkerUpgrades.LoadInfoAndSavedOptions(savedFile.currentUpgradeLevels, savedFile.learnableUpgrades, savedFile.possibleOptions, savedFile.hasPicked, true);
                }  else
                {
                    // this function is the 'reset' of the above.  It sets false to 'has picked' and 'hasRolled', while setting empty to the sotred options.
                    _tinkerUpgrades.LoadInfoAndSavedOptions(savedFile.currentUpgradeLevels, savedFile.learnableUpgrades, new int[] { }, false, false);
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
