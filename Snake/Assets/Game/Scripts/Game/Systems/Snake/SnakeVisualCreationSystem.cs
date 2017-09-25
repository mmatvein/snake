using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;

    public class SnakeVisualCreationSystem : IExecuteSystem
    {
        readonly ViewContext viewContext;
        readonly IGroup<GameEntity> snakes;

        public SnakeVisualCreationSystem(Contexts contexts)
        {
            this.viewContext = contexts.view;

            this.snakes = contexts.game.GetGroup(GameMatcher.SnakePosition);
        }
     
        public void Execute()
        {
            foreach (var snake in this.snakes.GetEntities())
            {
                bool alreadyHadVisual = snake.hasSnakeVisual;
                List<ViewEntity> visuals = alreadyHadVisual ? snake.snakeVisual.linkedVisuals : new List<ViewEntity>();

                for (int i = 0; i < snake.snakePosition.positions.Count; i++)
                {
                    if (visuals.Count <= i)
                    {
                        ViewEntity newVisual = this.viewContext.CreateEntity();
                        newVisual.AddCreateView(typeof(SpriteRenderer));
                        visuals.Add(newVisual);
                    }
                }

                if (!alreadyHadVisual)
                    snake.AddSnakeVisual(visuals);
                else
                    snake.ReplaceSnakeVisual(visuals);
            }
        }
    }
}