using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/RealDistance")]
public class RealDistanceDecision : Decision
{
    [SerializeField]
    private float minDistance;

    public override bool Decide(StateController controller)
    {
        return calculateDistance(controller) < minDistance; 
    }

    public float calculateDistance(StateController controller)
    {
        Vector3 destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 currentPos = controller.transform.position;

        return Vector3.Distance(destination, currentPos);
    }

}
