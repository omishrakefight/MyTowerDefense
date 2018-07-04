using UnityEngine;
using UnityEngine.UI;

public class EnableScriptsOnStart : MonoBehaviour {


    [SerializeField] Canvas canvasToEnable;

	void Start () {
        canvasToEnable.gameObject.SetActive(true);
	}
	
}
