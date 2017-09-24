using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;
    public class TimerSystem : IExecuteSystem
    {
        readonly GameContext gameContext;
        readonly IGroup<GameEntity> timers;

        public TimerSystem(Contexts contexts)
        {
            this.gameContext = contexts.game;

            this.timers = this.gameContext.GetGroup(GameMatcher.Timer);
        }

        public void Execute()
        {
            foreach (GameEntity entity in this.timers.GetEntities())
            {
                float timerValue = entity.timer.value;
                entity.ReplaceTimer(timerValue += Time.deltaTime);
            }
        }
    }
}