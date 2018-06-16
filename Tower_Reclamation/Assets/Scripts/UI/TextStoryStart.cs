using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextStoryStart : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] string talking;
    List<string> conversations;
    int conversationTracker = 0;

    [SerializeField] Texture mainCharacter;
    [SerializeField] Texture leader;

    [SerializeField] RawImage personTalking;

    [SerializeField] Canvas talkingCanvas;

    string string0;
    string string1;
    string string2;
    string string3;
    string string4;
    string string5;

    // Use this for initialization
    void Start () {
        // initializing strings
        string0 = "Ok gents, we need to get out there and scavenge some metal.  That last wandering group of bugs damaged " +
            "the gate a bit.  Let's bring some back and repair before more arrive.";
        string1 = " Yes sir.  I’ll head out now.";
        string2 = "Sir!!!  There's almost nothing left! I can only find one good piece, we’ve already scavenged most of the metal here.";
        string3 = "SHUT UP! THE BUGS ARE BACK, GET BACK HERE NOW!!";
        string4 = "?!?!?!?? AGHHHHH";
        conversations = new List<string>();
        conversations.AddRange  (new string[] { string0, string1, string2, string3, string4 });
        conversationTracker = 0;


        StartCoroutine(TextTyper());
    }

    IEnumerator TextTyper()
    {

            text.text = "";
            talking = "Welcome, Commander.  It is great to see that you have yet lived you awesome son of a bitch!!";
            StartCoroutine(SlowMessageTyping());

        yield break;
    }

    private void TutorialSpeech()
    {
        text.text = "";
        SlowMessageTyping();

    }

    IEnumerator ConversationPicker()
    {
        if (conversationTracker == 0)
        {
            personTalking.texture = leader;
            talking = conversations[conversationTracker];
        }
        if (conversationTracker == 1)
        {
            personTalking.texture = mainCharacter;
            talking = conversations[conversationTracker];
        }
        if (conversationTracker == 2)
        {
            personTalking.texture = mainCharacter;
            talking = conversations[conversationTracker];
        }
        if (conversationTracker == 3)
        {
            personTalking.texture = leader;
            talking = conversations[conversationTracker];
        }
        if (conversationTracker == 4)
        {
            personTalking.texture = mainCharacter;
            talking = conversations[conversationTracker];
        }

        conversationTracker++;
        yield break;
    }
    IEnumerator SlowMessageTyping()
    {
        StartCoroutine(ConversationPicker());
        text.text = "";
        // Loop the converstation 1 char at a time.
        for (int i = 0; i < talking.Length; i++)
        {
            var letter = talking.ToCharArray(i, 1);
            text.text += new string(letter);
            yield return new WaitForSeconds(.02f);
        }
        yield return new WaitForSeconds(3);
        if (conversationTracker < conversations.Count)
        {
            print(conversationTracker);
            yield return StartCoroutine(SlowMessageTyping());
        }
        print("spawn enemies?");
        StartCoroutine(FindObjectOfType<EnemySpawner>().ContinualSpawnEnemies());

        StartCoroutine(DisableText());
    }

    private IEnumerator DisableText()
    {
        yield return new WaitForSeconds(4);
        talkingCanvas.enabled = false;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
