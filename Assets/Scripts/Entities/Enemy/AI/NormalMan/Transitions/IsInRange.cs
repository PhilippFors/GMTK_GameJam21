using UnityEngine;

namespace Entities.Enemy.AI.NormalMan.Transitions
{
    public class IsInRange : Transition
    {
        public override bool Check(StateMachine stateMachine)
        {
            return (stateMachine.transform.position - stateMachine.Player.position).sqrMagnitude < Mathf.Pow(stateMachine.AttackBehaviour.AttackRange, 2);
        }
    }
}
