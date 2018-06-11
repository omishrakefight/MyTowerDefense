using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextStoryStart : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] string talking;

	// Use this for initialization
	void Start () {
        StartCoroutine(TextTyper());
    }

    IEnumerator TextTyper()
    {
        text.text = "";
        talking = "Welcome, Commander.  It is great to see that you have yet lived you awesome son of a bitch!!";
        for (int i = 0; i < talking.Length; i++)
        {

            var letter = talking.ToCharArray(i, 1);
            text.text += new string(letter); 
            yield return new WaitForSeconds(.05f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
