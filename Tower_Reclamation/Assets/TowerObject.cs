using UnityEngine;
using System;

[Serializable]
public class TowerObject : MonoBehaviour {

    public string Name = "";
    public int towerHeadType = -1;
    public int towerBaseType = -1;
	// Use this for initialization
	void Start () {
		
	}

    // MAYBE LATER add in mastery as well?
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetTowerName()
    {
        return Name;
    }
    public int GetTowerHeadType()
    {
        return towerHeadType;
    }
    public int GetTowerBaseType()
    {
        return towerBaseType;
    }

    public void SetTowerName(string _name)
    {
        Name = _name;
    }
    public void SetTowerHeadType(int headType)
    {
        towerHeadType = headType;
    }
    public void SetTowerBaseType(int baseType)
    {
        towerBaseType = baseType;
    }
}
