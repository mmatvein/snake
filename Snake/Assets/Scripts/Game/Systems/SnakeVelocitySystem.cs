using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.Systems
{
    using ECS;
    public class SnakeVelocitySystem : ISystemTicks
    { 
        private readonly IEntityDB entityDB;

        public SnakeVelocitySystem(IEntityDB entityDB)
        {
            this.entityDB = entityDB;
        }

        public void Update()
        {
            this.entityDB.GetEntitiesWithComponents<Component<Components.SnakeDirection>, Component<Components.Velocity>>()
                .Subscribe(
                    entity =>
                    {
                        this.SetVelocity(
                            this.entityDB.GetComponent<Component<Components.SnakeDirection>>(entity),
                            this.entityDB.GetComponent<Component<Components.Velocity>>(entity));
                    }
                );
        }

        void SetVelocity(Component<Components.SnakeDirection> snakeDirection, Component<Components.Velocity> snakeVelocity)
        {
            Vector2 newVelocity = snakeVelocity.CurrentValue.velocity;
            Components.Direction wantedDirection = snakeDirection.CurrentValue.currentWantedDirection;
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