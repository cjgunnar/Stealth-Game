using System;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/GoToLastSeenLocation")]
public class LocalSearchAction : Action {

    public override void Enter(StateController controller)
    {
        controller.agent.SetDestination(controller.enemySenses.lastSeenlocation);
        Debug.Log("moving to last seen location");
    }

    public override void Act(StateController controller)
    {
        //LocalSearch(controller);
    }

    private void LocalSearch (StateController controller)
    {
        //all logic done in enter state, nothing to update
    }

    public override void Exit(StateController controller)
    {
        //nothing needed here
    }

}
