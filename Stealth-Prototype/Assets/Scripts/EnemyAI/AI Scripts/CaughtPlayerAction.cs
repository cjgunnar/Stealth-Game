using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/End Game")]
public class CaughtPlayerAction : Action {

    public override void Enter(StateController controller)
    {
        if (!controller.gameOver)
        {
            controller.anim.Caught();
            controller.manager.LostGame();
        }
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
