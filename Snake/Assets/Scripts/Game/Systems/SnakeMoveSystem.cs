using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.Systems
{
    using ECS;
    using Game.Components;

    public class SnakeMoveSystem : ISystemReactive
    {
        private readonly IEntityDB entityDB;
        System.IDisposable tickerSubscription;

        public SnakeMoveSystem(IEntityDB entityDB)
        {
            this.entityDB = entityDB;
        }

        public void Register()
        {
            this.tickerSubscription = this.entityDB.GetObservableComponentStream<Ticker>()
                .Subscribe(this.HandleTick);
        }

        public void Unregister()
        {
            this.tickerSubscription.Dispose();
        }

        void HandleTick(Component<Ticker> ticker)
        {
            this.entityDB
                .GetEntitiesWithComponents<SnakePosition, Velocity>()
                .Subscribe(
                    entity =>
                    {
                        this.UpdateMovement(
                            this.entityDB.GetComponent<SnakePosition>(entity),
                            this.entityDB.GetComponent<Velocity>(entity)
                        );
                    }
                );
        }

        private void UpdateMovement(Component<SnakePosition> snakePosition, Component<Velocity> velocity)
        {
            SnakePosition currentPosition = snakePosition.Value;
            Velocity currentVelocity = velocity.Value;

            List<Vector2> newPositions = new List<Vector2>(currentPosition.positions);
            Vector2 newHeadPosition = newPositions[0] + currentVelocity.velocity;
            newPositions.Insert(0, newHeadPosition);
            newPositions.RemoveAt(newPositions.Count - 1);
            snakePosition.SetValue(new SnakePosition(newPositions));
        }
    }
}