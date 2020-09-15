using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SaveSerializedObject  {

    public bool[] towerList;
    public int currentLevel = 1;

    public bool hasChosenEnemies = false;
    public int[] enemyList;
    public int[] enemyOption1List;
    public int[] enemyOption2List;

    public bool hasChosenATower = false;

    //Tinker room
    public int[] currentUpgradeLevels;
    public int[] learnableUpgrades;
    public int[] possibleOptions;
    public bool hasPicked;

    public List<int> List;
    public Dictionary<string, int> dic;
    public Dictionary<string, Dictionary<string, int>> knownTowersAndParts;
    public Dictionary<string, Dictionary<string, int>> learnableTowersAndParts;
    public Dictionary<string, Dictionary<string, int>> unlearnableTowersAndParts;

    public bool isTutorial = false;
    public bool hasExplainedComputerRoom = false;
    public bool hasExplainedEngineerRoom = false;
    public bool hasExplainedTinkerRoom = false;
    public bool hasExplainedTurretRoom = false;
    public bool hasExplainedMeetingRoom = false;

    // add the bool

    /// <summary>
    /// I need to make more parts.  I am going to assimilate them into a single dictionary of learned ones string int,
    ///  then I need to make the keys strings that have tower type at front (flame), and end with other type (base) and name in middle.\
    ///  could change enums to be spaced every 20, and then have a big bool array for towers parts known.
    /// </summary>

    public SaveSerializedObject()
    {
    }

    //public void SaveList(List<int> list)
    //{
    //    List = list;
    //}
    //public List<int> LoadList()
    //{
    //    return List;
    //}
    //public void SaveDic()
    //{
    //    dic = new Dictionary<string, int>();
    //    dic.Add("f", 1);
    //    dic.Add("z", 2);
    //    dic.Add("a", 3);
    //    dics = new Dictionary<string, Dictionary<string, int>>();
    //    dics.Add("Rifled Tower", dic);
    //    dic.Remove("z");
    //    dics.Add("Slow Tower", dic);
    //}
    //public Dictionary<string, Dictionary<string, int>> LoadDic()
    //{
    //    return dics;
    //}
    public void SaveTinkerRoomInfo(int[] _currentUpgradeLevels, int[] _learnableUpgrades, int[] _possibleOptions, bool _hasPicked)
    {
        currentUpgradeLevels = _currentUpgradeLevels;
        learnableUpgrades = _learnableUpgrades;
        possibleOptions = _possibleOptions;
        hasPicked = _hasPicked;
    }

    public void SetTutorial(bool _isTutorial)
    {
        isTutorial = _isTutorial;
    }

    public void IsHasChosenATower(bool chosen)
    {
        hasChosenATower = chosen;
    }

    public void SaveTowers(bool[] towerListSaves)
    {
        towerList = towerListSaves;
    }

    public void SaveTowersAndParts(Dictionary<string, Dictionary<string, int>> _knownTowersAndParts, Dictionary<string, Dictionary<string, int>> _learnableTowersAndParts, Dictionary<string, Dictionary<string, int>> _unlearnableTowersAndParts)
    {
        knownTowersAndParts = _knownTowersAndParts;
        learnableTowersAndParts = _learnableTowersAndParts;
        unlearnableTowersAndParts = _unlearnableTowersAndParts;
    }


    public void IsHasChosenEnemies(bool hasChosen)
    {
        hasChosenEnemies = hasChosen;
    }

    public void SaveEnemiesChosen(int[] enemies)
    {
        enemyList = enemies;
    }

    public void SaveEnemyOptions(int[] option1, int[] option2)
    {
        enemyOption1List = option1;
        enemyOption2List = option2;
    }

    public void UpdateCurrentLevel()
    {
        
    }

    //public void UpdateTowerList(bool[] newTowerList)
    //{
    //    towerList = newTowerList;
    //}
    //Use this for initialization
    //   void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
