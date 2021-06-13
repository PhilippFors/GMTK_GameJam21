using UnityEngine;

namespace Entities.Enemy.AI.NormalMan.States
{
    [CreateAssetMenu(menuName = "AI/Normal Man/States/Attack")]
    public class NormalManAttackState : State
    {
        public override void Tick(StateMachine stateMachine)
        {
            if (!CheckInFront(stateMachine) && !stateMachine.EnemyAttack.IsAttacking)
            {
                var dir = stateMachine.Player.position - stateMachine.transform.position;
            
                dir.y = 0;

                var newRot = Quaternion.LookRotation(dir);
                stateMachine.transform.rotation =
                    Quaternion.Lerp(stateMachine.transform.rotation, newRot, 7f * Time.deltaTime);
            }
            else
            {
                stateMachine.EnemyAttack.Attack();
            }
        }
        bool CheckInFront(StateMachine stateMachine)
        {
            Ray ray = new Ray(stateMachine.transform.position, stateMachine.transform.forward);
            return Physics.Raycast(ray, stateMachine.EnemyAttack.AttackRange + 1, LayerMask.GetMask("Player"));
        }
        
        public override void OnStateEnter(StateMachine stateMachine)
        {
            stateMachine.NavMeshAgent.isStopped = true;
        }

        public override void OnStateExit(StateMachine stateMachine)
        {
            stateMachine.NavMeshAgent.isStopped = false;
        }
    }
}