﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.Systems
{
    using ECS;
    public class SnakeDirectionSystem : ISystemDeltaTime
    {
        private readonly IEntityDB entityDB;

        public SnakeDirectionSystem(IEntityDB entityDB)
        {
            this.entityDB = entityDB;
        }

        public void Update(float dt)
        {
            this.entityDB.GetEntitiesWithComponents<Component<Components.Input>, Component<Components.SnakeDirection>>()
                .Subscribe(
                    entity =>
                        {
                            this.SetDirection(
                                this.entityDB.GetComponent<Component<Components.Input>>(entity),
                                this.entityDB.GetComponent<Component<Components.SnakeDirection>>(entity));
                        }
                );
        }

        void SetDirection(Component<Components.Input> input, Component<Components.SnakeDirection> snakeDirection)
        {
            Components.Direction currentDirection = snakeDirection.CurrentValue.currentDirection;
            Components.Direction wantedDirection = snakeDirection.CurrentValue.currentWantedDirection;
            Components.Input currentInput = input.CurrentValue;
            
            switch (currentDirection)
            {
                case Components.Direction.Left:
                case Components.Direction.Right:
                    if (currentInput.up)
                        wantedDirection = Components.Direction.Up;
                    else if (currentInput.down)
                        wantedDirection = Components.Direction.Down;
                    break;
                case Components.Direction.Up:
                case Components.Direction.Down:
                    if (currentInput.right)
                        wantedDirection = Components.Direction.Right;
                    else if (currentInput.left)
                        wantedDirection = Components.Direction.Left;
                    break;
            }

            snakeDirection.SetValue(new Components.SnakeDirection(currentDirection, wantedDirection));
        }
    }
}