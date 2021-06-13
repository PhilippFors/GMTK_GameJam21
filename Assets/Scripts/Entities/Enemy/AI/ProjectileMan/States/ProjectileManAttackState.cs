using Entities.Player.Movement;
using UnityEngine;

namespace Entities.Enemy.AI.ProjectileMan.States
{
    [CreateAssetMenu(menuName = "AI/Projectile Man/States/Attack")]
    public class ProjectileManAttackState : State
    {
        [SerializeField] private LayerMask lineOfSightMask;

        private ProjectileManAttack att;
        private PlayerMovementController playerMovementController;

        public override void Tick(StateMachine stateMachine)
        {
            stateMachine.NavMeshAgent.destination = stateMachine.Player.position;

            var dir = LookAhead(stateMachine);

            dir.y = 0;

            var newRot = Quaternion.LookRotation(dir);
            stateMachine.transform.rotation =
                Quaternion.Lerp(stateMachine.transform.rotation, newRot, 6f * Time.deltaTime);
            
            if (HasLineOfSight(stateMachine))
            {
                stateMachine.EnemyAttack.Attack();
            }
        }

        private bool HasLineOfSight(StateMachine stateMachine)
        {
            var dir = stateMachine.Player.position - stateMachine.transform.position;
            return !Physics.Raycast(stateMachine.transform.position, dir, 50f, lineOfSightMask,
                QueryTriggerInteraction.Ignore);
        }

        private Vector3 LookAhead(StateMachine stateMachine)
        {

            if (InterceptionDirection(stateMachine.Player.position, stateMachine.transform.position,
                playerMovementController.CurrentVelocity, att.BulletSpeed, out var direction))
            {
                return direction * Random.Range(0.8f, 0.95f);
            }
            else
            {
                return stateMachine.Player.position - stateMachine.transform.position;
            }
        }

        private bool InterceptionDirection(Vector3 a, Vector3 b, Vector3 vA, float sB, out Vector3 result)
        {
            var aToB = b - a;
            var dC = aToB.magnitude;
            var alpha = Vector3.Angle(aToB, vA) * Mathf.Deg2Rad;
            var sA = vA.magnitude;
            var r = sA / sB;
            if (SolveQuadratic(1 - r * r, 2 * r * dC * Mathf.Cos(alpha), -dC * dC, out var root1, out var root2) == 0)
            {
                result = Vector3.zero;
                return false;
            }

            var dA = Mathf.Max(root1, root2);
            var t = dA / sB;
            var c = a + vA * t;
            result = (c - b).normalized;
            return true;
        }

        private int SolveQuadratic(float a, float b, float c, out float root1, out float root2)
        {
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                root1 = Mathf.Infinity;
                root2 = -root1;
                return 0;
            }

            root1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
            root2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
            return discriminant > 0 ? 2 : 1;
        }

        public override void OnStateEnter(StateMachine stateMachine)
        {
            att = (ProjectileManAttack) stateMachine.EnemyAttack;
            playerMovementController = stateMachine.Player.GetComponent<PlayerMovementController>();
        }
    }
}