using System;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Investigate")]
public class InvestigateAction : Action {

    public override void Enter(StateController controller)
    {
        controller.agent.SetDestination(controller.enemySenses.noisePosition);
        controller.agent.speed = controller.enemyStats.investigateSpeed;

        controller.anim.SomethingHeard();
        controller.anim.AnimationWalk();
    }

    public override void Act(StateController controller)
    {
        if ((Vector3.Distance(controller.transform.position, controller.enemySenses.noisePosition) < .5f || controller.agent.remainingDistance < 1f) && !controller.agent.pathPending)
        {
            Debug.Log("done investigating...");
            controller.TransitionToPreviousState();
        }

    }

    public override void Exit(StateController controller)
    {
        //reset the noiseLocation;
        controller.enemySenses.noisePosition = controller.enemySenses.defaultNoisePosition;
    }

}
