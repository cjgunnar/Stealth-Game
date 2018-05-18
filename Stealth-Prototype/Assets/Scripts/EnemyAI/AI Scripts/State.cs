using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject {

    public Action[] actions;
    public Transition[] transitions;

    public void EnterState (StateController controller)
    {
        if (actions.Length != 0)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Enter(controller);
            }
        }
    }


    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions (StateController controller)
    {
        if (actions.Length!= 0)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(controller);
            }
        }
        else
        {
            Debug.Log("no actions assigned for current state: " + controller.currentState);
        }
    }

    private void CheckTransitions (StateController controller)
    {
        if (transitions.Length != 0)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                bool DecisionSucceeded = transitions[i].decision.Decide(controller);

                if (DecisionSucceeded)
                {
                    controller.TransitionToState(transitions[i].trueState);
                }
                else
                {
                    controller.TransitionToState(transitions[i].falseState);
                }
            }
        }
        else
        {
            Debug.Log("no transitions assigned for current state: " + controller.currentState);
        }
    }

    public void ExitState (StateController controller)
    {
        if (actions.Length != 0)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Exit(controller);
            }
        }
        else
        {
            //Debug.Log("no actions assigned for current state");
        }

    }

}
