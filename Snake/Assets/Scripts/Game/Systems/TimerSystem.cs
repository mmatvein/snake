using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.Systems
{
    using ECS;
    using Components;
    public class TimerIncrementSystem : ISystemContinuous
    {
        private readonly IEntityDB entityDB;

        public TimerIncrementSystem(IEntityDB entityDB)
        {
            this.entityDB = entityDB;
        }

        public void Update(float dt)
        {
            this.entityDB.GetEntitiesWithComponent<Timer>()
                .Subscribe(entity => this.UpdateTimer(dt, entity.Item2));
        }

        void UpdateTimer(float dt, Component<Timer> timer)
        {
            float newTimerValue = timer.Value.value + dt;

            timer.SetValue(new Timer(newTimerValue));
        }
    }
}