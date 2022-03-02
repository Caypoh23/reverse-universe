using Cores.CoreComponents;
using Enemies.StateMachine;
using Enemies.States.Data;
using ObjectPool;
using Projectiles;
using UnityEngine;

namespace Enemies.States
{
    public class RangedAttackState : AttackState
    {
        protected readonly RangedAttackStateData StateData;

        protected GameObject projectile;
        protected Projectile projectileScript;

        public RangedAttackState(
            Entity entity, 
            FiniteStateMachine stateMachine, 
            int animBoolId, 
            Transform attackPosition,
            RangedAttackStateData stateData) : 
            base(
                entity, 
                stateMachine, 
                animBoolId, 
                attackPosition)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void TriggerAttack()
        {
            base.TriggerAttack();

            projectile = ObjectPooler.Instance.SpawnFromPool(StateData.projectileTag, AttackPosition.position, AttackPosition.rotation);
            //GameObject.Instantiate(StateData.projectile, AttackPosition.position, AttackPosition.rotation);
            projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.FireProjectile(StateData.projectileSpeed, StateData.projectileTravelDistance, StateData.projectileDamage);

            if(Core.Movement.IsRewinding) projectile.SetActive(false);
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
        }
    }
}
