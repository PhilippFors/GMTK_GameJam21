using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy.AI.NormalMan.Transitions
{
    [CreateAssetMenu(menuName = "AI/Transitions/IsInRange")]
    public class IsInRange : Transition
    {
        public override bool Check(StateMachine stateMachine)
        {
            if (Vector3.Distance(stateMachine.Player.position, stateMachine.transform.position) > stateMachine.EnemyAttack.AttackRange)
            {
                if (!stateMachine.EnemyAttack.IsAttacking)
                {
                    stateMachine.NavMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                    return false;
                }
                else
                {
                    stateMachine.NavMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                    return true;
                }
            }
            else
            {
                stateMachine.NavMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                return true;
            }
        }
    }
}