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
    private bool isLastChatSegment = false;
    private bool isEnableingChatTurnOff = false;

    [SerializeField] float typingSpeed = .02f;

    [SerializeField] Button towerButton;
    [SerializeField] Light spotLight;

    [Header("Soldier")]
    [SerializeField] Texture soldierNeutral;
    [SerializeField] Texture soldierScared;

    [Header("General")]
    [SerializeField] Texture general;
    [SerializeField] Texture generalShouting;

    [Header("WayPoints")]
    [SerializeField] Waypoint waypoint1;
    [SerializeField] Waypoint waypoint2;
    private int lightHeight = 19;

    private int towerSpawnCount = 0;

    string string0 = ". . . ";
    string string1 = "It shouldn't take this long to find scraps.";
    string string2 = "Sir!!!  There's almost nothing left! I can only find one good piece, we’ve already scavenged most of the metal here.";
    string string3 = "SHUT UP! THE BUGS ARE BACK, GET BACK HERE NOW!!";
    string string4 = "?!?!?!?? HNNGGG";
    string string5 = "NO!! no, we WILL save him, it will have to be enough... ";
    string string6 = "THROW THE TOWER!  Up there! on the node, that base should give the tower increased range and damage.";
    string string7 = "He WILL make it back.";
    string string8 = "Good job.";
    string string9 = "Now, we wait and see if it is enough.";
    string string10 = "Ugh.  I hope this pack ends soon... The ones up front are always the starved and malnurished, those are easy to kill; but as it goes longer" +
    " the ones in the back are health and strong.  They have no need to rush for food, and they take many more bullets to kill.";
    string string11 = "Tobias!  There are a LOT more than usual. HELP!!!.";
    string string12 = "HELP!!!.";
    string string13 = "This isn't good.. That tower alone wont be enough.";
    string string14 = "Fire the last tower over there.  We will need to utilize the base doors to hold them back, hopefully they hold long enough.";


    string stringy = "Oh my god thats a big wave! WHAT??";
    string stringz = "It has the swarm strain in its pack??  I dont know if it will be enough but.. THROW THE LAST TOWER"; //maybe popup explanations as well?

    // O SHIT! they have Rorendurs? something*.  We will need more than one tower for those enemies for sure. ?  those are swarm enemies that have less HP than the normal
    // guys but they spawn in a brood of 2, making the single shot rifled tower less effective.
    // Use this for initialization
    void Start()
    {
        //towerButton.enabled = false;
        towerButton.gameObject.SetActive(false);
        spotLight.gameObject.SetActive(false);
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.SetDelayedSpawnTime(25f);
        Singleton singleton = Singleton.Instance;
        singleton.enemyList = new List<int> { 1, -1,   1, -1,   1, -1,   1, 1, (int)Enemies.doubles, (int)Enemies.doubles, 1};
        //enemySpawner.enemyList = new List<int> { 1, 1, -1, 1, 1, 1};

        singleton.SetLevel(1);

        // initializing strings
        text.text = "";
        
        conversations = new List<string>();
        conversations.AddRange(new string[] { string0, string1, string2, string3, string4, string5, string6, string7 });
        conversationTracker = 0;

        GoldManagement GM = FindObjectOfType<GoldManagement>();
        GM.goldCount = 90;
        GM.GoldCounter();

        StartCoroutine(SlowMessageTyping());
    }

    // I can make it whe ni build a tower start slow message again, but change the converstaions item, clear and add new dialogues.
    //NOPE IM STUPID  this is a function based off a button, a button that is not a necessary prefab.  Just have the button also do everything I want.

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

        isEnableingChatTurnOff = true;
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
                conversationTracker++;
                break;
            case 1:
                typingSpeed = .02f;
                personTalking.texture = soldierNeutral;
                talking = conversations[conversationTracker];
                conversationTracker++;
                break;
            case 2:
                personTalking.texture = soldierNeutral;
                talking = conversations[conversationTracker];
                conversationTracker++;
                break;
            case 3:
                personTalking.texture = generalShouting;
                talking = conversations[conversationTracker];
                conversationTracker++;
                break;
            case 4:
                // To make the character run in scene
                timeToRun = true;
                SpawnTheEnemiesAtScreem();

                personTalking.texture = soldierScared;
                talking = conversations[conversationTracker];
                conversationTracker++;
                break;
            case 5:
                personTalking.texture = general;
                talking = conversations[conversationTracker];
                conversationTracker++;
                Time.timeScale = 0f;
                break;
            case 6:
                personTalking.texture = generalShouting;
                talking = conversations[conversationTracker];
                conversationTracker++;
                break;
            case 7:
                personTalking.texture = general;
                talking = conversations[conversationTracker];
                conversationTracker++;

                towerButton.gameObject.SetActive(true);
                spotLight.gameObject.SetActive(true);
                spotLight.transform.position = (waypoint1.transform.position + new Vector3(0f, lightHeight, 0f));
                isLastChatSegment = true;
                //towerButton.enabled = true;
                //Time.timeScale = 1.0f;
                break;
            case 8:
                personTalking.texture = general;
                talking = conversations[conversationTracker];
                conversationTracker++;
                break;
            case 9:
                personTalking.texture = general;
                talking = conversations[conversationTracker];
                conversationTracker++;
                isLastChatSegment = true; // SIGH maybe use this bool to make an if in the slowtalk function so IF this is enabled, it swaps the one that it looks at in the function to disable below... w/e
                break;


        }

        yield break;
    }


    public void BuiltTowerNextConversation()
    {

        switch (towerSpawnCount)
        {
            case 0:
                break;
            case 1:
                //TODO treat differently.  This is
                return;
            default:
                break;
        }
        isEnableingChatTurnOff = false;
        Time.timeScale = 1.0f;
        //conversations.Clear();
        conversations.AddRange(new string[] { string8, string9 });
        conversationTracker = 8;

        spotLight.gameObject.SetActive(false);
        talkingCanvas.enabled = true;
        StartCoroutine(SlowMessageTyping());

        StartCoroutine(DisableText());
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
        yield return new WaitForSecondsRealtime(3);
        if (isEnableingChatTurnOff)
        {
            talkingCanvas.enabled = false;
        }       
    }


    // Update is called once per frame
    void Update()
    {

    } 
}
