using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;

    public class TickerSystem : IExecuteSystem
    {
        readonly GameContext gameContext;
        readonly IGroup<GameEntity> tickers;

        public TickerSystem(Contexts contexts)
        {
            this.gameContext = contexts.game;
            this.tickers = this.gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Timer, GameMatcher.Ticker));
        }

        public void Execute()
        {
            foreach (GameEntity entity in this.tickers.GetEntities())
            {
                float timerValue = entity.timer.value;
                float tickLength = entity.ticker.tickLength;

                if (timerValue >= tickLength)
                {
                    int ticks = entity.ticker.currentTick + 1;
                    timerValue -= tickLength;
                    entity.ReplaceTimer(timerValue, entity.timer.dt);
                    entity.ReplaceTicker(tickLength, ticks);
                }
            }
        }
    }
}