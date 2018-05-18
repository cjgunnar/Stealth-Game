using System;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/GoToAlarm")]
public class GoToAlarmAction : Action {

    public override void Enter(StateController controller)
    {
        LocateNearestAlarm(controller);

        controller.agent.speed = controller.enemyStats.patrolSpeed;
    }

    public override void Act(StateController controller)
    {
        TriggerAlarm(controller);
    }

    private void TriggerAlarm (StateController controller)
    {
        if ((controller.agent.remainingDistance <= controller.agent.stoppingDistance || controller.agent.remainingDistance <= 0.1) && !controller.agent.pathPending)
        {
            controller.manager.TriggerAlarm();
        }
    }

    private void LocateNearestAlarm (StateController controller)
    {
        Collider[] col = Physics.OverlapSphere(controller.transform.position, controller.searchRadius, controller.alarmLayer);

        float distance = 100000;
        GameObject closest = null;
        if (col.Length != 0)
        {
            Debug.Log("Found " + col.Length + " alarm button(s) nearby");
            for (int i = 0; i < col.Length; i++)
            {
                float toDistance = Vector3.Distance(controller.transform.position, col[i].transform.position);
                if (toDistance <= distance)
                {
                    closest = col[i].gameObject;
                    distance = toDistance;
                }
            }
            if (closest != null)
            {
                controller.agent.SetDestination(closest.transform.position);
            }
        }

        else
        {
            Debug.Log("found no alarm buttons nearby");
        }
    }

    public override void Exit(StateController controller)
    {
        //nothing needed here
    }

}
