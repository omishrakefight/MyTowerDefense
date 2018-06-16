using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterPathfinder : MonoBehaviour {

    Dictionary<Vector2, Waypoint> grid = new Dictionary<Vector2, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    List<Waypoint> path = new List<Waypoint>();
    Waypoint searchCenter;
    bool isSearching;
    bool makeNewPath = true;

    // So we have the path upon start, as well as the enemy path lights.
    private void Start()
    {
        GivePath();
    }

    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.right,
        Vector2Int.left
    };


    private void CreatePath()
    {
        path.Add(endWaypoint);
        endWaypoint.isPlaceable = false;
        endWaypoint.isAvailable = false;

        Waypoint previous = endWaypoint.ExploredFrom;
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous.isPlaceable = false;
            previous.isAvailable = false;
            previous = previous.ExploredFrom;
        }
        path.Add(startWaypoint);
        path.Reverse();
    }


    public List<Waypoint> GivePath()
    {
        if (makeNewPath)
        {
            LoadBlocks();
            BreadthFirstSearch();
            CreatePath();
            makeNewPath = false;
        }
        return path;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        isSearching = true;

        //Only works while isSearching is true, turned off when end found.
        while (queue.Count > 0 && isSearching == true)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isnotExplored = false;
            StopIfEnd();
            ExploreAdjacent();
        }
    }

    private void StopIfEnd()
    {
        if (searchCenter == endWaypoint)
        {
            isSearching = false;
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            if (isOverlapping)
            {
                Debug.LogWarning("Overlapping Block " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
    }

    private void ExploreAdjacent()
    {
        if (!isSearching) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoordinants = searchCenter.GetGridPos() + direction;
            try
            //if(grid.ContainsKey(adjacent)
            {
                Waypoint adjacent = grid[explorationCoordinants];
                if (adjacent.isnotExplored && !queue.Contains(adjacent) && adjacent.gameObject.CompareTag("WalkPath"))
                {
                    queue.Enqueue(adjacent);
                    adjacent.ExploredFrom = searchCenter;
                }

            }
            catch
            {
                // do nothing
            }
        }
    }

}
