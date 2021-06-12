using UnityEngine;

namespace Entities.Enemy.AI.ProjectileMan.States
{
    [CreateAssetMenu(menuName = "AI/Projectile Man/States/Chase")]
    public class ProjectileManChaseState : State
    {
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [SerializeField] private float findNewPointDistance;
        [SerializeField] private float newPointTimer;
        private float currentTimer;
        private Vector3 currentPoint;
        public override void Tick(StateMachine stateMachine)
        {
            Move(stateMachine);
            Timer(stateMachine);
        }



        private void Move(StateMachine stateMachine)
        {
            if (Vector3.Distance(stateMachine.Player.position, stateMachine.transform.position) > findNewPointDistance || stateMachine.NavMeshAgent.remainingDistance < 0.5f)
            {
                currentTimer -= newPointTimer;
                FindPointNearPlayer(stateMachine);
            }
            
            stateMachine.NavMeshAgent.destination = currentPoint;

            var dir = currentPoint - stateMachine.transform.position;

            dir.y = 0;

            stateMachine.transform.rotation = Quaternion.LookRotation(dir);
            stateMachine.NavMeshAgent.Move(stateMachine.transform.forward * stateMachine.EnemyBase.MovementSpeed *
                                           Time.deltaTime);
        }

        private void Timer(StateMachine stateMachine)
        {
            if (currentTimer >= newPointTimer)
            {
                FindPointNearPlayer(stateMachine);
                currentTimer -= newPointTimer;
            }
            else
            {
                currentTimer += Time.deltaTime;
            }
        }

        private void FindPointNearPlayer(StateMachine stateMachine)
        {
            var player = stateMachine.Player.position;

            var randomXY = Random.insideUnitCircle;

            var randomPos = new Vector3(Random.Range(minDistance, maxDistance) * randomXY.x, 0,
                Random.Range(minDistance, maxDistance) * randomXY.y) + player;

            currentPoint = randomPos;
        }

        public override void OnStateEnter(StateMachine stateMachine)
        {
            stateMachine.NavMeshAgent.isStopped = false;
            FindPointNearPlayer(stateMachine);
        }

        public override void OnStateExit(StateMachine stateMachine)
        {
            stateMachine.NavMeshAgent.isStopped = true;
        }
    }
}