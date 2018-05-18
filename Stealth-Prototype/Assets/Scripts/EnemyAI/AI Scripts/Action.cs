using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject {

    public abstract void Act(StateController controller);

    public abstract void Enter(StateController controller);

    public abstract void Exit(StateController controller);
}
