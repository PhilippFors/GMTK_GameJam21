using UnityEngine;

namespace Entities.Enemy.AI.NormalMan.States
{
    [CreateAssetMenu(menuName = "AI/Normal Man/States/Attack")]
    public class NormalManAttackState : State
    {
        public override void Tick(StateMachine stateMachine)
        {
            Move(stateMachine);
            stateMachine.AttackBehaviour.Attack();
        }

        private void Move(StateMachine stateMachine)
        {
            if (!IsInRange(stateMachine, stateMachine.AttackBehaviour.AttackRange - 1f))
            {
                stateMachine.NavMeshAgent.isStopped = false;

                stateMachine.NavMeshAgent.destination = stateMachine.Player.position;

                var dir = stateMachine.Player.position - stateMachine.transform.position;

                dir.y = 0;

                stateMachine.transform.rotation = Quaternion.LookRotation(dir);
                stateMachine.NavMeshAgent.Move(stateMachine.transform.forward *
                                               (stateMachine.EnemyBase.MovementSpeed - 2f) * Time.deltaTime);
            }
            else
            {
                stateMachine.NavMeshAgent.isStopped = true;
            }
        }

        private bool IsInRange(StateMachine stateMachine, float range)
        {
            return (stateMachine.transform.position - stateMachine.Player.position).sqrMagnitude < Mathf.Pow(range, 2);
        }
    }
}