using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Heard Something")]
public class HeardSomethingDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool heardSomething = CheckHearing(controller);
        return heardSomething;
    }

    private bool CheckHearing (StateController controller)
    {
        if (controller.enemySenses.noisePosition != controller.enemySenses.defaultNoisePosition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
