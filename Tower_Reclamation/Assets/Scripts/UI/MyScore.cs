using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MyScore : MonoBehaviour {

    [SerializeField] public Text score;
    [SerializeField] public int scoreCount = 0;

	// Use this for initialization
	void Start () {
        score.text = "Score : " + scoreCount.ToString();
	}
	

    public void ScoreUpOne()
    {
        ++scoreCount;
        score.text = "Score : " + scoreCount.ToString();
    }
}
