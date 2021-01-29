using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    private Seeker seeker;
    private Path path;

    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;
    public bool reachedEndOfPath;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
    }

    public void Move(Vector3 destination)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
