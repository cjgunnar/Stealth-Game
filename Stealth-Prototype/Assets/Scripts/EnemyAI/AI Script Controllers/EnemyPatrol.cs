using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

    //patrol
    private int destPoint = -1;
    public bool willPatrol = true;
    public Transform pathHolder;

    [HideInInspector]
    public Vector3[] waypoints;

    //post variables
    [HideInInspector]
    public Vector3 postPosition;

    [HideInInspector]
    public Quaternion postRotation;

    void Start () {

        if (willPatrol)
        {
            //Debug.Log("setting waypoints");
            //get positions of all path elements
            waypoints = new Vector3[pathHolder.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = pathHolder.GetChild(i).position;
            }
        }
        else
        {
            postPosition = transform.position;
            postRotation = transform.rotation;
            //Debug.Log("Set post to position " + postPosition + " and rotation to " + postRotation);
        }

    }

    public Vector3 GetRandomWaypoint ()
    {
        if (waypoints.Length != 0)
        {
            //add way to make sure the same waypoint is not selected twice
            return waypoints[Random.Range(0, waypoints.Length)];
        }
        else
        {
            Debug.LogError("Error: waypoints is not assigned");
            return Vector3.zero;
        }
    }

    public Vector3 GetNextWaypoint ()
    {
        if (waypoints.Length != 0)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            //Debug.Log("got next waypoint: " + waypoints[destPoint]);
            return waypoints[destPoint];
        }
        else
        {
            Debug.LogError("Error: waypoints is not assigned");
            return Vector3.zero;
        }

    }

    void OnDrawGizmos ()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in pathHolder)
        {
            if (waypoint.position == startPosition)
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = Color.white;
            }
            Gizmos.DrawSphere(waypoint.position, .3f);
            Vector3 tempPrevious = previousPosition;
            tempPrevious.y += .25f;
            Vector3 tempCurrent = waypoint.position;
            tempCurrent.y += .25f;
            Gizmos.DrawLine(tempPrevious, tempCurrent);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }
}
