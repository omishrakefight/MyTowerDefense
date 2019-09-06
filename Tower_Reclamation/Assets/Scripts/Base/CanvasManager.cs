using System.Collections;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

    FadeScript fader;

    [Header ("Canvases")]
    [SerializeField] Canvas computerBase;
    [SerializeField] Canvas turretFactory;
    [SerializeField] Canvas engineerer;
    [SerializeField] Canvas meetingRoom;
    [SerializeField] Canvas Tinker;
    Canvas currentActiveCanvas;

    [Header ("Fade Filters")]
    [SerializeField] GameObject computerFader;
    [SerializeField] GameObject engineerFader;
    [SerializeField] GameObject tinkerFader;
    [SerializeField] GameObject turretFader;
    [SerializeField] GameObject meetingRoomFader;
    public GameObject currentScreenFader;

    // Use this for initialization
    void Start () {
        // Only have 1 Canvas going at a time
        turretFactory.gameObject.SetActive(true);
        computerBase.gameObject.SetActive(true);
        engineerer.gameObject.SetActive(true);
        Tinker.gameObject.SetActive(true);
        meetingRoom.gameObject.SetActive(true);

        // UI being turned off but the object is on (just canvas part).
        turretFactory.GetComponent<Canvas>().enabled = false;
        computerBase.GetComponent<Canvas>().enabled = false;
        engineerer.GetComponent<Canvas>().enabled = false;
        Tinker.GetComponent<Canvas>().enabled = false;


        computerFader.SetActive(true);
        engineerFader.SetActive(true);
        tinkerFader.SetActive(true);
        turretFader.SetActive(true);
        meetingRoomFader.SetActive(true);

        currentActiveCanvas = meetingRoom;
        currentScreenFader = meetingRoomFader;

        //StartupLoad();
	}
	
    void FadeIn_DisableOldCanvas()
    {
        currentScreenFader.GetComponent<FadeScript>().FadeIn();
        currentActiveCanvas.GetComponent<Canvas>().enabled = false;
    }

    //public void StartupLoad()
    //{
    //    meetingRoom.GetComponent<Canvas>().enabled = false;  //set unenabled and see if the scrips load
    //}

    // room buttons, first checks if the active room is the one clicked.
    public void ChooseComputerRoom()
    {
        if (currentActiveCanvas != computerBase)
        {
            StartCoroutine(ComputerRoom());
        }
    }
    public void ChooseTurretRoom()
    {
        if (currentActiveCanvas != turretFactory)
        {
            StartCoroutine(TurretRoom());
        }
    }
    public void ChooseEngineerRoom()
    {
        if (currentActiveCanvas != engineerer)
        {
            StartCoroutine(EngineerRoom());
        }
    }
    public void ChooseTinkerRoom()
    {
        if (currentActiveCanvas != Tinker)
        {
            StartCoroutine(TinkerRoom());
        }
    }
    public void ChooseMeetingRoom()
    {
        if (currentActiveCanvas != meetingRoom)
        {
            StartCoroutine(MeetingRoom());
        }
    }

    IEnumerator MeetingRoom()
    {
        FadeIn_DisableOldCanvas();
        var delay = currentScreenFader.GetComponent<FadeScript>().fadeTime;
        yield return new WaitForSeconds(delay);

        meetingRoom.GetComponent<Canvas>().enabled = true;
        //meetingRoom.gameObject.SetActive(true);
        meetingRoomFader.gameObject.SetActive(true);

        currentActiveCanvas = meetingRoom;
        currentScreenFader = meetingRoomFader;
        currentScreenFader.GetComponent<FadeScript>().FadeOut();
        yield break;
    }
    IEnumerator TinkerRoom()
    {
        FadeIn_DisableOldCanvas();
        var delay = currentScreenFader.GetComponent<FadeScript>().fadeTime;
        yield return new WaitForSeconds(delay);

        Tinker.GetComponent<Canvas>().enabled = true;
        //Tinker.gameObject.SetActive(true);
        tinkerFader.gameObject.SetActive(true);

        currentActiveCanvas = Tinker;
        currentScreenFader = tinkerFader;
        currentScreenFader.GetComponent<FadeScript>().FadeOut();
        yield break;
    }
    IEnumerator EngineerRoom()
    {
        FadeIn_DisableOldCanvas();
        var delay = currentScreenFader.GetComponent<FadeScript>().fadeTime;
        yield return new WaitForSeconds(delay);

        engineerer.GetComponent<Canvas>().enabled = true;
        //engineerer.gameObject.SetActive(true);
        engineerFader.gameObject.SetActive(true);

        currentActiveCanvas = engineerer;
        currentScreenFader = engineerFader;
        currentScreenFader.GetComponent<FadeScript>().FadeOut();
        yield break;
    }
    IEnumerator TurretRoom()
    {
        FadeIn_DisableOldCanvas();
        var delay = currentScreenFader.GetComponent<FadeScript>().fadeTime;
        yield return new WaitForSeconds(delay);

        turretFactory.GetComponent<Canvas>().enabled = true;
        //turretFactory.gameObject.SetActive(true);
        turretFader.gameObject.SetActive(true);

        currentActiveCanvas = turretFactory;
        currentScreenFader = turretFader;
        currentScreenFader.GetComponent<FadeScript>().FadeOut();
        yield break;
    }
    IEnumerator ComputerRoom()
    {
        FadeIn_DisableOldCanvas();
        var delay = currentScreenFader.GetComponent<FadeScript>().fadeTime;
        yield return new WaitForSeconds(delay);

        computerBase.GetComponent<Canvas>().enabled = true;
        //computerBase.gameObject.SetActive(true);
        computerFader.gameObject.SetActive(true);

        currentActiveCanvas = computerBase;
        currentScreenFader = computerFader;
        currentScreenFader.GetComponent<FadeScript>().FadeOut();
        yield break;
    }

    /*
    public IEnumerator Wait()
    {
        var delay = currentScreenFader.GetComponent<FadeScript>().fadeTime;
        yield return new WaitForSeconds(delay);
        currentScreenFader.GetComponent<FadeScript>().FadeOut();
    }  */
}
