using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action {

    public override void Enter(StateController controller)
    {
        controller.agent.speed = controller.enemyStats.chaseSpeed;

        controller.anim.AnimationRun();
        controller.anim.PlayerSpottedBark();
        controller.stats.timesSpotted += 1;

        if (!controller.aware)
        {
            controller.aware = true;
        }
    }

    public override void Act (StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        controller.agent.SetDestination(controller.player.transform.position);

        //controller.anim.ChasingBarks();
    }

    public override void Exit(StateController controller)
    {
        //play "got away" bark
        controller.anim.LostPlayer();
    }

}
