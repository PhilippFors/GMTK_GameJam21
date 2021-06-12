using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemy.AI
{
    public abstract class State : ScriptableObject
    {
        [SerializeField] private List<Transition> transitions;
        
        public void CheckTransitions(StateMachine stateMachine)
        {
            if (transitions != null && transitions.Count > 0)
            {
                foreach (Transition t in transitions)
                {
                    if (t.Check(stateMachine))
                    {
                        stateMachine.SwitchStates(t.NextState);
                    }
                }
            }
        }

        public void StateUpdate(StateMachine stateMachine)
        {
            Tick(stateMachine);
            CheckTransitions(stateMachine);
        }

        public abstract void Tick(StateMachine stateMachine);
        
        public virtual void OnStateEnter(StateMachine stateMachine){}
        public virtual void OnStateExit(StateMachine stateMachine){}
    }

    [System.Serializable]
    public abstract class Transition : ScriptableObject
    {
        public State NextState => nextState;
        [SerializeField] private State nextState;

        public abstract bool Check(StateMachine stateMachine);
    }
}
