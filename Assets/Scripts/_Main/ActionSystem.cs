using System.Collections;
using System.Collections.Generic;

public class ActionSystem
{
    public Queue<IAction> ActionQueue {get; private set;}
    public IAction CurrentAction {get; private set;}
    public bool Busy;

    public ActionSystem()
    {
        ActionQueue = new();
        Busy = false;
    }

    public void AddAction(IAction action)
    {
        ActionQueue.Enqueue(action);
    }

    public IEnumerator ExecuteNextAction()
    {
        if (ActionQueue.Count > 0)
        {
            CurrentAction = ActionQueue.Dequeue();
        } else
        {
            CurrentAction = null;
            yield break;
        }

        Busy = true;

        yield return CurrentAction.Execute();

        CheckTriggeredAbilities(CurrentAction);

        Busy = false;
    }

    public IEnumerator ExecuteImmediate(IAction action)
    {
        yield return action.Execute();

        CheckTriggeredAbilities(action);
    }

    public void CheckTriggeredAbilities(IAction action)
    {
        // does nothing yet;
    }
}