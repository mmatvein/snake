using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.GameSystems
{
    using Entitas;
    using Components;
    using Definitions;

    public class SnakeVisualUpdateSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> snakeVisuals;

        public SnakeVisualUpdateSystem(Contexts contexts)
        {
            this.snakeVisuals = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.SnakePosition, GameMatcher.SnakeVisual));
        }

        public void Execute()
        {
            foreach (var gameEntity in this.snakeVisuals.GetEntities())
            {
                List<Vector2> positionParts = gameEntity.snakePosition.positions;
                List<ViewEntity> visualParts = gameEntity.snakeVisual.linkedVisuals;

                SnakeVisualDefinition visualDefinition = gameEntity.snakeVisual.snakeVisualDefinition;
                {
                    Direction headDirection = this.GetPartDirection(positionParts[1], positionParts[0]);
                    this.SetSprite(visualParts[0], this.GetHeadSprite(headDirection, visualDefinition));
                }
                
                for (int i = 1; i < visualParts.Count - 1; i++)
                {
                    Direction towardsHeadDirection = this.GetPartDirection(positionParts[i], positionParts[i - 1]);
                    Direction fromTailDirection = this.GetPartDirection(positionParts[i + 1], positionParts[i]);
                    this.SetSprite(visualParts[i], this.GetBodySprite(towardsHeadDirection, fromTailDirection, visualDefinition));
                }

                {
                    Direction tailDirection = this.GetPartDirection(positionParts[positionParts.Count - 1], positionParts[positionParts.Count - 2]);
                    this.SetSprite(visualParts[positionParts.Count - 1], this.GetTailSprite(tailDirection, visualDefinition));
                }

                for (int i = 0; i < visualParts.Count; i++)
                {
                    visualParts[i].ReplacePosition(positionParts[i]);
                }
            }
        }

        void SetSprite(ViewEntity viewEntity, Sprite sprite)
        {
            SpriteVisual spriteVisual = viewEntity.spriteVisual.value;
            spriteVisual.sprite = sprite;
            viewEntity.ReplaceSpriteVisual(spriteVisual);
        }

        Direction GetPartDirection(Vector2 from, Vector2 to)
        {
            Assert.IsTrue(from.x == to.x || from.y == to.y, "No support for diagonals!");

            if (from.x > to.x)
                return Direction.Left;
            else if (from.x < to.x)
                return Direction.Right;
            else if (from.y > to.y)
                return Direction.Down;
            else if (from.y < to.y)
                return Direction.Up;

            Assert.IsTrue(false, "Parts on top of each other");
            return Direction.Up;
        }

        Sprite GetHeadSprite(Direction direction, SnakeVisualDefinition visualDefinition)
        {
            switch (direction)
            {
                case Direction.Up:
                    return visualDefinition.headUp;
                case Direction.Down:
                    return visualDefinition.headDown;
                case Direction.Left:
                    return visualDefinition.headLeft;
                case Direction.Right:
                    return visualDefinition.headRight;
                default:
                    return visualDefinition.headUp;
            }
        }

        Sprite GetBodySprite(Direction towardsHeadDirection, Direction fromTailDirection, SnakeVisualDefinition visualDefinition)
        {
            Debug.Log("Towards Head: " + towardsHeadDirection + ", from tail: " + fromTailDirection);
            switch (fromTailDirection)
            {
                case Direction.Up:
                    switch (towardsHeadDirection)
                    {
                        case Direction.Up:
                            return visualDefinition.straightUp;
                        case Direction.Down:
                            Assert.IsTrue(false, "This direction should not happen");
                            return visualDefinition.straightUp;
                        case Direction.Left:
                            return visualDefinition.upToLeft;
                        case Direction.Right:
                            return visualDefinition.upToRight;
                        default:
                            return visualDefinition.straightUp;
                    }
                case Direction.Down:
                    switch (towardsHeadDirection)
                    {
                        case Direction.Up:
                            Assert.IsTrue(false, "This direction should not happen");
                            return visualDefinition.straightDown;
                        case Direction.Down:
                            return visualDefinition.straightDown;
                        case Direction.Left:
                            return visualDefinition.downToLeft;
                        case Direction.Right:
                            return visualDefinition.downToRight;
                        default:
                            return visualDefinition.straightDown;
                    }
                case Direction.Left:
                    switch (towardsHeadDirection)
                    {
                        case Direction.Up:
                            return visualDefinition.leftToUp;
                        case Direction.Down:
                            return visualDefinition.leftToDown;
                        case Direction.Left:
                            return visualDefinition.straightLeft;
                        case Direction.Right:
                            Assert.IsTrue(false, "This direction should not happen");
                            return visualDefinition.straightLeft;
                        default:
                            return visualDefinition.straightLeft;
                    }
                case Direction.Right:
                    switch (towardsHeadDirection)
                    {
                        case Direction.Up:
                            return visualDefinition.rightToUp;
                        case Direction.Down:
                            return visualDefinition.rightToDown;
                        case Direction.Left:
                            Assert.IsTrue(false, "This direction should not happen");
                            return visualDefinition.straightRight;
                        case Direction.Right:
                            return visualDefinition.straightRight;
                        default:
                            return visualDefinition.straightRight;
                    }
                default:
                    return visualDefinition.straightUp;
            }   
        }

        Sprite GetTailSprite(Direction direction, SnakeVisualDefinition visualDefinition)
        {
            switch (direction)
            {
                case Direction.Up:
                    return visualDefinition.tailUp;
                case Direction.Down:
                    return visualDefinition.tailDown;
                case Direction.Left:
                    return visualDefinition.tailLeft;
                case Direction.Right:
                    return visualDefinition.tailRight;
                default:
                    return visualDefinition.tailUp;
            }
        }
    }
}