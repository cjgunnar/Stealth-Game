using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Die")]
public class DieAction : Action {

    public override void Enter(StateController controller)
    {
        //call die function in state controller
        controller.Die();
    }

    public override void Act(StateController controller)
    {
        //can not act in death
    }

    public override void Exit(StateController controller)
    {
        //can not exit death
    }

}
