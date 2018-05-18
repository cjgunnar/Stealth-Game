using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/GameOver")]
public class GameOverDecision : Decision {

    public override bool Decide(StateController controller)
    {
        if (controller.gameOver)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
