using UnityEngine;

namespace Entities.Enemy.AI.FastMan.States
{
    public class FastManChaseState : State
    {
        public override void Tick(StateMachine stateMachine)
        {
            stateMachine.NavMeshAgent.destination = stateMachine.Player.position;

            var dir = stateMachine.Player.position - stateMachine.transform.position;
            
            dir.y = 0;
            
            stateMachine.transform.rotation = Quaternion.LookRotation(dir);
            stateMachine.NavMeshAgent.Move(stateMachine.transform.forward * stateMachine.EnemyBase.MovementSpeed * Time.deltaTime);
        }
    }
}
