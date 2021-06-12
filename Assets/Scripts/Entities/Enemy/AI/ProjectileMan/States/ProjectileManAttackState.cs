using UnityEngine;

namespace Entities.Enemy.AI.ProjectileMan.States
{
    [CreateAssetMenu(menuName = "AI/Projectile Man/States/Attack")]
    public class ProjectileManAttackState : State
    {
        [SerializeField] private LayerMask lineOfSightMask;
        public override void Tick(StateMachine stateMachine)
        {
            if (HasLineOfSight(stateMachine))
            {
                Debug.Log("Projectile shooting");
            }
            
            stateMachine.NavMeshAgent.destination = stateMachine.Player.position;

            var dir = stateMachine.Player.position - stateMachine.transform.position;

            dir.y = 0;

            stateMachine.transform.rotation = Quaternion.LookRotation(dir);
        }
        
        private bool HasLineOfSight(StateMachine stateMachine)
        {
            var dir = stateMachine.Player.position - stateMachine.transform.position;
            return Physics.Raycast(stateMachine.transform.position, dir, 50f, lineOfSightMask, QueryTriggerInteraction.Ignore);
        }
        
        
    }
}
