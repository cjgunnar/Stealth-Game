using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Alarm")]
public class AlarmDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return DecideAlarm(controller);
    }

    private bool DecideAlarm(StateController controller)
    {
        if (controller.gameOver)
            return false;

        return controller.alarmActive;
    }
}
