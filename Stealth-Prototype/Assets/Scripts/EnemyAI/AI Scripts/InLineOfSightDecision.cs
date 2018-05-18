using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class InLineOfSightDecision : Decision {

    public override bool Decide (StateController controller)
    {
        //Debug.Log("checking los");
        return Look(controller);
    }

    private bool Look (StateController controller)
    {
        if (controller.enemySenses.CheckLineOfSight())
        {
            //Debug.Log("Player in sight!");
        }
        return controller.enemySenses.CheckLineOfSight();
    }

}
