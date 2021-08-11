using Enemies.States;
using UnityEngine;

namespace Intermediaries
{
    public class AnimationToStateMachine : MonoBehaviour
    {
        public AttackState AttackState;

        public void TriggerAttack()
        {
            AttackState.TriggerAttack();
        }

        public void FinishAttack()
        {
            AttackState.FinishAttack();
        }
    }
}