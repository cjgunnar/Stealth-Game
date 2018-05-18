using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/RandomPatrol")]
public class RandomPatrolAction : Action {

    public override void Enter(StateController controller)
    {
        controller.agent.SetDestination(controller.patrol.GetRandomWaypoint());
        controller.agent.isStopped = false;
        controller.agent.speed = controller.enemyStats.searchSpeed;

        //set animation
        controller.anim.AnimationWalk();
    }

    public override void Act(StateController controller)
    {
        RandomPatrol(controller);
    }

    private void RandomPatrol (StateController controller)
    {
        if (controller.agent.remainingDistance <= controller.agent.stoppingDistance && !controller.agent.pathPending)
        {
            controller.agent.SetDestination(controller.patrol.GetRandomWaypoint());
            controller.anim.AnimationWalk();
        }
    }

    public override void Exit(StateController controller)
    {
        //throw new NotImplementedException();
    }

}
