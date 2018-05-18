using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/RemainingDistance")]
public class RemainingDistanceDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return RemainingDistance(controller);
    }

    private bool RemainingDistance (StateController controller)
    {
        if ((controller.agent.remainingDistance <= controller.agent.stoppingDistance  || controller.agent.remainingDistance < 0.5f) && !controller.agent.pathPending)
        {
            Debug.Log("Remaing Distance reached " + controller.agent.remainingDistance);
            return true;
        }
        else
        {
            return false;
        }
    }

}
