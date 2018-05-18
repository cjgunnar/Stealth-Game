using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Seen Player")]
public class SeenPlayerDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return PlayerSeen(controller);
    }

    private bool PlayerSeen (StateController controller)
    {
        if (controller.aware)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
