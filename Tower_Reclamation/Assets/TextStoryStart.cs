using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextStoryStart : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] Text talking;

	// Use this for initialization
	void Start () {
        talking.text = "Welcome, Commander.  It is great to see that you have yet lived you awesome son of a bitch!!";
        for (int i = 0; i < talking.text.Length; i++)
        {
            //string startWord;

           // text.text = ;

           // string startWord = talking.text.ToCharArray(i, i+1);
            talking.text = "dkfka:";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
