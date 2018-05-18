using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action {

    public override void Enter(StateController controller)
    {
        controller.agent.SetDestination(controller.patrol.GetNextWaypoint());
        controller.agent.isStopped = false;
        controller.agent.speed = controller.enemyStats.patrolSpeed;

        controller.anim.AnimationWalk();
    }

    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {

        if (controller.agent.remainingDistance <= controller.agent.stoppingDistance && !controller.agent.pathPending)
        {
            controller.agent.SetDestination(controller.patrol.GetNextWaypoint());
        }
    }

    public override void Exit(StateController controller)
    {
        //throw new NotImplementedException();
    }

}
