using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;
    using Framework;
    using UniRx;

    public class SnakeVisualCreationSystem : IExecuteSystem
    {
        readonly ViewContext viewContext;
        readonly IGroup<GameEntity> snakes;

        readonly AssetBundleManager assetBundleManager;

        public SnakeVisualCreationSystem(Contexts contexts, AssetBundleManager assetBundleManager)
        {
            this.viewContext = contexts.view;
            this.assetBundleManager = assetBundleManager;

            this.snakes = contexts.game.GetGroup(GameMatcher.SnakePosition);
        }
     
        public void Execute()
        {
            foreach (var snake in this.snakes.GetEntities())
            {
                bool alreadyHadVisual = snake.hasSnakeVisual;
                List<ViewEntity> visuals = alreadyHadVisual ? snake.snakeVisual.linkedVisuals : new List<ViewEntity>();
                Definitions.SnakeVisualDefinition snakeVisualDefinition = alreadyHadVisual ? 
                    snake.snakeVisual.snakeVisualDefinition : 
                    this.assetBundleManager.LoadAsset(new AssetDefinition<Definitions.SnakeVisualDefinition>("definitions", "Main Player Snake")).Wait();

                for (int i = 0; i < snake.snakePosition.positions.Count; i++)
                {
                    if (visuals.Count <= i)
                    {
                        ViewEntity newVisual = this.viewContext.CreateEntity();
                        newVisual.AddCreateView(typeof(SpriteRenderer));
                        Components.SpriteVisual spriteVisual = new Components.SpriteVisual()
                        {
                            sprite = this.assetBundleManager.LoadAsset(new AssetDefinition<Sprite>("art", "Snake_Tile0")).Wait(),
                            sortingLayer = "Default",
                            sortingOrder = 5
                        };
                        newVisual.AddSpriteVisual(spriteVisual);
                        newVisual.AddPosition(snake.snakePosition.positions[i]);
                        visuals.Add(newVisual);
                    }
                }

                if (!alreadyHadVisual)
                    snake.AddSnakeVisual(snakeVisualDefinition, visuals);
                else
                    snake.ReplaceSnakeVisual(snakeVisualDefinition, visuals);
            }
        }
    }
}