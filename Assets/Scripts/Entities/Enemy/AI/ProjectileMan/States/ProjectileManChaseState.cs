using UnityEngine;

namespace Entities.Enemy.AI.ProjectileMan.States
{
    [CreateAssetMenu(menuName = "AI/Projectile Man/States/Chase")]
    public class ProjectileManChaseState : State
    {
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [SerializeField] private float findNewPointDistance;

        public override void Tick(StateMachine stateMachine)
        {
            var att = (ProjectileManAttack) stateMachine.EnemyAttack;
            Move(stateMachine, att);
        }

        private void Move(StateMachine stateMachine, ProjectileManAttack att)
        {
            var newDest = AISteering.AvoidanceSteering(stateMachine.transform.forward, stateMachine, att.CurrentPoint);
            stateMachine.NavMeshAgent.destination = newDest;
            
            var dir = Vector3.zero;
            if (Vector3.Distance(stateMachine.Player.position, stateMachine.transform.position) >
                findNewPointDistance || stateMachine.NavMeshAgent.remainingDistance < 0.8f)
            {
                dir = stateMachine.Player.position - stateMachine.transform.position;
            }
            else
            {
                dir = newDest - stateMachine.transform.position;
            }
            
            dir.y = 0;

            var newRot = Quaternion.LookRotation(dir);
            stateMachine.transform.rotation =
                Quaternion.Lerp(stateMachine.transform.rotation, newRot, 7f * Time.deltaTime);
            stateMachine.NavMeshAgent.Move(stateMachine.transform.forward * stateMachine.EnemyBase.MovementSpeed *
                                           Time.deltaTime);
        }

        public override void OnStateEnter(StateMachine stateMachine)
        {
            stateMachine.Animator.Play("Running");
            stateMachine.Animator.SetBool("isRunning", true);
            var att = (ProjectileManAttack) stateMachine.EnemyAttack;
            stateMachine.NavMeshAgent.isStopped = false;
            att.FindPointNearPlayer(stateMachine.Player.position, minDistance, maxDistance);
        }

        public override void OnStateExit(StateMachine stateMachine)
        {
            stateMachine.Animator.SetBool("isRunning", false);
            stateMachine.Animator.Play("Idle");
            stateMachine.NavMeshAgent.isStopped = true;
        }
    }
}