using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MoreInformationPanel : MonoBehaviour {

    [SerializeField] Text informationText;
    [SerializeField] Sprite picture;
    float outsideTimeScale;
    float pauseTime = 0f;

	// Use this for initialization
	void Start () {
        outsideTimeScale = Time.timeScale;
        Time.timeScale = pauseTime;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void CloseThePanel()
    {
        // revert it to whatever timescale it was before the popup.
        Time.timeScale = outsideTimeScale;
        Destroy(this.gameObject);
    }

    public MoreInformationPanel() { }

}
