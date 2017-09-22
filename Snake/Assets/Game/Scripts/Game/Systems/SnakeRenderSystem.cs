using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.Systems
{
    using ECS;
    using Game.Components;
    using Game.Components.Visuals;

    public class SnakeRenderSystem : ISystemContinuous
    {
        private readonly IEntityDB entityDB;

        public SnakeRenderSystem(IEntityDB entityDB)
        {
            this.entityDB = entityDB;
        }

        public void Update(float dt)
        {
            this.entityDB.GetEntitiesWithComponents<SnakePosition, SnakeVisuals>()
               .Subscribe(entity => this.UpdateRenderer(entity.Item2, entity.Item3));
        }

        private void UpdateRenderer(Component<SnakePosition> snakePosition, Component<SnakeVisuals> snakeVisuals)
        {
            List<GameObject> visualBlocks = snakeVisuals.Value.snakeBlocks;
            if (visualBlocks != null)
                foreach (var gameObject in visualBlocks)
                    GameObject.Destroy(gameObject);

            List<Vector2> positions = snakePosition.Value.positions;
            List<GameObject> newVisualBlocks = new List<GameObject>(positions.Count);

            foreach (var position in positions)
            {
                GameObject block = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                block.transform.position = position;
                newVisualBlocks.Add(block);
            }

            snakeVisuals.SetValue(new SnakeVisuals(newVisualBlocks));
            
        }
    }
}