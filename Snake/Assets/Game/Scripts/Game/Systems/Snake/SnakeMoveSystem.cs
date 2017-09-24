using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;

    public class SnakeMoveSystem : ReactiveSystem<GameEntity>
    {
        readonly GameContext gameContext;
        readonly IGroup<GameEntity> snakes;

        public SnakeMoveSystem(Contexts contexts) : base(contexts.game)
        {
            this.gameContext = contexts.game;

            this.snakes = this.gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.SnakePosition, GameMatcher.SnakeDirection));
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity snakeEntity in this.snakes.GetEntities())
            {
                List<Vector2> positions = new List<Vector2>(snakeEntity.snakePosition.positions);

                Vector2 update = Vector2.zero;
                Components.Direction direction = snakeEntity.snakeDirection.currentWantedDirection;
                switch (direction)
                {
                    case Components.Direction.Down:
                        update = Vector2.down;
                        break;
                    case Components.Direction.Up:
                        update = Vector2.up;
                        break;
                    case Components.Direction.Left:
                        update = Vector2.left;
                        break;
                    case Components.Direction.Right:
                        update = Vector2.right;
                        break;
                }

                Vector2 newHeadPosition = positions[0] + update;
                positions.Insert(0, newHeadPosition);
                positions.RemoveAt(positions.Count - 1);

                snakeEntity.ReplaceSnakePosition(positions);
                snakeEntity.ReplaceSnakeDirection(direction, direction);
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.MainTicker, GameMatcher.Ticker));
        }
    }
}