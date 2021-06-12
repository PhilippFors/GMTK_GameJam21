using UnityEngine;

namespace Entities.Enemy.AI.NormalMan.Transitions
{
    [CreateAssetMenu(menuName = "AI/Transitions/IsInRange")]
    public class IsInRange : Transition
    {
        public override bool Check(StateMachine stateMachine)
        {
            return (stateMachine.transform.position - stateMachine.Player.position).sqrMagnitude < Mathf.Pow(stateMachine.AttackBehaviour.AttackRange, 2);
        }
    }
}
