using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Return To Previous State")]
public class ReturnToPreviousAction : Action {

    public override void Enter(StateController controller)
    {
        Debug.Log("Returning to previous state");
        controller.TransitionToPreviousState();
    }

    public override void Act(StateController controller)
    {
        //throw new NotImplementedException();
    }

    public override void Exit(StateController controller)
    {
        //throw new NotImplementedException();
    }

}
