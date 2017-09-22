using UnityEngine;
using UniRx;

namespace Game.Systems
{
    using ECS;
    public class SnakeVelocitySystem : ISystemReactive
    { 
        private readonly IEntityDB entityDB;
        System.IDisposable tickerSubscription;

        public SnakeVelocitySystem(IEntityDB entityDB)
        {
            this.entityDB = entityDB;
        }

        public void Register()
        {
            this.tickerSubscription = this.entityDB.GetObservableComponentStream<Components.Ticker>()
                .Subscribe(this.HandleTick);
        }

        public void Unregister()
        {
            this.tickerSubscription.Dispose();
        }

        void HandleTick(Component<Components.Ticker> ticker)
        {
            this.entityDB.GetEntitiesWithComponents<Components.SnakeDirection, Components.Velocity>()
                .Subscribe(entity => this.SetVelocity(entity.Item2, entity.Item3));
        }

        void SetVelocity(Component<Components.SnakeDirection> snakeDirection, Component<Components.Velocity> snakeVelocity)
        {
            Vector2 newVelocity = snakeVelocity.Value.velocity;
            Components.Direction wantedDirection = snakeDirection.Value.currentWantedDirection;
            switch (wantedDirection)
            {
                case Components.Direction.Up:
                    newVelocity = Vector2.up;
                    break;
                case Components.Direction.Down:
                    newVelocity = Vector2.down;
                    break;
                case Components.Direction.Right:
                    newVelocity = Vector2.right;
                    break;
                case Components.Direction.Left:
                    newVelocity = Vector2.left;
                    break;
            }

            snakeVelocity.SetValue(new Components.Velocity(newVelocity));
            snakeDirection.SetValue(new Components.SnakeDirection(wantedDirection, wantedDirection));
        }
    }
}