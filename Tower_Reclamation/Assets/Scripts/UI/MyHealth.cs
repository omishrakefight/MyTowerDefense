using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyHealth : MonoBehaviour {

    [SerializeField] public int myNumericalHealth = 10;
    [SerializeField] public Text myHealth;
    [SerializeField] public Text myHealthNumber;
    [SerializeField] Image HPbar;
    [SerializeField] AudioClip baseHurtAudio;

    // Use this for initialization
    void Start () {
        myHealth.text = "Base HP : ";// + myNumericalHealth.ToString() + " / 10";
        myHealthNumber.text = myNumericalHealth.ToString() + " / 10";
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void AnEnemyFinishedThePath()
    {
        myNumericalHealth -= 1;
        myHealthNumber.text = myNumericalHealth.ToString() + " / 10";
        GetComponent<AudioSource>().PlayOneShot(baseHurtAudio);
        HPbar.fillAmount -= .1f;
        if (myNumericalHealth <= 0)
        {
            FindObjectOfType<EnemySpawner>().stillAlive = false;
        }
    }
}
