using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour {

    //  Public because it is a data structure.
    public bool isnotExplored = true;
    public bool c_isnotExplored = true;
    public Waypoint ExploredFrom;
    public Waypoint c_ExploredFrom;

    Vector2Int gridPos;
    const int gridSize = 10;

    //  For Lights
    [SerializeField] Waypoint lastWaypoint;
    [SerializeField] Waypoint c_lastWaypoint;
    [SerializeField] Light waypointSpotLight;
    static Light currentWaypointLight;
    static float lightIntensity;
    static bool madeLight = false;

    // tower placer
    public bool isPlaceable = true;
    public bool isAvailable = true;

    public bool c_isPlaceable = true;
    public bool c_isAvailable = true;


    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(madeLight)
            currentWaypointLight.GetComponent<Light>().intensity = 0;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().LastWaypointClicked(this);

                Vector3 lightHeightAdjustment = new Vector3(0f, 16f, 0);
                if (!madeLight)
                {
                    currentWaypointLight = Instantiate(waypointSpotLight, this.transform.position + lightHeightAdjustment, Quaternion.Euler(90, 0, 0));
                    madeLight = true;
                    lightIntensity = currentWaypointLight.GetComponent<Light>().intensity;
                }
                else
                {
                    currentWaypointLight.transform.position = this.transform.position + lightHeightAdjustment;
                    currentWaypointLight.GetComponent<Light>().intensity = lightIntensity;
                }
            }
        }  
    }
}
