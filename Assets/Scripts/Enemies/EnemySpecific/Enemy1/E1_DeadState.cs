﻿using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.EnemySpecific.Enemy1
{
    public class E1_DeadState : DeadState
    {
        private readonly Enemy1 _enemy;

        public E1_DeadState(Entity entity, FiniteStateMachine stateMachine,
            string animBoolName, D_DeadState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName,
            stateData)
        {
            _enemy = enemy;
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
    }
}