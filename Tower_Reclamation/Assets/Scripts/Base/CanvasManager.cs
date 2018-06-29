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
        turretFactory.gameObject.SetActive(false);
        computerBase.gameObject.SetActive(false);
        engineerer.gameObject.SetActive(false);
        Tinker.gameObject.SetActive(false);
        meetingRoom.gameObject.SetActive(true);

        computerFader.SetActive(true);
        engineerFader.SetActive(true);
        tinkerFader.SetActive(true);
        turretFader.SetActive(true);
        meetingRoomFader.SetActive(true);

        currentActiveCanvas = computerBase;
        currentScreenFader = computerFader;
	}
	
    void FadeIn_DisableOldCanvas()
    {
        currentScreenFader.GetComponent<FadeScript>().FadeIn();
        currentActiveCanvas.gameObject.SetActive(false);
    }

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

        meetingRoom.gameObject.SetActive(true);
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

        Tinker.gameObject.SetActive(true);
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

        engineerer.gameObject.SetActive(true);
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

        turretFactory.gameObject.SetActive(true);
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

        computerBase.gameObject.SetActive(true);
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
