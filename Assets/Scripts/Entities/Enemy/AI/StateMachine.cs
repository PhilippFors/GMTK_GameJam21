using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy.AI
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private bool isActive;
        [SerializeField] private State startState;
        [SerializeField] private Transition[] anyTransitions;
        private EnemyAttack attackBehaviour;
        private NavMeshAgent navMeshAgent;
        private State currentState;

        private void Awake()
        {
            attackBehaviour = GetComponent<EnemyAttack>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (!isActive)
            {
                return;
            }
            
            if (currentState == null)
            {
                currentState = startState;
                currentState.OnStateEnter(this);
            }
            
            currentState.StateUpdate(this);
            
        }

        public void EnableAI(bool value)
        {
            isActive = value;
        }

        private void CheckAnyTransition()
        {
            if (anyTransitions.Length == 0)
                return;

            foreach (Transition transition in anyTransitions)
            {
                if (transition.Check(this))
                {
                    SwitchStates(transition.NextState);
                }
            }
        }
        
        public void SwitchStates(State nextState)
        {
            if (nextState != null)
            {
                currentState.OnStateExit(this);
                currentState = nextState;
                currentState.OnStateEnter(this);
            }
        }
    }
}
