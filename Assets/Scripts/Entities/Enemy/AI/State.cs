using System.Collections.Generic;
using UnityEngine;

namespace Entities.Enemy.AI
{
    public abstract class State : ScriptableObject
    {
        [SerializeField] private List<Transition> transitions;
        
        public void CheckTransitions(StateMachine stateMachine)
        {
            foreach (Transition t in transitions)
            {
                if (t.Check(stateMachine))
                {
                    stateMachine.SwitchStates(t.NextState);
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

    public abstract class Transition
    {
        public State NextState => nextState;
        [SerializeField] private State nextState;
        public abstract bool Check(StateMachine stateMachine);
    }
}
