using UnityEngine;

namespace Entities.Enemy.AI
{
    public static class AISteering
    {
        public static Vector3 AvoidanceSteering(Vector3 dir, StateMachine controller)
        {
            RaycastHit hit;

            if (!FindObstacle(dir, out hit, controller, false))
            {
                return controller.Player.position;
            }

            Vector3 targetpos = controller.Player.position;

            float angle = Vector3.Angle(dir * Time.deltaTime, hit.normal);
            if (angle > 165f)
            {
                Vector3 perp;

                perp = new Vector3(-hit.normal.z, hit.normal.y, hit.normal.x);

                targetpos = targetpos + (perp * Mathf.Sin((angle - 165f) * Mathf.Deg2Rad) * 2 * controller.ObstacleAvoidDistance);
            }

            return Seek(targetpos, controller);
        }

        // public void IsGrounded(StateMachine controller)
        // {
        //     Vector3 velocity = Vector3.zero;
        //     if (!Physics.CheckSphere(controller.transform.position + new Vector3(0, 1, 0), 1.1f))
        //     {
        //         controller.isGrounded = false;
        //         DoGravity(controller, velocity);
        //     }
        //     else
        //     {
        //         controller.isGrounded = true;
        //     }
        // }

        // private void DoGravity(StateMachine controller, Vector3 velocity)
        // {
        //     velocity.y = Physics.gravity.y * Time.deltaTime;
        //     controller.transform.position += velocity;
        // }

        private static Vector3 Seek(Vector3 targetPosition, StateMachine controller)
        {
            Vector3 acceleration = SteerTowards(targetPosition, controller);

            acceleration.Normalize();

            return acceleration;
        }

        private static Vector3 SteerTowards(Vector3 vector, StateMachine controller)
        {
            Vector3 v = vector.normalized * (controller.EnemyBase.MovementSpeed);
            return Vector3.ClampMagnitude(v, controller.EnemyBase.MovementSpeed);
        }

        private static bool FindObstacle(Vector3 dir, out RaycastHit hit, StateMachine controller, bool findPlayer)
        {
            dir = dir.normalized;

            Vector3[] dirs = new Vector3[controller.WhiskerAmount];
            dirs[0] = dir;

            float orientation = VectorToOrientation(dir);
            float angle = orientation;
            for (int i = 1; i < (dirs.Length + 1) / 2; i++)
            {
                angle += controller.AngleIncrement;
                dirs[i] = OrientationToVector(orientation + angle * Mathf.Deg2Rad);
            }
            angle = orientation;
            for (int i = (dirs.Length + 1) / 2; i < dirs.Length; i++)
            {
                angle -= controller.AngleIncrement;
                dirs[i] = OrientationToVector(orientation - angle * Mathf.Deg2Rad);
            }
            return CastWhiskers(dirs, out hit, controller, findPlayer);
        }

        private static bool CastWhiskers(Vector3[] dirs, out RaycastHit firsthit, StateMachine controller, bool findPlayer)
        {
            firsthit = new RaycastHit();
            for (int i = 0; i < dirs.Length; i++)
            {
                RaycastHit hit;

                if (findPlayer)
                {
                    if (Physics.SphereCast(controller.transform.position, 1f, dirs[i], out hit, controller.EnemyAttack.AttackRange, LayerMask.GetMask("Player")))
                    {
                        firsthit = hit;
                        return true;
                    }
                }
                else
                {
                    float dist = (i == 0) ? controller.MainWhiskerLength : controller.SecondaryWhiskerLength;
                    if (Physics.SphereCast(controller.transform.position, 1f, dirs[i], out hit, dist, LayerMask.GetMask("Enemy")))
                    {
                        firsthit = hit;
                        return true;
                    }
                }

            }
            return false;
        }

        static Vector3 OrientationToVector(float orientation)
        {
            return new Vector3(Mathf.Cos(-orientation), 0, Mathf.Sin(-orientation));
        }

        static float VectorToOrientation(Vector3 direction)
        {
            return -1 * Mathf.Atan2(direction.z, direction.x);
        }

        public static bool IsInFront(Vector3 target, StateMachine controller)
        {
            return IsFacing(target, 0, controller);
        }

        public static bool IsFacing(Vector3 target, float cosineValue, StateMachine controller)
        {
            Vector3 facing = controller.transform.right.normalized;

            Vector3 directionToTarget = (target - controller.transform.position);
            directionToTarget.Normalize();

            return Vector3.Dot(facing, directionToTarget) >= cosineValue;
        }
    }
}
