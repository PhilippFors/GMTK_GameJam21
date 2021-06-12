using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy.AI
{
    public class StateMachine : MonoBehaviour
    {
        public NavMeshAgent NavMeshAgent => navMeshAgent;
        public EnemyAttack AttackBehaviour => attackBehaviour;
        public EnemyBase EnemyBase => enemyBase;
        public Transform Player => player;
        
        [SerializeField] private Transform player;
        [SerializeField] private bool isActive;
        [SerializeField] private State startState;
        [SerializeField] private Transition[] anyTransitions;
        
        private EnemyAttack attackBehaviour;
        private EnemyBase enemyBase;
        private NavMeshAgent navMeshAgent;
        private State currentState;

        private void Awake()
        {
            attackBehaviour = GetComponent<EnemyAttack>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyBase = GetComponent<EnemyBase>();
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
                    SwitchStates(transition.TrueState);
                }
            }
        }
        
        public void SwitchStates(State nextState)
        {
            if (nextState != null && nextState != currentState)
            {
                currentState.OnStateExit(this);
                currentState = nextState;
                currentState.OnStateEnter(this);
            }
        }
    }
}
