using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;
    using Components;

    public class SnakeDirectionChangeSystem : ReactiveSystem<InputEntity>
    {
        readonly IGroup<GameEntity> controllableSnakes;
        public SnakeDirectionChangeSystem(Contexts contexts) : base(contexts.input)
        {
            this.controllableSnakes = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.SnakeDirection, GameMatcher.ReactToInput));
        }

        protected override void Execute(List<InputEntity> entities)
        {
            Input gatheredInput = new Input();
            foreach (var inputEntity in entities)
            {
                gatheredInput.up |= inputEntity.input.up;
                gatheredInput.down |= inputEntity.input.down;
                gatheredInput.left |= inputEntity.input.left;
                gatheredInput.right |= inputEntity.input.right;
            }

            foreach (var snake in this.controllableSnakes.GetEntities())
            {
                SnakeDirection snakeDirection = snake.snakeDirection;
                Direction newWantedDirection = snakeDirection.currentWantedDirection;

                if (gatheredInput.up)
                    newWantedDirection = Direction.Up;
                else if (gatheredInput.down)
                    newWantedDirection = Direction.Down;
                else if (gatheredInput.left)
                    newWantedDirection = Direction.Left;
                else if (gatheredInput.right)
                    newWantedDirection = Direction.Right;

                snake.ReplaceSnakeDirection(snakeDirection.currentDirection, newWantedDirection);
            }
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasInput;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Input);
        }
    }
}