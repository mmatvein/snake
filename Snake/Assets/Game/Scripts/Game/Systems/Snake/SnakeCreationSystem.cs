using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;
    public class SnakeCreationSystem : IInitializeSystem
    {
        readonly GameContext gameContext;

        public SnakeCreationSystem(Contexts contexts)
        {
            this.gameContext = contexts.game;
        }

        public void Initialize()
        {
            GameEntity snake = this.gameContext.CreateEntity();
            snake.AddSnakePosition(new List<Vector2>() {
                new Vector2(1, 1), new Vector2(1, 0), new Vector2(2, 0),
                new Vector2(3, 0), new Vector2(3, 1), new Vector2(4, 1),
                new Vector2(5, 1), new Vector2(6, 1), new Vector2(6, 2) });
            snake.AddSnakeDirection(Components.Direction.Left, Components.Direction.Left);
            snake.isReactToInput = true;
        }
    }
}
