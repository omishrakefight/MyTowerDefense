using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour {

    //  Public because it is a data structure.
    public bool isnotExplored = true;
    public Waypoint ExploredFrom;

    Vector2Int gridPos;
    const int gridSize = 10;

    // tower placer
    public bool isPlaceable = true;
    public bool isAvailable = true;

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
	

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().LastWaypointClicked(this);
            }
        }  
    }
}
