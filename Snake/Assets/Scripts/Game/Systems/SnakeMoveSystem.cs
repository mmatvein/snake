using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.Systems
{
    using ECS;
    using Game.Components;

    public class SnakeMoveSystem : ISystemTicks
    {
        private readonly IEntityDB entityDB;

        public SnakeMoveSystem(IEntityDB entityDB)
        {
            this.entityDB = entityDB;
        }

        public void Update()
        {
            this.entityDB
                .GetEntitiesWithComponents<Component<SnakePosition>, Component<Velocity>>() 
                .Subscribe(
                    entity =>
                        {
                            this.UpdateMovement(
                                this.entityDB.GetComponent<Component<SnakePosition>>(entity),
                                this.entityDB.GetComponent<Component<Velocity>>(entity)
                            );
                        }
                );
        }

        private void UpdateMovement(Component<SnakePosition> snakePosition, Component<Velocity> velocity)
        {
            Debug.Log("Updating Movement");

            SnakePosition currentPosition = snakePosition.CurrentValue;
            Velocity currentVelocity = velocity.CurrentValue;

            List<Vector2> newPositions = new List<Vector2>(currentPosition.positions);
            Vector2 newHeadPosition = newPositions[0] + currentVelocity.velocity;
            newPositions.Insert(0, newHeadPosition);
            newPositions.RemoveAt(newPositions.Count - 1);
            snakePosition.SetValue(new SnakePosition(newPositions));
        }
    }
}