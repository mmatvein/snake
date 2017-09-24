using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;
    public class TimerSystem : IExecuteSystem
    {
        public delegate float TimerSourceDelegate();

        readonly GameContext gameContext;
        readonly IGroup<GameEntity> timers;
        readonly TimerSourceDelegate source;

        public TimerSystem(Contexts contexts, TimerSourceDelegate source)
        {
            this.gameContext = contexts.game;
            this.timers = this.gameContext.GetGroup(GameMatcher.Timer);
            this.source = source;
        }

        public void Execute()
        {
            float dt = this.source();
            foreach (GameEntity entity in this.timers.GetEntities())
            {
                float timerValue = entity.timer.value;
                entity.ReplaceTimer(timerValue += dt, dt);
            }
        }
    }
}