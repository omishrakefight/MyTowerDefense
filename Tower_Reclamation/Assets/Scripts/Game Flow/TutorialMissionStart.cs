using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TutorialMissionStart : MonoBehaviour {
    [SerializeField] Text text;
    [SerializeField] string talking;
    List<string> conversations;
    int conversationTracker = 0;

    [SerializeField] Canvas talkingCanvas;
    [SerializeField] RawImage personTalking;

    public bool timeToRun = false;
    private bool spawnEnemies = true;

    [SerializeField] float typingSpeed = .02f;

    [Header("Soldier")]
    [SerializeField] Texture soldierNeutral;
    [SerializeField] Texture soldierScared;

    [Header("General")]
    [SerializeField] Texture general;
    [SerializeField] Texture generalShouting;



    string string0;
    string string1;
    string string2;
    string string3;
    string string4;
    string string5 = "NO!! no, we WILL save him, it will have to be enough... ";
    string string6 = "Throw the last tower!   ";
    string string7 = "He WILL make it back.";

    // Use this for initialization
    void Start()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.SetDelayedSpawnTime(25f);
        Singleton singleton = Singleton.Instance;
        singleton.enemyList = new List<int> { 1, 1, 1, -1, 1, -1, 1 };
        //enemySpawner.enemyList = new List<int> { 1, 1, -1, 1, 1, 1};

        singleton.SetLevel(1);

        // initializing strings
        text.text = "";

        string0 = ". . . ";
        string1 = "It shouldn't take this long to find scraps.";
        string2 = "Sir!!!  There's almost nothing left! I can only find one good piece, we’ve already scavenged most of the metal here.";
        string3 = "SHUT UP! THE BUGS ARE BACK, GET BACK HERE NOW!!";
        string4 = "?!?!?!?? HNNGGG";
        
        conversations = new List<string>();
        conversations.AddRange(new string[] { string0, string1, string2, string3, string4, string5, string6, string7 });
        conversationTracker = 0;


        StartCoroutine(SlowMessageTyping());
    }



    IEnumerator SlowMessageTyping()
    {
        // TODO maybe put this in a while loop instead of calling itself?
        StartCoroutine(ConversationPicker());
        text.text = "";
        //SpawnTheEnemiesAtScreem();  NOPE want more control here, removing the if and spawning on demand.
        // Loop the converstation 1 char at a time.
        for (int i = 0; i < talking.Length; i++)
        {
            var letter = talking.ToCharArray(i, 1);
            text.text += new string(letter);
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        yield return new WaitForSecondsRealtime(2);
        if (conversationTracker < conversations.Count)
        {
            //print(conversationTracker);
            yield return StartCoroutine(SlowMessageTyping());
        }

        StartCoroutine(DisableText());
    }

    IEnumerator ConversationPicker()
    {

        switch (conversationTracker)
        {
            case 0:
                typingSpeed = .5f;
                personTalking.texture = soldierNeutral;
                talking = conversations[conversationTracker];
                break;
            case 1:
                typingSpeed = .02f;
                personTalking.texture = soldierNeutral;
                talking = conversations[conversationTracker];
                break;
            case 2:
                personTalking.texture = soldierNeutral;
                talking = conversations[conversationTracker];
                break;
            case 3:
                personTalking.texture = generalShouting;
                talking = conversations[conversationTracker];
                break;
            case 4:
                // To make the character run in scene
                timeToRun = true;
                SpawnTheEnemiesAtScreem();

                personTalking.texture = soldierScared;
                talking = conversations[conversationTracker];
                break;
            case 5:
                personTalking.texture = general;
                talking = conversations[conversationTracker];
                Time.timeScale = .15f;
                break;
            case 6:
                personTalking.texture = generalShouting;
                talking = conversations[conversationTracker];
                break;
            case 7:
                personTalking.texture = general;
                talking = conversations[conversationTracker];
                Time.timeScale = 1.0f;
                break;
            case 8:
                break;
        }


        conversationTracker++;
        yield break;
    }

    private void SpawnTheEnemiesAtScreem()
    {
        //if (conversationTracker == conversations.Count && spawnEnemies)
        //{
            //StartCoroutine(FindObjectOfType<EnemySpawner>().ContinualSpawnEnemies());
            FindObjectOfType<EnemySpawner>().StartBattle();
            spawnEnemies = false;
        //}
    }

    private IEnumerator DisableText()
    {
        yield return new WaitForSeconds(4);
        talkingCanvas.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {

    } 
}
