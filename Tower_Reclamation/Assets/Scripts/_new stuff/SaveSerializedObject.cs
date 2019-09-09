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


    public SaveSerializedObject()
    {
    }

    public void IsHasChosenATower(bool chosen)
    {
        hasChosenATower = chosen;
    }

    public void SaveTowers(bool[] towerListSaves)
    {
        towerList = towerListSaves;
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
