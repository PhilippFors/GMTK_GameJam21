using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy.AI
{
    public class StateMachine : MonoBehaviour
    {
        public NavMeshAgent NavMeshAgent => navMeshAgent;
        public EnemyAttack EnemyAttack => enemyAttack;
        public EnemyBase EnemyBase => enemyBase;
        public Transform Player => player;
        public Animator Animator => animator;

        public float ObstacleAvoidDistance => obstacleAvoidDistance;
        public float AngleIncrement => angleIncrement;
        public int WhiskerAmount => whiskerAmount;
        public float MainWhiskerLength => mainWhiskerLength;
        public float SecondaryWhiskerLength => secondaryWhiskerLength;
        

        [SerializeField] private Transform player;
        [SerializeField] private bool isActive;
        [SerializeField] private State startState;
        [SerializeField] private Transition[] anyTransitions;

        [Header("Obstacle avoidance")]
        [SerializeField] private float obstacleAvoidDistance;
        [SerializeField] private float angleIncrement;
        [SerializeField] private int whiskerAmount;
        [SerializeField] private float mainWhiskerLength;
        [SerializeField] private float secondaryWhiskerLength;
        
        private EnemyAttack enemyAttack;
        private EnemyBase enemyBase;
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private State currentState;

        private void Awake()
        {
            enemyAttack = GetComponent<EnemyAttack>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyBase = GetComponent<EnemyBase>();
            animator = GetComponentInChildren<Animator>();
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
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
