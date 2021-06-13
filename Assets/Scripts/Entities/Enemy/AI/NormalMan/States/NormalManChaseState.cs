using DG.Tweening;
using UnityEngine;

namespace Entities.Enemy.AI.NormalMan
{
    [CreateAssetMenu(menuName = "AI/Normal Man/States/Chase")]
    public class NormalManChaseState : State
    {
        public override void Tick(StateMachine stateMachine)
        {
            var destination = AISteering.AvoidanceSteering(stateMachine.transform.forward, stateMachine, stateMachine.Player.position);

            stateMachine.NavMeshAgent.destination = destination;

            var dir = destination - stateMachine.transform.position;
            
            dir.y = 0;
            
            Vector3 moveTo = stateMachine.transform.forward * (stateMachine.EnemyBase.MovementSpeed * Time.deltaTime);

            stateMachine.NavMeshAgent.Move(moveTo);
            // stateMachine.NavMeshAgent.Move(stateMachine.transform.forward * stateMachine.EnemyBase.MovementSpeed * Time.deltaTime);
        }

        public override void OnStateEnter(StateMachine stateMachine)
        {
            stateMachine.Animator.SetBool("isRunning", true);
        }

        public override void OnStateExit(StateMachine stateMachine)
        {
            stateMachine.Animator.SetBool("isRunning", false);
        }
    }
}
