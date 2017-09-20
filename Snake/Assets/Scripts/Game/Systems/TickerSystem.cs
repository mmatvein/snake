using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.Systems
{
    using ECS;
    using Components;
    public class TickerSystem : ISystemContinuous
    {
        private readonly IEntityDB entityDB;

        public TickerSystem(IEntityDB entityDB)
        {
            this.entityDB = entityDB;
        }

        public void Update(float dt)
        {
            this.entityDB.GetEntitiesWithComponents<Timer, Ticker>()
                .Subscribe(entity => this.UpdateTicker(entity.Item2, entity.Item3));
        }

        void UpdateTicker(Component<Timer> timer, Component<Ticker> ticker)
        {
            float tickLength = ticker.Value.tickLength;

            while (timer.Value.value >= tickLength)
            {
                ticker.SetValue(new Ticker(tickLength, ticker.Value.currentTick + 1));
                timer.SetValue(new Timer(timer.Value.value - tickLength));
            }
        }
    }
}